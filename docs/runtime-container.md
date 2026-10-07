# Runtime container prototype

Build from the repository root:

```sh
docker build --target final -t scripthost-runtime .
docker build --target validation -t scripthost-validation .
docker run --rm scripthost-validation
```

Place a VG2 job and its local inputs in `data/`. Make the output directory writable
by UID 10001 (or run with your own non-root UID/GID on Linux). Run:

```sh
docker compose run --rm runtime /jobs/job.spfsql --workdir /jobs/output --timeout 300
```

The image preserves the checkout layout. The launcher creates one fresh child per
job; the child enters original ScriptHost. There is no web server or editor.
The production stage excludes tests and private DataSyncX; validation adds tests.
R is installed for original local interpreter tasks.

Both compiler and generated jobs are included. Run prepared Python jobs through
the same launcher, for example `/app/jobs/ICMPCS.py --workdir /work`. The worker
selects `.py` case-insensitively, so the imserverless image can set
`SCRIPT_PATH=/app/jobs/ICMPCS.py` without changing `main.py` or `handle()`.
Supply the existing prepared `configsets.csv`, AED configuration/environment and
history inputs in the work directory; Python jobs do not automatically bootstrap
AED configuration. Direct `python job.py` exits with launcher instructions.

For live corporate queries, extend the runtime image in your corporate environment
with the approved DataSyncX 1.1.6 distribution and its native database/authentication
prerequisites. Configure network/CA and supply credentials at runtime. The inspected
DataSyncX Linux configuration uses `/app/.env`; bind-mount that file read-only when
needed. Never copy credentials into an image or the build context.

Credential-free Linux/container certification does not certify corporate database
connectivity inside the container. That remains UNCERTIFIED until run on the target
network with the approved dependencies. Stage 3 local Linux/container evidence
is recorded separately from live-service readiness.
