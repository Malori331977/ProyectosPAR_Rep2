# Filtro de días para vencimiento en LicenciasList Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add a numeric "días para vencimiento" control above the licencias grid in `Pages/Procesos/LicenciasList.razor` that colors each row's background (red = vencida, orange = próxima a vencer, none = OK) based on `FechaExpiracion` vs. today, without hiding any rows.

**Architecture:** Single-file Blazor component change. A new bound field (`DiasVencimiento`, default `15`) feeds the existing `RowRender` callback, which already sets a CSS class per row for the `EstadoId == "I"` (inactive) case; the new logic slots in as a lower-priority branch. Two CSS classes: reuse the existing `radzen-grid-row-background-color-red-light` for "vencida", add a new `radzen-grid-row-background-color-orange-light` for "próxima a vencer".

**Tech Stack:** Blazor Server (.razor), Radzen Blazor Components (`RadzenNumeric`, `RadzenDataGrid`, `RowRender`).

## Global Constraints

- Spec: `docs/superpowers/specs/2026-08-04-licencias-list-filtro-vencimiento-design.md`
- The control only recolors rows already in the grid — it must never hide/filter records.
- `NoExpira == true` rows are never colored by this feature, regardless of `FechaExpiracion`.
- `EstadoId == "I"` (inactive) keeps its current red coloring and takes priority over vencimiento coloring — the two must never both try to set the row's class.
- `diasRestantes = (FechaExpiracion.Date - DateTime.Today).Days`. Thresholds: `< 0` → vencida (red). `0..DiasVencimiento` inclusive → próxima a vencer (orange). `> DiasVencimiento`, or `DiasVencimiento == null` → no color.
- No automated test project exists in this solution (Blazor Server app, no `*.Tests.csproj`). Verification is manual, in a browser, per the project's UI-change convention.

---

### Task 1: Add the "Días para vencimiento" numeric control

**Files:**
- Modify: `Pages/Procesos/LicenciasList.razor:9-13` (new markup block)
- Modify: `Pages/Procesos/LicenciasList.razor:90-98` (`@code` fields)

**Interfaces:**
- Produces: `int? DiasVencimiento` field (default `15`), used by Task 2's `RowRender`.
- Produces: `Task OnDiasVencimientoChanged()` method, which reloads the grid so `RowRender` re-evaluates immediately.

- [ ] **Step 1: Add the `DiasVencimiento` field and change handler in `@code`**

In `Pages/Procesos/LicenciasList.razor`, inside the `@code` block, right after the existing field declarations (after line 97, `IList<LicenciaExt> selectedData;`, and before the blank line preceding `listEstadoLicencia`), add:

```csharp
    int? DiasVencimiento = 15;

    async Task OnDiasVencimientoChanged()
    {
        await grid.Reload();
    }
```

- [ ] **Step 2: Add the control markup above the grid**

In `Pages/Procesos/LicenciasList.razor`, between the closing `}` of the `@if (InProcess) { ... }` block (line 12) and the `<RadzenDataGrid ...>` tag (line 14), insert:

```razor
            <RadzenStack Orientation="Radzen.Orientation.Horizontal" AlignItems="AlignItems.Center" Gap="0.5rem">
                <RadzenLabel Text="Días para vencimiento" Component="diasVencimiento" />
                <RadzenNumeric TValue="int?" Name="diasVencimiento" @bind-Value="DiasVencimiento" Min="0" Style="width: 100px" Change="@OnDiasVencimientoChanged" />
            </RadzenStack>
```

- [ ] **Step 3: Verify the control renders and updates the field**

Run the app:

```bash
dotnet run
```

Navigate to `/licenciaslist` in a browser. Expected:
- A "Días para vencimiento" label and numeric input appear above the grid, inside the card, showing `15` by default.
- Typing a new number and tabbing/clicking away does not throw any errors in the browser console or server console (row coloring won't change yet — that's Task 2).

- [ ] **Step 4: Commit**

```bash
git add Pages/Procesos/LicenciasList.razor
git commit -m "feat: add dias-para-vencimiento control to LicenciasList"
```

---

### Task 2: Color rows by vencimiento status

**Files:**
- Modify: `Pages/Procesos/LicenciasList.razor:294-303` (`RowRender` method)
- Modify: `Pages/Procesos/LicenciasList.razor:306-319` (`<style>` block)

**Interfaces:**
- Consumes: `int? DiasVencimiento` field from Task 1.
- Consumes existing `LicenciaExt` properties: `EstadoId` (string), `NoExpira` (bool), `FechaExpiracion` (DateTime, non-nullable).

- [ ] **Step 1: Add the orange CSS class**

In `Pages/Procesos/LicenciasList.razor`, inside the existing `<style>` block (after the `.radzen-grid-row-background-color-green-light > td { ... }` rule, before the closing `</style>`), add:

```css
    .radzen-grid-row-background-color-orange-light > td {
        background-color: #FF9F40 !important;
    }
```

- [ ] **Step 2: Extend `RowRender` with vencimiento coloring**

Replace the existing `RowRender` method (lines 294-303):

```csharp
    void RowRender(RowRenderEventArgs<LicenciaExt> args)
    {
        switch (args.Data.EstadoId)
        {
            case "I":
                args.Attributes.Add("class", "radzen-grid-row-background-color-red-light rz-data-row");
                break;
        }

    }
```

with:

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

- [ ] **Step 3: Verify coloring manually in the browser**

With the app still running (`dotnet run` from Task 1, or restart it), navigate to `/licenciaslist` and check, using existing data or by temporarily editing a couple of licencias via `/licenciasdetalle` to set known `FechaExpiracion`/`NoExpira`/`EstadoId` values:

- A licencia with `FechaExpiracion` in the past and `NoExpira` off (and `EstadoId` not `"I"`) → row background is red.
- A licencia with `FechaExpiracion` between today and `today + 15 days` (default threshold), `NoExpira` off → row background is orange.
- A licencia with `FechaExpiracion` more than 15 days out → no background color.
- A licencia with `NoExpira` on → no background color, regardless of `FechaExpiracion`.
- A licencia with `EstadoId == "I"` → stays red (inactive color), even if its `FechaExpiracion` would otherwise be orange/none.
- Change the "Días para vencimiento" value (e.g. to `60`) → rows near the new threshold recolor immediately (orange/none) without a page reload.
- Change it to `0` → only already-expired rows are red; no rows are orange (since orange requires `0 <= diasRestantes <= 0`, i.e. only rows expiring exactly today).

- [ ] **Step 4: Commit**

```bash
git add Pages/Procesos/LicenciasList.razor
git commit -m "feat: color LicenciasList rows by vencimiento status"
```
