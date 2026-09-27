# SQLPathFinder runtime prototype

VG2 -> thin launcher/run_job -> fresh child process -> PortableScriptHostRuntime ->
original SPFManager/GetQuery/task hierarchy/SPFGlobals -> original ScriptHost semantics.

Use this repository as a checkout. Keep `src/scripthost_portable` beside
`scripthost-utilities-decompiled/SPSQL3_py`; standalone wheel distribution is out of scope.
The original source archive is preserved as provenance.

## Setup and execution

Use Python 3.11-3.13 (Linux CI uses 3.12):

```sh
python -m pip install -e ".[dev]"
python -m pytest tests/scripthost_portable -q
python -m scripthost_portable.launcher job.spfsql --workdir output --timeout 300
```

Install local R separately for original R tasks. For live Oracle queries, install
corporate DataSyncX 1.1.6 using `uv sync --extra live` or your approved installation.
Configure DataSyncX authentication and network access in the inherited environment.
Credentials are not job payloads.

```python
from scripthost_portable.worker import ScriptHostJob, run_job
result = run_job(ScriptHostJob(
    script_path="/absolute/path/job.spfsql",
    working_directory="/absolute/path/output",
), timeout=300)
assert result.success, result.message
```

Each call starts a fresh child process. Never use the in-process runtime concurrently
in threads. Original VG2 parsing, control flow, utility arguments and reports remain
in ScriptHost. Portable helpers cover only external transport and OS operations.

## Capability vocabulary

- SUPPORTED: exercised original behavior with asserted outputs in the documented subset.
- UNRESOLVED: a known required platform boundary is unavailable (Linux email delivery).
- UNCERTIFIED: no execution evidence for this capability or variant.
- RETIRED: compiler, editor, generated-Python runtime, and migration/reference runtime.

See [the utility matrix](docs/scripthost-utility-portability-matrix.md) for bounded
capabilities and [historical live evidence](docs/session-2.6a-live-datasyncx-validation.md).
Linux email recipient/role policy is preserved; no replacement sender is provided.

See [runtime container instructions](docs/runtime-container.md) for the prototype image.
