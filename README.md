# SQLPathFinder runtime prototype

VG2 -> thin launcher/run_job -> fresh child process -> PortableScriptHostRuntime ->
original SPFManager/GetQuery/task hierarchy/SPFGlobals -> original ScriptHost semantics.

The original ScriptHost tree is vendored at `src/scripthost_portable/_vendor/SPSQL3_py`
and ships inside the package, so `pip install .` works without the repository checkout.
The original source archive is preserved as provenance in `scripthost-utilities-decompiled`.

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

Follows the SOIMS "Script Running on imserverless Guidance". `main.py` exposes `handle()`
for the imserverless Python function base image. Each trigger runs `SCRIPT_PATH` in
`WORKDIR` (both usually on the mounted share) through `run_job`, logs ECS JSON lines to
stdout (ELK) and returns `(success, message, message)`. The trigger body is ignored.

```powershell
$tag = "amr-registry-pre.caas.intel.com/<project>/sqlpathfinder:1.0.0"
docker build -f imserverless.Dockerfile -t $tag `
  --build-arg HTTP_PROXY=http://proxy-dmz.intel.com:912 --build-arg HTTPS_PROXY=http://proxy-dmz.intel.com:912 .
docker push $tag
```

1. Fill in [DeploymentYaml/Stg/AppSpec_sqlpathfinder-app_1_0.yaml](DeploymentYaml/Stg/AppSpec_sqlpathfinder-app_1_0.yaml)
   (image tag, share `networkpath`, `SCRIPT_PATH`, `WORKDIR`) and upload it in "Create Application".
   Per-deployment values can be overridden in the imserverless UI instead of the YAML.
2. In Rancher, create a secret (e.g. `db-credentials`) with key `TITAN` = titan DB password,
   add `envFrom: [{secretRef: {name: db-credentials}}]` to the deployment, and set the share
   login in `cifs-secret`. Never put `TITAN` in the AppSpec or image.
3. Configure the cron / time-based trigger in the UI (owner enables it first).
4. Verify: pod running in imcloud1.0, then ELK shows `reaching inside the handle function`
   and the result line with `script_path`, `success`, `generated_outputs`.
5. Run with `ENV_MODE=test`, check share outputs, then switch to `ENV_MODE=prod`.

`DATASYNCX_USERNAME` (e.g. `titan` or `GAR\idsid`) makes MARS/ARIES readers use that account
instead of OS auth; `DATASYNCX_PASSWORD` supplies its password. Both may live in the DataSyncX
`.env` (`/app/.env` in the container) or come from secret env vars. The base image's
`python --version` must be 3.11-3.13.

## Capability vocabulary

- SUPPORTED: exercised original behavior with asserted outputs in the documented subset.
- UNRESOLVED: a known required platform boundary is unavailable (Linux email role verification).
- UNCERTIFIED: no execution evidence for this capability or variant.
- RETIRED: editor, embedded/generated runtime, and migration/reference runtime.

See [the utility matrix](docs/scripthost-utility-portability-matrix.md) for bounded
capabilities and [historical live evidence](docs/session-2.6a-live-datasyncx-validation.md).
Linux email recipient/role policy is preserved; no replacement sender is provided.

See [runtime container instructions](docs/runtime-container.md) for the prototype image.

## Clean Python compiler (Stage 2)

Compile the two current targets against the installed Stage 1 API:

```sh
python -m vg2c ICMPCS.txt output/aed-migration/CSR_IAM_v2.aed.txt --out-dir output/clean-python
python -m scripthost_portable.launcher output/clean-python/ICMPCS.py --workdir <prepared-job-directory>
```

The compiler restores parsing, classification, scope/source identity and direct
emission from the useful `main` architecture. It emits one ordinary `run()` with
calls to `macros`, `query`, `utilities`, `reports` and `aed`. `OPERATION` and
simple `out_date >= SYSDATE - N` / `TRUNC(SYSDATE) - N` filters become editable
constants; all other SQL/report content and query options reach the original API.
The launcher owns runtime initialization. Generated files need the installed
`scripthost_portable` package and do not import the compiler or embed runtime code.

Only the two target jobs' operation surface is supported. Unsupported options,
utilities, malformed scopes, multiple/nested macro scopes, positional macros and
multi-clause conditions fail compilation. Utility prompt labels affect logging
only and are omitted because Stage 1's utility facade has no prompt argument.
Query/report prompts are retained. Macro references after `END-MACRO` are rejected
because the API retains its loaded table until job cleanup.

Stage 2 tests compilation, output structure and a small offline API smoke test.
Full differential parity, deployment bootstrap and production cutover remain Stage 3.

## AED IAM jobs

`ICMPCS.txt` and `CSR_IAM_v2.txt` now use one `{AED}` task in place of the
CSR/MMS preparation and posting chain. All new integration logic lives in
`src/scripthost_portable/aed_api.py`, reusing the existing `aed_updater.py`.
Both Dockerfiles install the SOIMS API-key client wheel;
the imserverless image includes both IAM scripts under `/app/jobs`.

Select `SCRIPT_PATH=/app/jobs/ICMPCS.txt` (or `CSR_IAM_v2.txt`), a writable
`WORKDIR`, and `SITE=KM` (or another site). Inject `X_API_KEY` through deployment
secrets. DataSyncX credentials remain as described above. Start with `ENV_MODE=test`;
only `prod` writes AED/history.
Site is the only required site-specific selection after deployment credentials
and the job entry point are configured.

Outputs, snapshots and `HIST` (default `<WORKDIR>/<SITE>/HIST/HIST.txt`) stay in
`WORKDIR` on the single AppSpec `data` volume (`/mnt/data`). Config is read live from
the ICM_PCS network path on every run:
`\\AZATSHFS.intel.com\AZATAnalysis$\MAOATM\Config\VF_POR_Cfg\ICM_PCS\<SFOLDER>\<SITE>\CONFIG\config.txt`,
where `SFOLDER` defaults to the script filename (`ICMPCS_CWFNCO_CSR_IAM.txt` ->
`ICMPCS_CWFNCO_CSR_IAM`). Windows opens the UNC path natively; Linux reads it over SMB
with `CIFS_USERNAME`/`CIFS_PASSWORD` (inject the password as a deployment secret).
`ICMPCS_ROOT`, `SFOLDER` and `HIST_PATH` optionally override these.
The existing `icmpcs,parameter,value[,comment]` format is authoritative;
ICMPCS rows become `config.json`, `configsets.csv`, and original-name environment
variables. Explicit environment values override matching keys. Secrets and OS
control variables are rejected as config keys and never included in snapshots.
MARS defaults to `<SITE>.[<facility>.].MARS` (KM: `KM.[A15_PROD_21.].MARS`) and ARIES
to `<SITE>.ARIES`; `dEmail` must be supplied
by the config or an original-name environment override. IAM attribute/value
defaults remain `1064`/`2446`, overridable with `ATTR_LIST`/`SKIP_OPERATION`.

`AED_results.csv` records each distinct facility/lot outcome. Production history
is saved to the share's `<job>/<site>/HIST/HIST.txt` only after verification;
legacy `LOT` and `Lot_NCORisk` history columns are accepted. Historical lots stay
excluded even if their attributes later clear. Earlier successes survive a later
lot failure, and any failed lot makes the job fail. Run one writer per job/site.
Dry runs write local audit output but neither AED attributes nor shared history.
The existing Linux email role-verification limitation still applies to reports.
