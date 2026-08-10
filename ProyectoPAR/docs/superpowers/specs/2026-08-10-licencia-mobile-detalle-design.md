# Componente LicenciaMobileDetalle

## Contexto

`Pages/Procesos/LicenciasMobileList.razor` (ya existente) navega a
`./licenciasmobiledetalle/{id}/{modo}` desde sus acciones de agregar
(`OnClickAdd`), seleccionar fila (`OnSelect`) y borrar (`OnDelete`), pero el
componente de destino no existe todavía. `Data/Interfaces/IProcesoService.cs`
y `Data/ProcesoService.cs` ya exponen `GetLicenciaMobile()` y
`GetLicenciaMobile(int id)`.

El modelo `LicenciaMobile` (`ProyectoParLibs.Models.Procesos.LicenciaMobile`,
compilado en `Dll/ProyectoParLibs.dll`) tiene:

```csharp
long Id;
string ClienteId;
string Compania;
string ApiUrl;
string ApiUser;
string ApiClientId;
string ApiPassword;
string ApiDataBase;
string ApiSchema;
bool ActivarMarcador;
DateTime FechaCreacion;
DateTime FechaModificacion;
string UsuarioCreacion;
string UsuarioModificacion;
Cliente Cliente; // navegación
```

A diferencia de `Licencia` (que llega al Cliente indirectamente vía
`Contrato.Cliente` y por eso necesita la clase auxiliar `LicenciaExt`),
`LicenciaMobile` tiene `ClienteId`/`Cliente` directamente en el modelo — igual
que `Contrato`. Por eso este componente sigue el patrón de
`Pages/Procesos/ContratosDetalle.razor` (formulario plano de una sola
sección, sin clase `*Ext`, con un campo local `NombreCliente` para mostrar el
nombre del cliente encontrado), en vez del patrón de `LicenciasDetalle.razor`
(que tiene secciones condicionales por tipo de producto — no aplica aquí, no
hay variación de "tipo" en `LicenciaMobile`).

## Componente

- Archivo nuevo: `Pages/Procesos/LicenciaMobileDetalle.razor`
- `@page "/licenciasmobiledetalle/{Id:int}/{Modo:int}"` — coincide con la
  navegación ya presente en `LicenciasMobileList.razor`.
- `@inherits DialogSettingLayout` (necesario para abrir `ClientesPop` como
  diálogo con `Resize`/`Drag`/`Settings`, igual que `ContratosDetalle`).
- `Titulo = "Licencia Móvil"`, `UrlRetorno = "./licenciasmobilelist"`.
- Estructura de marcado: `PageReturn` + `RadzenTemplateForm<LicenciaMobile>`
  dentro de un `RadzenFieldset`, con los botones Guardar/Cancelar al final —
  mismo layout que `ContratosDetalle.razor`.

## Campos del formulario

| Campo | Control Radzen | Bind | Obligatorio | Notas |
|---|---|---|---|---|
| Id | `RadzenNumeric` | `licenciaMobile.Id` | — | `Disabled="true"`, visible solo si `Modo != 1` |
| Cliente | botón búsqueda + `RadzenTextBox` + `RadzenTextBox` readonly | `licenciaMobile.ClienteId` / `NombreCliente` | Sí | patrón `OnShowModalClientes`/`BuscarCliente`/`HideModalClientes` de `ContratosDetalle` |
| Compañía | `RadzenTextBox` | `licenciaMobile.Compania` | Sí | |
| URL de API | `RadzenTextBox` | `licenciaMobile.ApiUrl` | Sí | |
| Usuario de API | `RadzenTextBox` | `licenciaMobile.ApiUser` | Sí | |
| Password de API | `RadzenTextBox` | `licenciaMobile.ApiPassword` | Sí | texto plano (confirmado con el usuario, no `RadzenPassword`) |
| Client Id de API | `RadzenTextBox` | `licenciaMobile.ApiClientId` | Sí | |
| Base de Datos de API | `RadzenTextBox` | `licenciaMobile.ApiDataBase` | Sí | |
| Schema de API | `RadzenTextBox` | `licenciaMobile.ApiSchema` | Sí | |
| Activar Marcador | `RadzenSwitch` | `licenciaMobile.ActivarMarcador` | — | default `false` al crear (Modo 1) |

Todos los campos de texto están deshabilitados (`Disabled="@soloLectura"`)
cuando `Modo == 3` (borrar), igual que en los demás formularios Detalle.

## Búsqueda de cliente

Mismo patrón que `ContratosDetalle.razor` (líneas 392-446):

```csharp
private async Task BuscarCliente()
{
    if (String.IsNullOrEmpty(licenciaMobile!.ClienteId))
    {
        NombreCliente = "";
        return;
    }

    var cliente = await mtoService.GetCliente(licenciaMobile.ClienteId);
    if (cliente is not null)
    {
        NombreCliente = cliente.Nombre;
    }
    else
    {
        await swal.FireAsync("Atención", "El cliente no existe", SweetAlertIcon.Info);
        licenciaMobile.ClienteId = "";
        NombreCliente = "";
    }
    StateHasChanged();
}

private async Task OnShowModalClientes()
{
    await dialogService.OpenAsync<ClientesPop>("Clientes",
          new Dictionary<string, object>()
            {
                { "OnItemSelected", EventCallback.Factory.Create<Cliente>(this, HideModalClientes)}
            },
          new DialogOptions()
          {
              Resizable = true,
              Draggable = true,
              Resize = base.OnResize,
              Drag = base.OnDrag,
              Width = Settings != null ? Settings.Width : "700px",
              Height = Settings != null ? Settings.Height : "512px",
              Left = Settings != null ? Settings.Left : null,
              Top = Settings != null ? Settings.Top : null
          });
}

private async Task HideModalClientes(Cliente args)
{
    licenciaMobile!.ClienteId = args.Id;
    NombreCliente = args.Nombre;

    dialogService.Close(true);
    await InvokeAsync(StateHasChanged);
}
```

## Carga de datos

```csharp
private async Task LoadLicenciaMobile()
{
    if (Modo != 1)
    {
        var repo = await processService.GetLicenciaMobile(Id);

        if (repo is not null)
        {
            licenciaMobile = new LicenciaMobile
            {
                Id = repo.Id,
                ClienteId = repo.ClienteId,
                Compania = repo.Compania,
                ApiUrl = repo.ApiUrl,
                ApiUser = repo.ApiUser,
                ApiClientId = repo.ApiClientId,
                ApiPassword = repo.ApiPassword,
                ApiDataBase = repo.ApiDataBase,
                ApiSchema = repo.ApiSchema,
                ActivarMarcador = repo.ActivarMarcador,
                FechaCreacion = repo.FechaCreacion,
                UsuarioCreacion = repo.UsuarioCreacion,
                FechaModificacion = repo.FechaModificacion,
                UsuarioModificacion = repo.UsuarioModificacion,
            };

            NombreCliente = repo.Cliente != null ? repo.Cliente.Nombre : "";
        }
    }
}
```

`OnInitializedAsync` sigue el mismo esqueleto que `ContratosDetalle`:
`GetLoadTasks()` con `Task.WhenAll`, `soloLectura = Modo == 3`,
`soloLecturaAdd = Modo != 1`, y en Modo 1 inicializa un `LicenciaMobile`
vacío con `Id = 0`, strings vacíos, `ActivarMarcador = false`,
`FechaCreacion/FechaModificacion = DateTime.Now`,
`UsuarioCreacion/UsuarioModificacion = loginState.User.consultor!.Id.ToString()`.

No hay dropdowns de catálogo que cargar (a diferencia de `ContratosDetalle`
o `LicenciasDetalle`), así que `GetLoadTasks()` solo incluye
`LoadLicenciaMobile()`.

## Guardar / Eliminar

```csharp
private async Task Guardar(LicenciaMobile arg)
{
    InProcess = true;
    try
    {
        if (await Validaciones())
        {
            List<LicenciaMobile> listAdd = new List<LicenciaMobile> { arg };

            EventResponse respuesta;
            if (Modo == 3)
            {
                SweetAlertResult result = await swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Atención",
                    Text = $"¿Está seguro(a) que desea eliminar la licencia móvil actual?",
                    Icon = SweetAlertIcon.Error,
                    ShowCancelButton = true,
                    ConfirmButtonText = "Sí",
                    CancelButtonText = "No"
                });

                if (result.IsConfirmed)
                {
                    respuesta = await genericService.Delete("LicenciaMobile", arg.Id.ToString());
                }
                else
                {
                    NavigationManager.NavigateTo(UrlRetorno);
                    return;
                }
            }
            else
            {
                respuesta = await genericService.Post("LicenciaMobile", listAdd);
            }

            if (respuesta.Id == "0")
            {
                NavigationManager.NavigateTo(UrlRetorno);
            }
            else
            {
                await swal.FireAsync("Error", respuesta.Descripcion, SweetAlertIcon.Error);
            }
        }
    }
    catch (Exception e)
    {
        await swal.FireAsync("Error", "Ocurrió un error al ejecutar el proceso. Detalle de Error: " + e.Message, SweetAlertIcon.Error);
    }
    finally
    {
        InProcess = false;
    }
}
```

`genericService.Post`/`Delete` usan `"LicenciaMobile"` como nombre de
entidad, igual que `GetLicenciaMobile` ya usa el método `/LicenciaMobile/`
en `ProcesoService` — mismo nombre de recurso de la API.

## Validaciones

```csharp
private async Task<bool> Validaciones()
{
    if (Modo != 3)
    {
        if (String.IsNullOrEmpty(licenciaMobile!.ClienteId))
        {
            await swal.FireAsync("Atención", "Debe indicar el cliente de esta licencia móvil.", SweetAlertIcon.Info);
            return false;
        }

        if (String.IsNullOrEmpty(licenciaMobile.Compania))
        {
            await swal.FireAsync("Atención", "Debe indicar la compañía.", SweetAlertIcon.Info);
            return false;
        }

        if (String.IsNullOrEmpty(licenciaMobile.ApiUrl))
        {
            await swal.FireAsync("Atención", "Debe indicar la URL de API.", SweetAlertIcon.Info);
            return false;
        }

        if (String.IsNullOrEmpty(licenciaMobile.ApiUser))
        {
            await swal.FireAsync("Atención", "Debe indicar el usuario de API.", SweetAlertIcon.Info);
            return false;
        }

        if (String.IsNullOrEmpty(licenciaMobile.ApiPassword))
        {
            await swal.FireAsync("Atención", "Debe indicar el password de API.", SweetAlertIcon.Info);
            return false;
        }

        if (String.IsNullOrEmpty(licenciaMobile.ApiClientId))
        {
            await swal.FireAsync("Atención", "Debe indicar el Client Id de API.", SweetAlertIcon.Info);
            return false;
        }

        if (String.IsNullOrEmpty(licenciaMobile.ApiDataBase))
        {
            await swal.FireAsync("Atención", "Debe indicar la base de datos de API.", SweetAlertIcon.Info);
            return false;
        }

        if (String.IsNullOrEmpty(licenciaMobile.ApiSchema))
        {
            await swal.FireAsync("Atención", "Debe indicar el schema de API.", SweetAlertIcon.Info);
            return false;
        }
    }

    return true;
}
```

Todos los campos (Cliente, Compañía, ApiUrl, ApiUser, ApiPassword,
ApiClientId, ApiDataBase, ApiSchema) son obligatorios — confirmado con el
usuario. `ActivarMarcador` no requiere validación (booleano, siempre tiene
un valor).

## Manejo de errores

Igual que el resto de páginas Detalle: `SpinnerControl` mientras
`licenciaMobile == null`, `try/catch` en `OnInitializedAsync`/`Guardar` con
`swal.FireAsync("Error", ...)`.

## Fuera de alcance

- No se modifica `LicenciasMobileList.razor` (ya tiene las llamadas de
  navegación correctas).
- No se agrega paginación de catálogo/dropdown alguno — `LicenciaMobile` no
  tiene campos de tipo catálogo.
- No se resuelve la discrepancia de tipo `Id` (`long` en el modelo vs.
  `{Id:int}` en la ruta / `GetLicenciaMobile(int id)` en el servicio): se
  sigue la convención ya establecida en el resto del proyecto (`int`), igual
  que hacen hoy `ContratosDetalle` y `LicenciasDetalle`.
- No se agrega máscara/mostrar-ocultar al campo de password — se deja como
  texto plano, según lo confirmado.

## Testing

Sin proyecto de pruebas automatizadas en esta solución (Blazor Server, sin
`*.Tests.csproj`). Verificación:

- `dotnet build` para confirmar que el componente compila sin errores.
- No es posible probar el flujo real contra la API en este entorno (no hay
  backend corriendo), así que no se hará verificación manual en navegador
  para esta tarea.
