# Portable VG2C Job

This folder is independent of the translating machine and the installed VG2C
compiler. `dependencies/vg2c/runtime` contains the runtime snapshot,
`dependencies` contains the approved AED wheel and dependency requirements,
and `main.py` contains the editable job. SQL and HTML assets stay editable.

Build from this folder:

```sh
docker build -t my-job:local .
docker run --rm --network none --entrypoint python my-job:local -c "import main; assert callable(main.handle)"
```

The default base is `amr-registry.caas.intel.com/soia/icmpcs-soims:base`.
Override it with `--build-arg SOIMS_BASE_IMAGE=<approved-image>` when needed.
The base supplies the imserverless host, DataSyncX/Oracle system dependencies
and corporate trust. Additional Python packages in `dependencies/requirements.txt`
are installed during the build. Registry and package-index access are required
for uncached builds; this is not an offline wheelhouse for every third-party library.

The hosted entry is `main.handle(job_id, context, header, body)`, matching SOIMS.
It calls `run()` and returns success only if the job completes without an exception.
The compatible shared base supplies `/etc/icmpcs/conf/func.json` configured to
load `main.py`; this project does not duplicate that config or use a wrapper file.
Mount writable persistent storage at `/app/output` for job results.

The image defaults to `ENV_MODE=test`. Approve production before setting
`ENV_MODE=prod`. Configure `SITE` and database credentials through runtime Secrets
according to your DataSyncX/base image contract. Do not add credentials to source.

For AED, inject an authorized `X_API_KEY`, the encrypted `API_KEY`/`API_CONFIG`
pair, or mount `key.key` and `config_new.json` read-only and set `AED_SECRETS_DIR`
to their absolute container directory. A direct `X_API_KEY` overrides encrypted
credentials, so remove stale keys when changing credential sources.

To test AED without running the job or manufacturing writes:

```sh
docker run --rm --entrypoint python \
  --mount type=bind,source=/private/aed-secrets,target=/run/secrets/aed,readonly \
  -e AED_SECRETS_DIR=/run/secrets/aed my-job:local \
  -c "from vg2c.runtime import AEDClient; assert AEDClient('A15').test_connection('7645D737')"
```

Use a known authorized lot for your facility. This verifies read access, not
permission to change manufacturing state. Secret files, `.env` files, local
virtual environments and outputs are excluded from the Docker context.