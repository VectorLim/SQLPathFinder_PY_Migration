# vg2c

`vg2c` compiles legacy VG2 `.txt` pipelines into Python projects with editable SQL and HTML assets.

`translate(input_path, out_dir=None)` returns `<root>/<sanitized-source-stem>/main.py`.
The root defaults to the source directory. Existing nonempty projects and batch name
collisions are rejected before generation writes files. Move the entire generated
directory together; the original VG2 file is not needed for execution.

Generated code imports the installed `vg2c.runtime`, uses native Python controls and
one instance-scoped `MacroStore`, and rereads external SQL, HTML and CSS assets at
execution time. By default,
`run()` takes no parameters and explicitly creates its `JobRuntime` inside the
function. It reads and writes job data under `<project>/output`; edit the local
`workdir` assignment to choose a different data folder. SQL assets stay under
`<project>/sql` and report shells under `<project>/html`. There is no process-wide
working-directory change.
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
changed working directory, parameterless `run()`, SQLite, crosstab/CTARRAY,
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
python -m pip install build wheel setuptools pandas==3.0.3
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

## AED and legacy CSR submissions

`vg2c.runtime.aed_client` owns the client, attribute-update method, and validated
CSR request loader. Generated scripts import `AEDClient` from `vg2c.runtime`.

The compiler recognizes CSR holds from a parsed `RoboCopy.va` destination of
`<<<CSRPATH>>>`, exactly one preceding SQL CSV producer with the original CSR
headers, and enclosing `SIGNAL > 0` / `DCSR = Y` conditions. It replaces that
submission with an ordinary `AEDClient.lot_hold` call. Original SQL filters,
query periods, macro scopes, and the independent MMS submission are preserved.
Ambiguous or unsupported CSR submissions fail with source locations. Multiple
CSR submissions in one signal branch are currently unsupported.

Set the emitted `AED_FACILITY`, `AED_HOLD_OPERATION`, and `AED_HOLD_CODE` explicitly.
The VG2 payload does not establish their AED mapping: query operation `2303`,
XRAY report text `2846`, and `HOLDCATEGORY` are not inferred as hold parameters.
`AED_MODE = None` reads `ENV_MODE`, defaulting to `test`. Test-mode manufacturing
writes return dry-run results without loading the SDK or credentials. Explicit
`lot_status` and `lot_get_attr` calls still perform reads against the PROD context.

```python
from vg2c.runtime import AEDClient

aed = AEDClient(facility=facility, mode="test")
response = aed.lot_hold(lotid=lot, operation=operation, holdcode=code, comment=note)
# Each business action is independent and editable:
# status = aed.lot_status(lot)
# aed.lot_release(lotid=lot, operation=operation, comment=note)
# aed.update_lot_attributes(lot, ("1064",), "Y", overwrite=True)
```

`1064="Y"` is the SOIMS CSR_IAM upgrade's business choice, not a translator
default. The updater preserves its first-attribute and overwrite behavior.
The old `{AED} candidates.csv` pipeline, config bootstrap, hardcoded site/business
defaults, and entrypoint decorator are retired. Such directives now produce a
unsupported-utility error; replace them with explicit client calls. Attribute update results
follow the reference updater's acknowledgement behavior; use an explicit status or
attribute read if your business logic requires post-write verification.

Install the approved API-key wheel together with the optional extra:

```powershell
python -m pip install "<approved-wheel-directory>/aed_client_apikey-6.2.0-py3-none-any.whl" "vg2c[aed]"
```

Use an isolated environment without `aed-client-winauth`, since both distributions
own the `aed_client` import. Credentials are loaded only for an actual request,
using nonempty `X_API_KEY`, then the encrypted `API_KEY`/`API_CONFIG` pair, then
`key.key` and `config_new.json` under an explicitly supplied absolute
`AED_SECRETS_DIR`. Keep secret files outside the repository and generated projects.
Set `REQUESTS_CA_BUNDLE` to an existing absolute corporate CA bundle; certificate
verification remains enabled. Existing proxy bypass entries are retained.
Generated projects do not import the SOIMS checkout, compiler, or visual editor.

CSR outcomes are saved atomically in `output/AED_results.csv`, including completed
actions before a failure. No manufacturing write retries are automatic. The CSR
report email includes actual outcomes and CCB references; it does not claim
readback-confirmed hold state. Failed actions stop subsequent workflow steps;
dry-run CSR actions suppress the associated production history append. Failure
email errors do not mask the original failure. Other HTML email directives still
have the existing local-rendering behavior.

Operations are sequential: the SDK uses process-global context and import-time
argument parsing. Do not call the client concurrently. Attribute updates are not
automatic containment or CCB-system association; those actions require separate
business logic. Test hold/release/attribute writes using `service_factory` fakes
only. A live status read additionally requires an authorized test facility/lot
and approved credentials; offline tests do not establish live API access.

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

Native scope payloads own their generated headers and bodies for macro, conditional, and loop scopes; the project emitter walks the resolved scope tree and assembles imports, assets, and source maps. HTML report emission belongs to a per-translation `HtmlReport`, while CSR submission and history policy belongs to a per-translation `CsrWorkflow`. Generated jobs call the installed `vg2c.runtime` APIs, which own file, SQL, email, and control behavior at execution time.

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

## Email delivery (DataSyncX)

Email steps call DataSyncX `send_msg`, which owns SMTP authentication and retries
(three attempts, about 30 seconds between attempts). The default sender is
`atmanalytic@intel.com`; an explicit legacy sender overrides the message's From
address. DataSyncX loads its platform/Python SMTP extension, or falls back to its
Python service using `ATMANALYTIC` from its configured `.env`. Windows Credential
Manager's `SMTP` entry is no longer used.

Body files and attachments resolve from the job workdir. `.htm` and `.html` body
files are sent as HTML; other files and literal bodies use plain text. Missing
attachments are skipped. One existing attachment is sent directly; several are
bundled into `attachments.zip` because DataSyncX accepts one file. Bundled files
must have distinct filenames. Delivery errors propagate from DataSyncX, and
`enabled=False` skips email without importing its service. Use Intel recipients,
as required by DataSyncX's email API.

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
