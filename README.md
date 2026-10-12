# vg2c

`vg2c` compiles legacy VG2 `.txt` pipelines into Python projects with editable SQL and HTML assets.

`translate(input_path, out_dir=None)` returns `<root>/<sanitized-source-stem>/main.py`.
The root defaults to the source directory. Existing nonempty projects and batch name
collisions are rejected before generation writes files. Move the entire generated
directory together; the original VG2 file is not needed for execution.

Generated code imports the installed `vg2c.runtime`, uses native Python controls and
one instance-scoped `MacroStore`, and rereads external SQL, HTML and CSS assets at
execution time. By default,
`run()` reads and writes job data under `<project>/output`; `run(workdir=...)`
selects an explicit data root. SQL assets stay under `<project>/sql` and report
shells under `<project>/html`. There is no process-wide working-directory change.
CSV table inputs and backend node aliases are separate from SQL parameter binds.
Direct named macro lookups such as `macros["CSRV"]` return `None` when absent.
Unresolved macro tokens in SQL and templates still raise an error.

CSS is emitted under `<project>/styles` and published on HTML-RUN execution.
Explicit relative CSS assets are resolved from the generated project (or an
already-published stylesheet in the workdir), copied beside the output on render,
or embedded for CSSEMBED. Editing a generated CSS asset does not require recompiling VG2. Deferred reports read CSV data and resolve raw macro
paths/options at layout time. Data and headers are escaped once; authored markup
is preserved. Unsupported active report options fail with source locations.
Legacy browser/delivery/security/chart integration settings produce explicit
`local-html-only` diagnostics; the local renderer does not implement those services.

The core Python editing/reorder APIs retain invocation identities and source
ranges for direct calls. SQL and report definitions exposed through these APIs
are read-only through Python offsets: edit their external assets or regenerate
from VG2. Literal values belong to their individual calls rather than shared
generated globals. Embedded Python must use explicit runtime functions and
`workdir`; the retired `ctx` API and top-level return/yield have diagnostics.

The generated project has been checked in a clean Linux virtual environment using
a built and installed `vg2c` wheel. The offline probe covers a moved generated project,
changed working directory, explicit `run(workdir=...)`, SQLite, crosstab/CTARRAY,
editable SQL/HTML/CSS, linked/embedded styles, Unicode and failure handling.
This is **not** certification of private DataSyncX/Oracle/AED services or the original
ScriptHost engine: those require their target infrastructure and credentials.
The old `MacroState`, `PipelineContext` and runtime HTML preview/rendering engines
and the unused source-embedding pipeline have been retired. Compiler-only operation
metadata and HTML parsing remain because they are necessary to translate VG2 and
preserve editing/source offsets. The optional visual editor and its legacy HTML
preview are not a supported Session 05 release gate; HPC/JMP/JSL remain unsupported.

---


## Offline installed-wheel Linux verification

From a clean Linux checkout with Python 3.12, the repository's Session 05 CI
workflow builds the wheel, installs the wheel into a separate virtual environment
and executes a generated project from a detached directory without a compiler
checkout, VG2 source or ScriptHost at runtime:

```bash
python -m pip install build wheel setuptools pandas==3.0.3 keyring==25.7.0
python -m build --wheel --no-isolation --outdir /tmp/vg2c-dist
python -m venv /tmp/vg2c-wheel-venv
/tmp/vg2c-wheel-venv/bin/python -m pip install pandas==3.0.3
/tmp/vg2c-wheel-venv/bin/python -m pip install --no-deps /tmp/vg2c-dist/*.whl
python tests/deployment/verify_linux_wheel.py \\
    --python /tmp/vg2c-wheel-venv/bin/python \\
    --wheel /tmp/vg2c-dist/*.whl
```

The `--no-deps` wheel install is intentional for this **synthetic offline SQLite**
probe: runtime imports require pandas, while external DataSyncX and production
credentials are absent. For a real deployed job, install all declared project
requirements from an authorized corporate package source and validate its
reader/service integration separately. Do not replace the production dependency
install with this minimal offline test environment.

## Architecture Overview

```mermaid
graph TD
    A[VG2 Source File] --> B[Parse & Classify]
    B --> C[Resolve]
    C --> D[Analyze Dataflow]
    D --> E[Dispatch]
    E --> F[Emit Native Python + SQL/HTML Assets + Metadata]
    F --> G[CompilationResult]
    G --> H[Thin UI API / Serialization]
    H --> I[React Presentation]

    subgraph Utilities
        J[UtilitySpec / @emittable metadata] -. definitions & capabilities .-> F
        J -. direct vg2c.runtime calls .-> F
    end
```

`CompilationResult` and the compiler-stage objects are the authoritative semantic chain. The emitter records utility invocations, editable parameters, stable identities, artifact roles, capabilities, and exact generated-source spans while it generates Python. The UI API serializes those compiler-owned results and handles persistence/workspace security; React renders the returned metadata and sends edit intent back to core APIs rather than reconstructing SQL or dataflow semantics locally.

## Requirements

* Python 3.11, 3.12, or 3.13
* Git
* [`uv`](https://docs.astral.sh/uv/)
* Access to the Intel internal Git network

## Installation

Clone the repository and open PowerShell in the project directory.

Allow Git to access the required Intel internal repositories directly:

```powershell
$env:no_proxy = "mfg-github.mfg.intel.com,tmg-repo.mfg.intel.com"
```

Install `vg2c` and its dependencies into the project virtual environment:

```powershell
py -m uv sync
```

Verify the installation:

```powershell
uv run vg2c --help
```

## Oracle client setup (DataSyncX)

Only the approved full Oracle Client is supported. Set `ORACLE_HOME` to the
client root, not its `bin` directory:

```powershell
$env:ORACLE_HOME = 'C:\Oracle\Product\11.2.0\client_k64'
```

The generated workflow validates `ORACLE_HOME\network\admin` before importing
DataSyncX. That directory must contain `tnsnames.ora` and `sqlnet.ora`. If
either file is absent, copy SQLPathFinder's provided Oracle Net files from
`C:\Oracle\network` into `ORACLE_HOME\network\admin`, then start a new Python
process. No client discovery or fallback configuration is supported.

## CLI Usage

Run the interactive CLI from the project root:

```powershell
uv run vg2c .
```

Specify separate input and output directories:

```powershell
uv run vg2c path\to\inputs path\to\outputs
```

Build a standalone executable with PyInstaller:

```powershell
uv run vg2c --build
```

When the virtual environment is already activated, `uv run` may be omitted:

```powershell
vg2c .
vg2c path\to\inputs path\to\outputs
vg2c --build
```

## Visual editor

Install the optional local-app dependencies and frontend packages:

```powershell
python -m pip install -e ".[ui]"
Set-Location src/vg2c_ui/frontend
npm install
```

For development, run the API and Vite in separate terminals from the repository root:

```powershell
vg2c-ui --data-dir .\data
npm --prefix src/vg2c_ui/frontend run dev
```

For a single local server, build the frontend once, then start `vg2c-ui`:

```powershell
npm --prefix src/vg2c_ui/frontend run build
vg2c-ui .
```

The Vite build writes the packaged frontend to `src/vg2c_ui/static`. The server only accepts source/output paths within the workspace passed to `vg2c-ui`.

Utility names, methods, parameters, annotations, defaults, `Literal` choices, return types, documentation, artifact roles, and editor capabilities come from the actual compiler utility definitions. Ordinary utilities therefore use the generic React parameter editor without utility-specific frontend code. Specialized editors are selected by explicit capabilities such as `structured-sql`.

Edits use a preview/apply flow backed by `vg2c.editing`, with compiler-owned value validation, generated-source spans, syntax validation, revision/hash conflict checks, and atomic persistence. Structured SQL parsing/transformation lives in `vg2c.sql_editor`. Draft and cross-document producer/consumer relationships are projected through `vg2c.dataflow`, including unsaved changes in inactive tabs.

The frontend contracts are generated from the Python transport models. Run:

```powershell
npm --prefix src/vg2c_ui/frontend run generate:contracts
npm --prefix src/vg2c_ui/frontend run test
```

The current API surface uses focused routes for document open/translation, change preview/apply, workspace projection, CSV preview, and structured SQL inspect/actions. There is no generic arbitrary-Python replacement or legacy `/api/commands` compatibility route.

Use **Translate** to regenerate Python from VG2. Use **Open** to reopen an existing generated workflow and retain previously applied visual-editor values when its sidecar still matches the source/output hashes.

## LAN Docker test deployment

The Docker deployment serves the visual editor over one LAN port with anonymous, isolated browser workspaces. Users upload VG2/data files and download generated results; generated workflows are not executed. See [the Docker LAN guide](docs/docker-lan.md) for the Intel dependency-build prerequisites, Docker Compose commands, firewall setup, and workspace lifecycle.
