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

## imserverless (Linux container)

`main.py` exposes `handle()` for the imserverless Python function base image. Each
trigger runs `SCRIPT_PATH` in `WORKDIR` (both usually on the mounted share) through
`run_job`, logs one JSON line to stdout (ELK) and returns `(success, message, message)`.
The trigger body is ignored.

```sh
docker build -f imserverless.Dockerfile -t amr-registry.caas.intel.com/<project>/sqlpathfinder:1.0 .
docker push amr-registry.caas.intel.com/<project>/sqlpathfinder:1.0
```

1. Fill in [deploy/AppSpec_sqlpathfinder-app_1_0.yaml](deploy/AppSpec_sqlpathfinder-app_1_0.yaml)
   (image, share `networkpath`, `SCRIPT_PATH`, `WORKDIR`) and upload it in "Create Application".
2. In Rancher, create a secret (e.g. `db-credentials`) with key `TITAN` = titan DB password,
   add `envFrom: [{secretRef: {name: db-credentials}}]` to the deployment, and set the share
   login in `cifs-secret`. Never put `TITAN` in the AppSpec or image.
3. Ask the imserverless owner to enable the time-based trigger, then set the schedule.
4. Run with `ENV_MODE=test`, check ELK and share outputs, then switch to `ENV_MODE=prod`.

`DATASYNCX_USERNAME` (e.g. `titan`) makes MARS/ARIES readers use that DB account instead of
OS/Kerberos auth. The base image's `python --version` must be 3.11-3.13.

## Capability vocabulary

- SUPPORTED: exercised original behavior with asserted outputs in the documented subset.
- UNRESOLVED: a known required platform boundary is unavailable (Linux email delivery).
- UNCERTIFIED: no execution evidence for this capability or variant.
- RETIRED: compiler, editor, generated-Python runtime, and migration/reference runtime.

See [the utility matrix](docs/scripthost-utility-portability-matrix.md) for bounded
capabilities and [historical live evidence](docs/session-2.6a-live-datasyncx-validation.md).
Linux email recipient/role policy is preserved; no replacement sender is provided.

See [runtime container instructions](docs/runtime-container.md) for the prototype image.
