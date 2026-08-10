# Componente LicenciaMobileDetalle Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Create `Pages/Procesos/LicenciaMobileDetalle.razor`, the missing detail/capture form for `LicenciaMobile` records, reachable from the already-existing navigation in `LicenciasMobileList.razor` (`./licenciasmobiledetalle/{id}/{modo}`).

**Architecture:** Single new Blazor Server component, one file (markup + `@code`), mirroring the existing `Pages/Procesos/ContratosDetalle.razor` pattern (flat single-section `RadzenTemplateForm`, direct `ClienteId`/`Cliente` lookup via `ClientesPop`, no `*Ext` wrapper class needed since `LicenciaMobile` already carries `ClienteId` directly).

**Tech Stack:** Blazor Server (.razor), Radzen Blazor Components (`RadzenTemplateForm`, `RadzenTextBox`, `RadzenSwitch`, `RadzenNumeric`, `RadzenButton`), SweetAlert2 (`swal.FireAsync`), `IProcesoService.GetLicenciaMobile`, `IMantenimientoService.GetCliente`, `IGenericService.Post`/`Delete`.

## Global Constraints

- Spec: `docs/superpowers/specs/2026-08-10-licencia-mobile-detalle-design.md`
- Route: `@page "/licenciasmobiledetalle/{Id:int}/{Modo:int}"` — must match the navigation already present in `Pages/Procesos/LicenciasMobileList.razor` (`OnClickAdd`, `OnSelect`, `OnDelete`).
- `UrlRetorno = "./licenciasmobilelist"`, `Titulo = "Licencia Móvil"`.
- `@inherits DialogSettingLayout` (required for `ClientesPop` dialog `Resize`/`Drag`/`Settings`).
- Obligatory fields on save (`Modo != 3`): Cliente (`ClienteId`), `Compania`, `ApiUrl`, `ApiUser`, `ApiPassword`, `ApiClientId`, `ApiDataBase`, `ApiSchema`. `ActivarMarcador` is a boolean switch, no validation needed, defaults to `false` on create.
- `ApiPassword` renders as a plain `RadzenTextBox` (not masked) — confirmed with the user.
- `genericService.Post`/`Delete` use `"LicenciaMobile"` as the entity name (matches the `/LicenciaMobile/` route already used by `ProcesoService.GetLicenciaMobile`).
- Follow the established `int` route-parameter convention for `Id`, even though the model's `Id` is `long` — same convention already used by `ContratosDetalle`/`LicenciasDetalle` and by `IProcesoService.GetLicenciaMobile(int id)`.
- No automated test project exists in this solution (Blazor Server app, no `*.Tests.csproj`). Verification is `dotnet build` plus a documented manual-testing checklist (full browser verification isn't possible in this environment — no backend running).

---

### Task 1: Create `LicenciaMobileDetalle.razor`

**Files:**
- Create: `Pages/Procesos/LicenciaMobileDetalle.razor`

**Interfaces:**
- Consumes: `IProcesoService.GetLicenciaMobile(int id)` (existing, `Data/Interfaces/IProcesoService.cs:285-286`), `IMantenimientoService.GetCliente(string id)` (existing), `IGenericService.Post(string, IEnumerable<T>)` / `.Delete(string, string)` (existing, used identically by `ContratosDetalle.razor`), `ProyectoParLibs.Models.Procesos.LicenciaMobile` (existing model, fields: `Id` (long), `ClienteId`, `Compania`, `ApiUrl`, `ApiUser`, `ApiClientId`, `ApiPassword`, `ApiDataBase`, `ApiSchema`, `ActivarMarcador`, `FechaCreacion`, `FechaModificacion`, `UsuarioCreacion`, `UsuarioModificacion`, `Cliente`), `ProyectoParLibs.Models.Mantenimiento.Cliente` (`Id`, `Nombre`), `ClientesPop` popup component (existing, `Pages/Popups/ClientesPop.razor`, exposes `EventCallback<Cliente> OnItemSelected`).
- Produces: nothing consumed by other tasks — this is the final deliverable, and `LicenciasMobileList.razor`'s existing navigation already targets this route.

- [ ] **Step 1: Write the component file**

Create `Pages/Procesos/LicenciaMobileDetalle.razor` with this exact content:

```razor
@page "/licenciasmobiledetalle/{Id:int}/{Modo:int}"
@inherits DialogSettingLayout

@if (licenciaMobile == null)
{
    <SpinnerControl></SpinnerControl>
}
else
{
    <div class="card">
        <PageReturn Titulo="@Titulo" UrlRetorno="@UrlRetorno" />

        <div class="card-body">
            <RadzenTemplateForm Data="@licenciaMobile" Submit="@((LicenciaMobile args) => { Guardar(args); })">
                <RadzenRow Gap="0.3rem" class="rz-p-0 rz-p-lg-2">
                    <RadzenColumn Size="12" SizeMD="12">
                        <RadzenStack>
                            <RadzenFieldset Text="@Titulo">
                                <RadzenStack Gap="0.3rem">
                                    <RadzenRow AlignItems="AlignItems.Center">
                                        @if (Modo != 1)
                                        {
                                            <RadzenColumn Size="12" SizeMD="2">
                                                <RadzenLabel Text="Id" Component="id" />
                                                <RadzenNumeric Style="width: 100%;" @bind-Value=@licenciaMobile.Id Name="id" Disabled="true" />
                                            </RadzenColumn>
                                        }
                                        <RadzenColumn Size="12" SizeMD="@sizeCli">
                                            <RadzenLabel Text="Cliente" Component="ClienteId" />
                                            <RadzenRow AlignItems="AlignItems.Center">
                                                <RadzenColumn Size="12" SizeMD="1">
                                                    <RadzenButton ButtonStyle="ButtonStyle.Info" Icon="search" class="rounded-circle rz-my-1 rz-ms-1" Disabled="@soloLecturaAdd" Size="Radzen.ButtonSize.Medium" Click=@OnShowModalClientes></RadzenButton>
                                                </RadzenColumn>
                                                <RadzenColumn Size="12" SizeMD="2">
                                                    <RadzenTextBox Style="width: 100%;" Name="ClienteId" @bind-Value="licenciaMobile.ClienteId" Disabled="@soloLecturaAdd"
                                                    @onfocusout="BuscarCliente" />
                                                </RadzenColumn>
                                                <RadzenColumn Size="12" SizeMD="9">
                                                    <RadzenTextBox Style="width: 100%;" Disabled="true" @bind-Value="NombreCliente" />
                                                </RadzenColumn>
                                            </RadzenRow>
                                        </RadzenColumn>
                                    </RadzenRow>

                                    <RadzenRow AlignItems="AlignItems.Center">
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="Compañía" Component="compania" />
                                            <RadzenTextBox Style="width: 100%;" Name="compania" @bind-Value="licenciaMobile.Compania" Disabled="@soloLectura" />
                                        </RadzenColumn>
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="URL de API" Component="apiurl" />
                                            <RadzenTextBox Style="width: 100%;" Name="apiurl" @bind-Value="licenciaMobile.ApiUrl" Disabled="@soloLectura" />
                                        </RadzenColumn>
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="Usuario de API" Component="apiuser" />
                                            <RadzenTextBox Style="width: 100%;" Name="apiuser" @bind-Value="licenciaMobile.ApiUser" Disabled="@soloLectura" />
                                        </RadzenColumn>
                                    </RadzenRow>

                                    <RadzenRow AlignItems="AlignItems.Center">
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="Password de API" Component="apipassword" />
                                            <RadzenTextBox Style="width: 100%;" Name="apipassword" @bind-Value="licenciaMobile.ApiPassword" Disabled="@soloLectura" />
                                        </RadzenColumn>
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="Client Id de API" Component="apiclientid" />
                                            <RadzenTextBox Style="width: 100%;" Name="apiclientid" @bind-Value="licenciaMobile.ApiClientId" Disabled="@soloLectura" />
                                        </RadzenColumn>
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="Base de Datos de API" Component="apidatabase" />
                                            <RadzenTextBox Style="width: 100%;" Name="apidatabase" @bind-Value="licenciaMobile.ApiDataBase" Disabled="@soloLectura" />
                                        </RadzenColumn>
                                    </RadzenRow>

                                    <RadzenRow AlignItems="AlignItems.Center">
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="Schema de API" Component="apischema" />
                                            <RadzenTextBox Style="width: 100%;" Name="apischema" @bind-Value="licenciaMobile.ApiSchema" Disabled="@soloLectura" />
                                        </RadzenColumn>
                                        <RadzenColumn Size="12" SizeMD="4">
                                            <RadzenLabel Text="Activar Marcador" Component="activarmarcador" />
                                            <RadzenStack Orientation="Radzen.Orientation.Horizontal" AlignItems="AlignItems.Center">
                                                <RadzenSwitch Placeholder="Activar Marcador" Name="activarmarcador"
                                                @bind-Value="licenciaMobile.ActivarMarcador" Disabled="@soloLectura"
                                                InputAttributes="@(new Dictionary<string, object>() { { "aria-label", "Switch value" } })" />
                                            </RadzenStack>
                                        </RadzenColumn>
                                    </RadzenRow>
                                </RadzenStack>
                            </RadzenFieldset>
                        </RadzenStack>
                    </RadzenColumn>
                </RadzenRow>
                <RadzenStack Orientation="Radzen.Orientation.Horizontal" JustifyContent="JustifyContent.Center" Gap="0.5rem" class="rz-mt-8 rz-mb-4">
                    <RadzenButton class="rz-border-radius-6" ButtonType="Radzen.ButtonType.Submit" Size="Radzen.ButtonSize.Large" Icon="save" Text="Guardar" />
                    <RadzenButton class="rz-border-radius-6" ButtonStyle="Radzen.ButtonStyle.Success" Variant="Variant.Flat" Size="Radzen.ButtonSize.Large" Icon="cancel" Text="Cancelar" Click="@Cancelar" />
                </RadzenStack>
            </RadzenTemplateForm>
        </div>
    </div>
}

@code {
    [Parameter] public int Id { get; set; }
    [Parameter] public int Modo { get; set; }

    private string Titulo = "Licencia Móvil";
    private string UrlRetorno = "./licenciasmobilelist";
    private bool InProcess = true;
    private bool soloLectura = false;
    private bool soloLecturaAdd = false;
    private LicenciaMobile? licenciaMobile = null;
    private string NombreCliente = "";
    int sizeCli = 12;

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

    private List<Task> GetLoadTasks()
    {
        return new List<Task>
        {
            LoadLicenciaMobile(),
        };
    }

    protected override async Task OnInitializedAsync()
    {
        sizeCli = Modo == 1 ? 12 : 10;
        soloLectura = Modo == 3;
        try
        {
            var tasks = GetLoadTasks();
            await Task.WhenAll(tasks);

            //Modos= 1:Insertar 2:Actualizar 3:Borrar 0:Consulta
            if (Modo != 1)
            {
                soloLecturaAdd = true;
            }
            else
            {
                licenciaMobile = new LicenciaMobile
                {
                    Id = 0,
                    ClienteId = "",
                    Compania = "",
                    ApiUrl = "",
                    ApiUser = "",
                    ApiClientId = "",
                    ApiPassword = "",
                    ApiDataBase = "",
                    ApiSchema = "",
                    ActivarMarcador = false,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = loginState.User.consultor!.Id.ToString(),
                    FechaModificacion = DateTime.Now,
                    UsuarioModificacion = loginState.User.consultor!.Id.ToString(),
                };
            }
        }
        catch (Exception e)
        {
            await swal.FireAsync("Error", e.Message, SweetAlertIcon.Error);
        }

        InProcess = false;
        StateHasChanged();
    }

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

    void Cancelar()
    {
        NavigationManager.NavigateTo(UrlRetorno);
    }
}
```

- [ ] **Step 2: Build to verify it compiles**

Run:

```bash
dotnet build
```

Expected: `0 Errores` (0 Errors). If the build fails with an SDK resolver error unrelated to this file (`MSB4040`, seen previously in this project when running plain `dotnet build`), retry with:

```bash
dotnet build ProyectoPAR.csproj
```

If there are real compile errors referencing `LicenciaMobileDetalle.razor`, fix them (most likely causes: a typo in a property name — cross-check against the `LicenciaMobile` field list in the Global Constraints section — or a missing `Disabled`/`Style` attribute mismatch) and rebuild.

- [ ] **Step 3: Self-check against the spec**

Confirm, by rereading the file just created:

- Route is exactly `/licenciasmobiledetalle/{Id:int}/{Modo:int}`.
- All 8 obligatory fields from Global Constraints are present in the markup AND validated in `Validaciones()`.
- `genericService.Post`/`Delete` both use the literal string `"LicenciaMobile"`.
- `ApiPassword` is a plain `RadzenTextBox`, not `RadzenPassword`.
- Deleting (`Modo == 3`) disables all inputs (`soloLectura`) and shows a confirmation `swal` before calling `genericService.Delete`.

- [ ] **Step 4: Manual verification checklist (for when the backend is available)**

Automated testing isn't available in this solution and the backend API isn't running in this environment, so this step can't be executed now — leave it documented for whoever next runs the app with a live backend:

- Navigate to `/licenciasmobilelist`, click "Agregar" → lands on `/licenciasmobiledetalle/0/1` with all fields empty and enabled.
- Click the search icon next to Cliente → `ClientesPop` opens; selecting a client fills `ClienteId` and the read-only name field.
- Typing a valid `ClienteId` directly and tabbing out (`BuscarCliente`) fills the name field the same way; typing an invalid id shows the "El cliente no existe" alert and clears the field.
- Clicking "Guardar" with any of the 8 obligatory fields empty shows the corresponding "Debe indicar..." alert and does not navigate away.
- Filling all obligatory fields and clicking "Guardar" posts successfully and returns to `/licenciasmobilelist`.
- From the list, clicking a row navigates to `/licenciasmobiledetalle/{id}/2` with all fields populated and editable.
- From the list, clicking the delete icon navigates to `/licenciasmobiledetalle/{id}/3` with all fields disabled; clicking "Guardar" shows the delete confirmation dialog, and confirming deletes the record and returns to the list.

- [ ] **Step 5: Commit**

```bash
git add Pages/Procesos/LicenciaMobileDetalle.razor
git commit -m "feat: add LicenciaMobileDetalle component to capture LicenciaMobile records"
```
