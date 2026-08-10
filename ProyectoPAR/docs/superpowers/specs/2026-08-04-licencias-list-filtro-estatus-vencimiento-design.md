# Filtro de estatus de vencimiento (dropdown) en LicenciasList

## Contexto

`Pages/Procesos/LicenciasList.razor` ya tiene un campo "Días para vencimiento"
(`DiasVencimiento`, mínimo 7, default 15) que colorea el fondo de cada fila del
grid según su estado de vencimiento (rojo = vencida, naranja = próxima a
vencer, sin color = OK), sin ocultar ninguna fila
(`docs/superpowers/specs/2026-08-04-licencias-list-filtro-vencimiento-design.md`).

Se agrega ahora un segundo control — un dropdown de estatus — que sí filtra
(oculta) filas del grid según ese mismo cálculo de vencimiento, reutilizando
exactamente los mismos parámetros ya establecidos (`DiasVencimiento`,
`NoExpira`, `FechaExpiracion`).

## UI

- En la misma `RadzenStack` horizontal donde vive el control de días (dentro
  de `card-body`, sobre el grid), se agrega:
  - Un `RadzenLabel` con texto "Estatus".
  - Un `RadzenDropDown<string>` enlazado a la nueva propiedad
    `EstadoVencimientoFiltro` (`@bind-Value`), con:
    - `Data`: una lista fija de 3 opciones (`Value`/`Text`):
      `("OK", "OK")`, `("VENCIDA", "VENCIDAS")`,
      `("PRONTO_A_VENCER", "PRONTO A VENCER")`.
    - `AllowClear="true"`, `Placeholder="Todos los estatus"`.
- Valor inicial: sin selección (`null`) — el grid muestra todas las
  licencias hasta que el usuario elija un estatus. Limpiar el dropdown
  (botón de `AllowClear`) vuelve a mostrar todas.

## Lógica de clasificación (compartida con el coloreo existente)

Se extrae la lógica de vencimiento de `RowRender` a un método reutilizable:

```csharp
string GetEstadoVencimiento(LicenciaExt licencia)
{
    if (licencia.NoExpira || !DiasVencimiento.HasValue)
    {
        return "OK";
    }

    var diasRestantes = (licencia.FechaExpiracion.Date - DateTime.Today).Days;

    if (diasRestantes < 0)
    {
        return "VENCIDA";
    }

    if (diasRestantes <= DiasVencimiento.Value)
    {
        return "PRONTO_A_VENCER";
    }

    return "OK";
}
```

`RowRender` se reescribe para usar este método (mismo comportamiento que hoy,
sin cambios visuales):

```csharp
void RowRender(RowRenderEventArgs<LicenciaExt> args)
{
    if (args.Data.EstadoId == "I")
    {
        args.Attributes.Add("class", "radzen-grid-row-background-color-red-light rz-data-row");
        return;
    }

    switch (GetEstadoVencimiento(args.Data))
    {
        case "VENCIDA":
            args.Attributes.Add("class", "radzen-grid-row-background-color-red-light rz-data-row");
            break;
        case "PRONTO_A_VENCER":
            args.Attributes.Add("class", "radzen-grid-row-background-color-orange-light rz-data-row");
            break;
    }
}
```

Importante: `GetEstadoVencimiento` **no** considera `EstadoId == "I"` — esa
regla sigue siendo exclusiva del color de fila (prioridad visual sobre
inactivas). Para el filtro de estatus, las licencias inactivas se clasifican
igual que cualquier otra, únicamente por fecha de vencimiento. Esto es
intencional: el usuario confirmó que las inactivas deben clasificarse "según
su fecha de vencimiento" para efectos de este filtro.

## Filtrado real (oculta filas)

- Nueva propiedad calculada:

```csharp
IEnumerable<LicenciaExt> FilteredDataList =>
    string.IsNullOrEmpty(EstadoVencimientoFiltro)
        ? dataList
        : dataList?.Where(l => GetEstadoVencimiento(l) == EstadoVencimientoFiltro);
```

- El grid cambia su binding de `Data="@dataList"` a `Data="@FilteredDataList"`.
- No se requiere un handler `Change` explícito en el dropdown: el
  `@bind-Value` estándar de Blazor ya dispara un re-render del componente
  padre al cambiar la selección, lo cual reevalúa `FilteredDataList` en el
  siguiente render (mismo mecanismo por el que el campo de días ya
  recalculaba los colores).
- Cambiar el dropdown de estatus reinicia la paginación del grid a la
  primera página (comportamiento esperado y deseado para un filtro que
  oculta filas — a diferencia del campo de días, que solo recolorea y por
  eso evita tocar el estado del grid).

## Exportar (XLS/CSV)

`Export()` cambia de `dataList.AsQueryable()` a `FilteredDataList.AsQueryable()`
— la exportación respeta el filtro de estatus activo, igual que ya respeta
los filtros de columna del grid vía `grid.Query.Filter`.

## Fuera de alcance

- No se agregan más opciones al dropdown (solo las 3 solicitadas).
- No se persiste la selección entre sesiones ni se agrega a la URL.
- No se modifica el coloreo de filas existente (rojo inactivo, rojo vencida,
  naranja próxima a vencer) — solo se refactoriza su implementación interna
  para reutilizar `GetEstadoVencimiento`.

## Testing

Sin proyecto de pruebas automatizadas en esta solución (Blazor Server, sin
`*.Tests.csproj`). Verificación manual en navegador:

- Coloreo de filas sin cambios visibles tras el refactor de `RowRender`
  (vencida roja, próxima a vencer naranja, inactiva roja, OK sin color).
- Seleccionar "VENCIDAS" en el dropdown → el grid muestra solo licencias
  vencidas (incluidas las inactivas cuya fecha ya venció).
- Seleccionar "PRONTO A VENCER" → solo licencias dentro del umbral de días.
- Seleccionar "OK" → solo licencias fuera del umbral (y las `NoExpira`).
- Limpiar el dropdown (`AllowClear`) → vuelven a verse todas las licencias.
- Cambiar el campo de días con un estatus ya seleccionado → el filtrado se
  recalcula con el nuevo umbral sin necesidad de volver a seleccionar el
  dropdown.
- Exportar a XLS/CSV con un estatus filtrado → el archivo contiene solo las
  licencias visibles bajo ese filtro.
