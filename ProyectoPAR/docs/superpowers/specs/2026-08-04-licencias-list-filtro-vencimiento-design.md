# Filtro de días para vencimiento en LicenciasList

## Contexto

`Pages/Procesos/LicenciasList.razor` muestra el grid de licencias. Se requiere
agregar un control numérico sobre el grid que permita indicar cuántos días de
anticipación se consideran "próximos a vencer", coloreando el fondo de cada
fila según el estado de vencimiento calculado a partir de `FechaExpiracion`.

Este control no oculta ni filtra registros: únicamente afecta el color de
fondo de las filas ya visibles.

## UI

- Se agrega una `RadzenStack` horizontal (`Orientation="Horizontal"`,
  `AlignItems="Center"`, `Gap="0.5rem"`) dentro de `card-body`, ubicada entre
  el bloque `@if (InProcess) { <SpinnerControl/> }` y el `RadzenDataGrid`.
- Contiene:
  - Un `RadzenLabel` (o `<label>`) con texto "Días para vencimiento".
  - Un `RadzenNumeric<int?>` enlazado a la nueva propiedad
    `DiasVencimiento` (`@bind-Value`), con `Min="0"`, y evento `Change` que
    dispara la recarga del grid (`await grid.Reload();`) para forzar el
    recálculo inmediato de `RowRender`.
- Valor inicial de `DiasVencimiento`: `15`.

## Lógica de cálculo (código, no diseño gráfico)

Se extiende el método existente `RowRender(RowRenderEventArgs<LicenciaExt> args)`:

1. Si `args.Data.EstadoId == "I"` → se aplica el color rojo de inactivo ya
   existente (`radzen-grid-row-background-color-red-light`) y se detiene la
   evaluación (prioridad máxima, sin cambios respecto al comportamiento
   actual).
2. Si `args.Data.NoExpira == true` → no se aplica ningún color de
   vencimiento (la licencia nunca vence).
3. En cualquier otro caso, se calcula:

   ```
   diasRestantes = (args.Data.FechaExpiracion.Date - DateTime.Today).Days
   ```

   - `diasRestantes < 0` → **vencida**: se aplica la clase
     `radzen-grid-row-background-color-red-light` (tono rojo, reutilizando
     la clase ya definida en el `<style>` del componente).
   - `0 <= diasRestantes <= DiasVencimiento` (cuando `DiasVencimiento` tiene
     valor) → **próxima a vencer**: se aplica una clase nueva
     `radzen-grid-row-background-color-orange-light` (tono anaranjado),
     agregada al bloque `<style>` existente del componente.
   - `diasRestantes > DiasVencimiento`, o `DiasVencimiento` es `null` → sin
     color (estado OK).

## CSS

Se agrega al `<style>` ya existente en `LicenciasList.razor`:

```css
.radzen-grid-row-background-color-orange-light > td {
    background-color: #FF9F40 !important;
}
```

(Tono anaranjado; no colisiona con las clases rojo/amarillo/verde ya
definidas en ese mismo bloque.)

## Fuera de alcance

- No se agregan otros campos de filtro adicionales (el pedido original
  menciona "opciones" en plural, pero solo se especificó este campo).
- No se modifica el comportamiento de exportación (XLS/CSV), que sigue
  operando sobre `dataList`/`grid.Query` tal como está.
- No se persiste el valor de `DiasVencimiento` entre sesiones ni se agrega a
  la URL/querystring.

## Testing

- Verificación manual en navegador (dev server):
  - Licencia con `FechaExpiracion` pasada y `NoExpira=false` → fila roja.
  - Licencia con `FechaExpiracion` dentro del umbral y `NoExpira=false` →
    fila naranja.
  - Licencia con `FechaExpiracion` fuera del umbral → sin color.
  - Licencia con `NoExpira=true` → sin color, sin importar la fecha.
  - Licencia con `EstadoId == "I"` → se mantiene roja de inactivo aunque la
    fecha esté fuera de umbral.
  - Cambiar el valor del campo de días recalcula los colores sin recargar
    la página.
