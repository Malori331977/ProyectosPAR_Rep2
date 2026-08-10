# Filtro de estatus de vencimiento (dropdown) en LicenciasList Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add a "Estatus" dropdown above the licencias grid in `Pages/Procesos/LicenciasList.razor` that filters (hides) rows by vencimiento status (OK / VENCIDAS / PRONTO A VENCER), reusing the exact same threshold (`DiasVencimiento`) and classification rules already driving the existing row-coloring feature.

**Architecture:** Single-file Blazor component change, in two steps. First, extract the vencimiento classification already inline in `RowRender` into a reusable `GetEstadoVencimiento(LicenciaExt)` method (pure refactor, same row-coloring behavior). Second, add the dropdown control, a computed `FilteredDataList` property that filters `dataList` through that same method, rebind the grid's `Data` to it, and make `Export()` use it too so exports respect the active filter.

**Tech Stack:** Blazor Server (.razor), Radzen Blazor Components (`RadzenDropDown`, `RadzenDataGrid`, `RowRender`).

## Global Constraints

- Spec: `docs/superpowers/specs/2026-08-04-licencias-list-filtro-estatus-vencimiento-design.md`
- Related prior spec (existing coloring feature, being reused/refactored, not changed in behavior): `docs/superpowers/specs/2026-08-04-licencias-list-filtro-vencimiento-design.md`
- The dropdown has exactly 3 options: `("OK", "OK")`, `("VENCIDA", "VENCIDAS")`, `("PRONTO_A_VENCER", "PRONTO A VENCER")` (internal value, display text).
- `AllowClear="true"`, `Placeholder="Todos los estatus"`. No selection (`null`/empty) means show all rows — no filtering.
- `GetEstadoVencimiento(LicenciaExt licencia)` classification (reused from the existing spec, now extracted into a method): if `licencia.NoExpira` or `DiasVencimiento` has no value → `"OK"`. Else `diasRestantes = (licencia.FechaExpiracion.Date - DateTime.Today).Days`. `diasRestantes < 0` → `"VENCIDA"`. `diasRestantes <= DiasVencimiento.Value` → `"PRONTO_A_VENCER"`. Otherwise → `"OK"`.
- `GetEstadoVencimiento` does NOT consider `EstadoId == "I"` (inactive) — that stays exclusively a `RowRender` coloring-priority rule. For the status filter, inactive licenses are classified purely by date, same as any other license.
- `RowRender`'s existing visible behavior (red for inactive, red for vencida, orange for próxima a vencer, no color for OK) must not change after the refactor.
- `Export()` must export `FilteredDataList`, not the unfiltered `dataList` — exports respect the active status filter (and the grid's existing column filters, unchanged).
- No automated test project exists in this solution (Blazor Server app, no `*.Tests.csproj`). Verification is manual, in a browser.

---

### Task 1: Extract `GetEstadoVencimiento` and refactor `RowRender` (no behavior change)

**Files:**
- Modify: `Pages/Procesos/LicenciasList.razor:310-333` (current `RowRender` method)

**Interfaces:**
- Produces: `string GetEstadoVencimiento(LicenciaExt licencia)`, returning `"OK"`, `"VENCIDA"`, or `"PRONTO_A_VENCER"`. Used by Task 2's `FilteredDataList` property.
- Consumes: existing `int? DiasVencimiento` field (already in the file).

- [ ] **Step 1: Replace `RowRender` with the extracted-method version**

In `Pages/Procesos/LicenciasList.razor`, replace the existing `RowRender` method:

```csharp
    void RowRender(RowRenderEventArgs<LicenciaExt> args)
    {
        if (args.Data.EstadoId == "I")
        {
            args.Attributes.Add("class", "radzen-grid-row-background-color-red-light rz-data-row");
            return;
        }

        if (args.Data.NoExpira || !DiasVencimiento.HasValue)
        {
            return;
        }

        var diasRestantes = (args.Data.FechaExpiracion.Date - DateTime.Today).Days;

        if (diasRestantes < 0)
        {
            args.Attributes.Add("class", "radzen-grid-row-background-color-red-light rz-data-row");
        }
        else if (diasRestantes <= DiasVencimiento.Value)
        {
            args.Attributes.Add("class", "radzen-grid-row-background-color-orange-light rz-data-row");
        }
    }
```

with:

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

- [ ] **Step 2: Verify no behavior change**

Run:

```bash
dotnet build
```

Confirm 0 errors. Then, with the app running (`dotnet run`), navigate to `/licenciaslist` and confirm row coloring is unchanged from before this refactor: licencias with `EstadoId == "I"` are red, expired licencias (past `FechaExpiracion`, not `NoExpira`) are red, licencias within the `DiasVencimiento` threshold are orange, everything else has no background color. This should look identical to the grid's behavior before this task — only the internal code structure changed.

- [ ] **Step 3: Commit**

```bash
git add Pages/Procesos/LicenciasList.razor
git commit -m "refactor: extract GetEstadoVencimiento from RowRender"
```

---

### Task 2: Add the "Estatus" dropdown filter

**Files:**
- Modify: `Pages/Procesos/LicenciasList.razor:14-17` (control markup, add dropdown next to the days field)
- Modify: `Pages/Procesos/LicenciasList.razor:19-42` (`RadzenDataGrid` — change `Data` binding)
- Modify: `Pages/Procesos/LicenciasList.razor:95-104` (`@code` fields — add dropdown data source and backing field)
- Modify: `Pages/Procesos/LicenciasList.razor:280-293` (`Export` method — use filtered data)

**Interfaces:**
- Consumes: `string GetEstadoVencimiento(LicenciaExt licencia)` from Task 1.
- Consumes: existing `IEnumerable<LicenciaExt> dataList` field.
- Produces: `IEnumerable<LicenciaExt> FilteredDataList` property, consumed by the grid's `Data` binding and by `Export()`.

- [ ] **Step 1: Add the dropdown's backing field, option list, and filtered-data property**

In `Pages/Procesos/LicenciasList.razor`, inside the `@code` block, right after the existing `int? DiasVencimiento = 15;` field and its `OnDiasVencimientoChanged` method (i.e. after the current lines ending the method — the block that starts `int? DiasVencimiento = 15;` and ends the `OnDiasVencimientoChanged` method's closing `}`), add:

```csharp
    string? EstadoVencimientoFiltro;

    private class EstadoVencimientoOption
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    private static readonly List<EstadoVencimientoOption> listEstadoVencimiento = new()
    {
        new EstadoVencimientoOption { Value = "OK", Text = "OK" },
        new EstadoVencimientoOption { Value = "VENCIDA", Text = "VENCIDAS" },
        new EstadoVencimientoOption { Value = "PRONTO_A_VENCER", Text = "PRONTO A VENCER" },
    };

    IEnumerable<LicenciaExt> FilteredDataList =>
        string.IsNullOrEmpty(EstadoVencimientoFiltro)
            ? dataList
            : dataList?.Where(l => GetEstadoVencimiento(l) == EstadoVencimientoFiltro);
```

- [ ] **Step 2: Add the dropdown markup**

In `Pages/Procesos/LicenciasList.razor`, inside the existing horizontal `RadzenStack` that holds the "Días para vencimiento" control:

```razor
            <RadzenStack Orientation="Radzen.Orientation.Horizontal" AlignItems="AlignItems.Center" Gap="0.5rem">
                <RadzenLabel Text="Días para vencimiento" Component="diasVencimiento" />
                <RadzenNumeric TValue="int?" Name="diasVencimiento" @bind-Value="DiasVencimiento" Min="7" Style="width: 100px" Change="@OnDiasVencimientoChanged" />
            </RadzenStack>
```

replace it with (adding the dropdown as two more children of the same stack):

```razor
            <RadzenStack Orientation="Radzen.Orientation.Horizontal" AlignItems="AlignItems.Center" Gap="0.5rem">
                <RadzenLabel Text="Días para vencimiento" Component="diasVencimiento" />
                <RadzenNumeric TValue="int?" Name="diasVencimiento" @bind-Value="DiasVencimiento" Min="7" Style="width: 100px" Change="@OnDiasVencimientoChanged" />
                <RadzenLabel Text="Estatus" Component="estadoVencimiento" />
                <RadzenDropDown TValue="string" Name="estadoVencimiento" @bind-Value="EstadoVencimientoFiltro"
                                Data="@listEstadoVencimiento" ValueProperty="Value" TextProperty="Text"
                                AllowClear="true" Placeholder="Todos los estatus" Style="width: 200px" />
            </RadzenStack>
```

- [ ] **Step 3: Rebind the grid's `Data` to the filtered list**

In `Pages/Procesos/LicenciasList.razor`, in the `<RadzenDataGrid ...>` tag, find:

```razor
                            Data="@dataList"
```

and replace with:

```razor
                            Data="@FilteredDataList"
```

- [ ] **Step 4: Make `Export()` use the filtered data**

In `Pages/Procesos/LicenciasList.razor`, in the `Export` method, find:

```csharp
    public async Task Export(string type)
    {
        var queryableList = dataList.AsQueryable();
```

and replace with:

```csharp
    public async Task Export(string type)
    {
        var queryableList = FilteredDataList.AsQueryable();
```

- [ ] **Step 5: Verify manually in the browser**

Run:

```bash
dotnet build
```

Confirm 0 errors. Then, with the app running (`dotnet run`), navigate to `/licenciaslist` and check:

- The "Estatus" dropdown appears next to "Días para vencimiento", starts empty (placeholder "Todos los estatus"), and the grid shows all licencias.
- Selecting "VENCIDAS" shows only licencias whose vencimiento status is vencida (including inactive ones whose `FechaExpiracion` has passed — they still show, still colored red for being inactive).
- Selecting "PRONTO A VENCER" shows only licencias within the `DiasVencimiento` threshold.
- Selecting "OK" shows only licencias outside the threshold and any with `NoExpira` on.
- Clicking the dropdown's clear button (AllowClear) shows all licencias again.
- With a status selected, changing the "Días para vencimiento" value updates which rows are visible without needing to reselect the dropdown.
- With a status selected, clicking "Exportar a XLS" or "Exportar a CSV" produces a file containing only the currently filtered licencias.

- [ ] **Step 6: Commit**

```bash
git add Pages/Procesos/LicenciasList.razor
git commit -m "feat: add estatus de vencimiento dropdown filter to LicenciasList"
```
