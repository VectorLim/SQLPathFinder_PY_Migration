# vg2c

`vg2c` compiles legacy VG2 `.txt` pipelines into Python projects with editable SQL and HTML assets.

`translate(input_path, out_dir=None)` returns `<root>/<sanitized-source-stem>/main.py`.
The root defaults to the source directory. Existing nonempty projects and batch name
collisions are rejected before generation writes files. Move the entire generated
directory together; the original VG2 file is not needed for execution.

Unsupported query blocks, including JMP/JSL, are skipped with source-located
`skipped-block` warnings. Generated Python retains a `pass` for each skipped block
and continues with supported steps. Skipped blocks do not create their outputs;
dependent steps may need those files supplied separately. Malformed recognized
commands and invalid workflow structure still fail compilation.

Generated code imports the installed `vg2c.runtime`, uses native Python controls and
one instance-scoped `MacroStore`, and rereads external SQL, HTML and CSS assets at
execution time. By default,
`run()` takes no parameters and explicitly creates its `JobRuntime` inside the
function. It reads and writes job data under `<project>/output`; edit the local
`workdir` assignment to choose a different data folder. SQL assets stay under
`<project>/sql` and report shells under `<project>/html`. There is no process-wide
working-directory change.
WRITE-FILE bodies are editable assets under `<project>/files`, retaining their
predetermined relative filenames. Repeated target names use `files/step_NNNN/`
subdirectories so each source step keeps its own template; macro-derived filenames
use a stable `files/step_NNNN.txt` asset and retain the resolved logical filename at
runtime. At the original execution point, `job.load_file` registers static content
and `job.update_file` expands macro templates into a per-run memory store. These
operations never overwrite the original assets or create resolved WRITE-FILE copies
under `output`. Each invocation rereads the templates and starts with an empty store.
SQL/CSV, macro rows, reports, email bodies, and file operations read by logical
filename through that store; `job.read_file(name)` exposes the same content to Python.
Explicit copy/append and query/report outputs still write their requested results.
External commands and single email attachments require disk files. Before external
commands, static inputs and macro-expanded executables are copied to their logical
paths under the command's working directory, preserving the original assets.
Other updated in-memory arguments and email attachments require an explicit copy
to a different output filename before passing that path to the external consumer.

CSV table inputs and backend node aliases are separate from SQL parameter binds.
Direct named macro lookups such as `macros["CSRV"]` return `None` when absent.
Unresolved macro tokens in SQL and templates still raise an error.

CSS is emitted under `<project>/styles` and published on HTML-RUN execution.
Explicit relative CSS assets are resolved from the generated project (or an
already-published stylesheet in the workdir), copied beside the output on render,
or embedded for CSSEMBED. Editing a generated CSS asset does not require recompiling VG2. Deferred reports read CSV data and resolve raw macro
paths/options at layout time. Data and headers are escaped once; authored markup
is preserved. Unsupported active report options fail with source locations.
Repeated report `SORT` rows are retained in the editable table's `data-sort`
attribute. Row spans merge adjacent cells within the complete preceding sort-key
prefix; sort fields without row spans still establish group boundaries.
Legacy browser/delivery/security/chart integration settings produce explicit
`local-html-only` diagnostics; the local renderer does not implement those services.

Literal values belong to their individual calls rather than shared
generated globals. Embedded Python must use explicit runtime functions and
`workdir`; the retired `ctx` API and top-level return/yield have diagnostics.

TXT prompt IDs such as `Step 6-1.` and `Step 6-2.` identify contiguous pre/post
members. These emit module-level functions named from the first member's prompt,
such as `prepost_6_fetch_data(job, macros)`, called at the original point in `run()`.
Names are sanitized Python identifiers with suffixes for duplicates; prompts
without a description fall back to `prepost_6`. Job and macro state are passed
explicitly, with the macro argument omitted when unused. Ordinary query
IDs such as `Step 7.0.` stay inline. Missing or conflicting IDs are not guessed;
mixed control scopes stay intact, with identified child groups called inside their
original gates. Unmarked blocks bounded by matching group IDs remain in that group.
Unmarked instance-consolidation SQL also belongs to the preceding fetch group when
its table inputs match that group's outputs and instance. Query order is unchanged.

The generated project has been checked in a clean Linux virtual environment using
a built and installed `vg2c` wheel. The offline probe covers a moved generated project,
changed working directory, parameterless `run()`, SQLite, crosstab/CTARRAY,
editable SQL/HTML/CSS, linked/embedded styles, Unicode and failure handling.
This is **not** certification of private DataSyncX/Oracle/AED services or the original
ScriptHost engine: those require their target infrastructure and credentials.
The old `MacroState`, `PipelineContext` and runtime HTML preview/rendering engines,
the unused source-embedding pipeline, the visual editor (`vg2c_ui`) and its
semantic editing/workflow layer have been retired. HPC/JMP/JSL remain unsupported.

---


## Offline installed-wheel Linux verification

From a clean Linux checkout with Python 3.12, the repository's Session 05 CI
workflow builds the wheel, installs the wheel into a separate virtual environment
and executes a generated project from a detached directory without a compiler
checkout, VG2 source or ScriptHost at runtime:

```bash
python -m pip install build wheel setuptools pandas==3.0.3 duckdb==1.5.6 "python-dotenv>=1.2,<2"
python -m build --wheel --no-isolation --outdir /tmp/vg2c-dist
python -m venv /tmp/vg2c-wheel-venv
/tmp/vg2c-wheel-venv/bin/python -m pip install pandas==3.0.3 duckdb==1.5.6 "python-dotenv>=1.2,<2"
/tmp/vg2c-wheel-venv/bin/python -m pip install --no-deps /tmp/vg2c-dist/*.whl
python tests/deployment/verify_linux_wheel.py \\
    --python /tmp/vg2c-wheel-venv/bin/python \\
    --wheel /tmp/vg2c-dist/*.whl
```

The `--no-deps` wheel install is intentional for this **synthetic offline SQLite**
probe: runtime imports require pandas, DuckDB and python-dotenv, while external DataSyncX and production
credentials are absent. For a real deployed job, install all declared project
requirements from an authorized corporate package source and validate its
reader/service integration separately. Do not replace the production dependency
install with this minimal offline test environment.

## AED API clients

`vg2c.runtime.aed_api` is the upstream ICMPCS `AEDAPIClient` (imports adapted only),
`vg2c.runtime.aed_client.AEDClient` wraps it with input validation, URL encoding and
per-job credentials. Jobs obtain the client through the `job.aed` property.
Set a default site with `JobRuntime.from_script(..., site="KM")` or `SITE`, or
set the `FACILITY` macro explicitly.

A legacy CSR submission (`RoboCopy.va <files> <source> <<<CSRPATH>>> ...`) compiles
to a read-only placeholder: `job.aed.test_connection("LOT_TO_TEST")` runs once,
without an exclusion guard. Replace `LOT_TO_TEST` with a known lot and enable AED
before running this step; an excluded AED client is `None`. The producer SQL, reports,
email and history steps around it are emitted unchanged. Replace the placeholder
with the job's real hold/contain logic; no CSR field is mapped to AED arguments.
Other `<<<CSRPATH>>>` file operations fail at compile time with a source location.

The optional shared `.env` beside `main.py` supplies
`ENV_MODE=test|prod` and authentication/network values. It is loaded once at job
startup, with process environment taking precedence, including blanks.
`ENV_MODE` defaults to `test`; other values are rejected. The normalized value is
available as `macros['ENV_MODE']`.

Every generated `main.py` has one editable setting near the top:

```python
TEST_MODE_EXCLUDED = {"aed", "email", "mms"}
```

The setting is passed once to `JobRuntime.from_script`. Every `JobRuntime` action
method checks `enabled()` before doing work and returns when its operation name
is excluded in test mode: `send_mail` is `email`, `submit_mms` is `mms`,
the `aed` property is `aed`, and the rest use their own name (`sql`,
`copy_file`, `write_file`, ...), so any of them can be listed. Readers such as
`row_count` and `read_macro_row` are never skipped. Generated MMS scopes retain
their `DMWF` condition and `job.enabled('mms')` gate.
Production runs allow all operations. Removing an operation from the set permits it in test
mode too, including real AED writes, email, or MMS delivery. AED still uses the
production service context, so only remove `aed` when that is intended.
When excluded, `job.aed` returns `None` without loading the SDK or credentials.
Guard the complete operation, including any dependent history updates:

```python
aed = job.aed
if aed is not None:
    result = aed.lot_hold(
        lotid=lot,
        operation=operation,
        holdcode=holdcode,
        comment=comment,
    )
```

Jobs supply confirmed lot IDs, operation and hold codes, comments, and reference
numbers. CSV validation, deduplication, CCB requirements, and reporting belong to
the job's business logic. `AEDClient` validates identifiers and returns each
successful response; unsuccessful or malformed responses raise an exception.

The write methods and their arguments match `aed_client.ManufacturingService`
6.2.0 one-to-one:

| Method | Arguments |
|---|---|
| `lot_hold` | `lotid, operation, holdcode, comment` |
| `lot_release` | `lotid, operation, comment` |
| `lot_set_attr` | `lotid, attributeid, attributevalue` |
| `lot_contain` | `lotid, operation, reference_number` |
| `lot_uncontain` | `lotid, operation, reference_number, comment` |

For an attribute write, use:
`aed.lot_set_attr(lotid=lot, attributeid='<approved-attribute-id>',
attributevalue='Y')`. `AEDClient` URL-encodes every value, because the SDK
formats them into the request URL unencoded. Calls run sequentially; if a later
call fails, earlier writes may already have completed. There is no batch rollback,
automatic failure email, history tracking, or dry-run simulation. Use explicit
status or attribute reads when post-write verification is required.

For a generated job in a Linux image, `docker run --env-file aed.env` supplies the
shared mode and credential values. Normal production runs use `ENV_MODE=prod`
plus the credentials and CA bundle described below. Windows batch/executable
steps still need Linux replacements before the whole job can run there. With the
default exclusions, `job.aed` returns `None`, including for status and attribute
reads. Existing generated projects must be migrated to the direct AED API and
the `aed` exclusion name; old CSR helpers are not retained.

To explicitly check connectivity before manufacturing operations, read a known
accessible lot through the same client:

```python
aed = job.aed
if aed is not None:
    aed.test_connection("7645D737")
```

This check is read-only and is never automatic.
It verifies authentication and lot read access, not permission to hold, release,
or change attributes.

Install the approved API-key wheel together with the optional extra:

```powershell
python -m pip install "<approved-wheel-directory>/aed_client_apikey-6.2.0-py3-none-any.whl" "vg2c[aed]"
```

Use an isolated environment without `aed-client-winauth`, since both distributions
own the `aed_client` import. Windows and Linux use the same API-key authentication;
no logged-in Windows identity or SSPI dependency is required.
Credentials supplied in `.env` or the process
environment are loaded only for an actual request, using nonempty `X_API_KEY`, then
the encrypted `API_KEY`/`API_CONFIG` pair, then `key.key` and `config_new.json`
under an explicitly supplied absolute `AED_SECRETS_DIR`. Keep secret files outside
the repository and generated projects. Set `REQUESTS_CA_BUNDLE` to an existing
absolute corporate CA bundle; certificate verification remains enabled. Existing
proxy bypass entries are retained. Generated projects do not import the SOIMS
checkout, compiler, or visual editor.

No manufacturing write retries are automatic. Failure email errors do not mask the
original failure. Other HTML email directives still have the existing local-rendering
behavior.

Operations are sequential: the SDK uses process-global context and import-time
argument parsing. Do not call the client concurrently. Attribute updates are not
automatic containment or CCB-system association; those actions require separate
business logic. Test hold/release/attribute writes using `service_factory` fakes only.
A live status read additionally requires an authorized test facility/lot and approved
credentials; offline tests do not establish live API access.

### Portable Linux Projects

Create a new empty project:

```powershell
vg2c init my-job
```

Run `vg2c init` without a path to initialize the current empty directory. Existing
nonempty directories are never overwritten. The Python API is
`vg2c.init_project(path)`. CLI/API translations and companion exports include
the same packaging automatically; no separate initialization step is needed.

Each project contains:

```text
main.py
Dockerfile
.dockerignore
README.md
sql/                         # Translated assets, when present
html/                        # Translated assets, when present
dependencies/
    requirements.txt
    aed_client_apikey-6.2.0-py3-none-any.whl
    vg2c/
        __init__.py
        runtime/             # Runtime source and Intel CA, without compiler
```

All packaging resources originate from `vg2c/project_template` inside the
installed VG2C distribution. The runtime snapshot is copied from that same
distribution. Generated projects do not reference this checkout or require
VG2C to be installed on the build or execution machine.

```powershell
Set-Location my-job
docker build -t my-job:local .
docker run --rm --network none --entrypoint python my-job:local -c "import main; assert callable(main.handle)"
```

The Dockerfile uses `amr-registry.caas.intel.com/soia/icmpcs-soims:base`; override
`SOIMS_BASE_IMAGE` at build time for another approved compatible image. That
base provides the imserverless host, DataSyncX/Oracle system libraries and trust.
Python requirements are installed during the build; uncached builds require
registry and package-index access. This is not an offline wheelhouse for every
third-party dependency. No Windows virtual environment or SSPI dependency is copied.

The platform loads `main.handle(job_id, context, header, body)`, matching SOIMS.
The shared base already supplies `/etc/icmpcs/conf/func.json` for `main.py`, so
projects do not duplicate that config or add a separate entrypoint wrapper.
The handler invokes `run()` and propagates failures. Mount persistent
writable storage at `/app/output`; inject database and AED credentials through
runtime Secrets, not image layers. Keep `ENV_MODE=test` until production is approved.
The generated README includes a read-only AED connection-test command.

For encrypted AED credentials, remove stale `X_API_KEY` settings: a direct key
takes precedence over `API_KEY`/`API_CONFIG` or `AED_SECRETS_DIR`. Existing SOIMS
encrypted credentials succeeded with API-key-only auth and TLS verification;
Windows SSO is not required. Docker build and hosted Linux execution still
require separate verification.
## Architecture Overview

```mermaid
graph TD
    A[VG2 Source File] --> B[Parse & Classify]
    B --> C[Resolve]
    C --> E[Dispatch]
    E --> F[Emit Native Python + SQL/HTML Assets + Step Source Map]
    F --> G[CompilationResult]
    G --> H[translate / CLI project writer]
    G --> I[Windows companion preview]

    subgraph Utilities
        J[UtilitySpec / @emittable call rendering] -. definitions .-> F
        J -. direct vg2c.runtime calls .-> F
    end
```

`CompilationResult` and the compiler-stage objects are the authoritative chain. The emitter records per-step source ranges and asset-producing parameters while it generates Python; the companion uses them to map source blocks to generated code and assets.

Native scope payloads own their generated headers and bodies for macro, conditional, and loop scopes; the project emitter walks the resolved scope tree and assembles imports, assets, and source maps. HTML report emission belongs to a per-translation `HtmlReport`. Generated jobs call the installed `vg2c.runtime` APIs, which own file, SQL, email, and control behavior at execution time.

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

## SQLPathFinder companion (Windows)

The optional companion observes a running `sqlpathfinder3` process through a
small .NET Framework native-window/MSAA helper. It detects open VG2 documents
automatically and translates a uniquely selected document on startup, when a
document is opened later, or when the selection changes. If several documents
are open, choose one in the filename-labelled **Open document** selector.
**Update Python** is always enabled: every press requests the latest native
source, with repeated presses during an update combined into one follow-up
refresh. **Refresh documents** also remains available.
The helper identifies the selected SQLPathFinder designer window,
invokes the accessible **File -> Show Query...** action, captures the newly
created editor, and closes that editor with a normal window close. The source is
then compiled into the latest local preview; an identical regenerated source is
still a successful refresh because the editor window is new.

Refresh runs away from the Tk UI with bounded waits. An existing Show Query
editor is left alone and reported as an actionable error, and inaccessible or
disabled menus, missing designers, empty output, and timeouts retain the last
good preview. The helper never runs queries, saves VG2 files, chooses native
dialogs, discards edits, injects Ctrl+Q, or relies on an unverified menu command
ID. The legacy `--pid` option remains available for passive observation and
compatibility. Passive read-only observation and opening exported `.txt`/`.spfsql`
previews remain supported.

Open or edit the VG2 in SQLPathFinder, then start the companion from the
repository root with no arguments:

```powershell
.\start-vg2c-companion.bat
```

The preview appears automatically when a document can be selected. After designer
edits, press **Update Python**, then use **Export generated** when the preview
is ready. The **Runtime library (installed)** tree shows the actual
`vg2c/runtime` folder and its Python files alongside the generated project.
Runtime sources are read-only library references; exports still contain the
generated project and its assets and use the installed `vg2c` package.
Clicking an emitted SQL, HTML, CSS, or other generated asset filename in
`main.py` focuses that file in the companion tree and preview. Designer-step
clicks focus `main.py` and its exact emitted block when the selected designer
label has one matching compiler `/PROMPT-TEXT=Step <id>.` record. The companion
does not guess mappings for missing or duplicate step IDs, and leaves its current
view unchanged while another application has focus.
Optional VG2 command-line paths remain available for identification and offline
context.

The launcher uses the repository `.venv` when present, then an installed
`vg2c-companion` command. The package entry point is also available with
`python -m vg2c_companion` in an installed wheel environment.

The `.VG2` argument supplies identification and context only. Export writes
`main.py` and compiler-owned SQL/HTML/CSS assets into a new or empty directory;
existing files are refused. An exported `.txt` or `.spfsql` source can be
opened directly for offline preview. The generated pane is read-only; **Edit
copy** opens an independent buffer so a later source refresh cannot erase work.
Use **Save copy** in that window to choose a new file; existing files and the
native/source inputs are refused. **Export generated** writes the retained
compiler snapshot and its generated assets separately.

The observer is compiled on first launch into the current user's local cache
with `Accessibility.dll` and `System.Runtime.Serialization.dll` references;
it does not require WPF UI Automation assemblies. It uses bounded native
window/edit reads and optional short MSAA selection reads, with honest missing,
exited, inaccessible, and truncated states. Selection-to-generated jumps require
a matching SQLPathFinder process, designer title and fingerprint, active designer
focus, and one compiler `/PROMPT-TEXT=Step <id>.` match; missing or duplicate
IDs do not navigate. Editor carets map only while the Show Query editor has focus,
and focus elsewhere preserves the current companion view.

## LAN Docker test deployment

The Docker deployment serves the visual editor over one LAN port with anonymous, isolated browser workspaces. Users upload VG2/data files and download generated results; generated workflows are not executed. See [the Docker LAN guide](docs/docker-lan.md) for the Intel dependency-build prerequisites, Docker Compose commands, firewall setup, and workspace lifecycle.
