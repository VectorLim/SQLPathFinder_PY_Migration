# Portable VG2C Job

This folder is a standalone job repository. It contains editable `main.py`,
SQL/HTML assets, a runtime snapshot, the approved AED wheel, `Dockerfile`,
`release.yaml`, `automation/`, and `.github/workflows/release.yml`.
No compiler installation, local Docker, or files from the translating machine
are needed to publish and deploy through GitHub Actions.

## Push or Manual Deployment

Make this folder the repository root. GitHub does not run workflows nested
inside a job subfolder. From this folder, publish to a repository authorized
to use the shared SOIMS release runner:

```sh
git init -b main
git add .
git commit -m "Add generated job"
git remote add origin <repository-url>
git push -u origin main
```

Pushing `main` (or any branch other than `prod`) builds, validates, publishes to
Harbor, and deploys a shared `-test` app to IMServerless/Rancher. When it works,
release the real app (the one named in `release.yaml`) by pushing the same
commit to `prod`:

```sh
git push origin HEAD:prod
```

Alternatively, select **Actions > Build, push and deploy
VG2C job > Run workflow**. Deployment defaults on; uncheck it for image-only
publication. Both modes use committed source, not uncommitted local files.

`release.yaml` is the deployment source of truth: a plain IMServerless AppSpec.
Generation supplies a stable app name, hostname, and image repository based on
the original folder name.
Copying or renaming the completed folder does not change its deployment target.
`prod` deploys the base app named in `release.yaml`; every other branch shares
the `-test` app (the `'*'` entry in the `branches` map of
`automation/settings.yaml`). Add a branch there with its own unique suffix to
give it a separate app, or remove `'*'` so unlisted branches are ignored. The
`prod` branch still uses `ENV_MODE=test`; it does not switch to production.
That file also names the
Kubernetes Secrets the workflow attaches; neither is sent to IMServerless.
Two repositories with the same app target update the same application.

## IMServerless UI Upload

`release.yaml` can be uploaded as-is in the IMServerless UI. Its `:latest` tag
points at the newest successfully released `prod` image; for another
branch's app, add that branch's suffix to `funcname`, `dnshostname` and the
image repository. The workflow sets `version` and `action` itself; for a UI
upload, set `version` above the current one (for example `1.4` to `1.5`) and
`action: update` once the app exists. UI uploads do not attach the Kubernetes
Secrets (`NAME`/`PW`, AED keys) or the pull Secret; the workflow patches those
only into versions it deploys itself. An uploaded version therefore runs
without them; release through the workflow when the job needs database or AED
access.

## Shared Credentials

All authorized users reuse the same existing Secrets in `icmpcs-soims`:

| Secret | Keys | Purpose |
| --- | --- | --- |
| `workflow-testing-database` | `NAME`, `PW` | DataSyncX database authentication |
| `aed-api-key` | `API_KEY`, `API_CONFIG` | Authorized AED authentication |
| `itcaas-regcred` | `.dockerconfigjson` | Registry access |

The workflow checks these references before deployment and attaches them to
each new app/version using `envFrom` and `imagePullSecrets`. Missing Secrets
fail the release; it never invents credentials, copies them into Git, or bakes
them into an image. No per-user database/AED credential entry is required.
JobRuntime reads `NAME`/`PW`, selects the database reader, and scopes Linux
Kerberos environment lookup to each read. Optional commented `job.environ`
overrides are for local use only; never commit filled-in credentials.

The shared Linux runner requires Docker, Python 3 with venv, and kubectl, with
the `soims-release` label and repository authorization. Platform maintainers
provide access once through the `soims-test` GitHub environment/organization
settings or the runner's protected defaults:

| GitHub Setting | Runner Default |
| --- | --- |
| Secret `RANCHER_KUBECONFIG` | Protected `RUNNER_KUBECONFIG` file |
| Variable `RANCHER_CONTEXT` | `RUNNER_RANCHER_CONTEXT` |
| Variable `IMSERVERLESS_OWNER` | `RUNNER_IMSERVERLESS_OWNER` |
| Variable `IMSERVERLESS_DOMAIN` | `RUNNER_IMSERVERLESS_DOMAIN` |

Optional GitHub Secrets `REGISTRY_USERNAME`/`REGISTRY_PASSWORD` override the
registry Secret login. Optional variable `SOIMS_BASE_IMAGE` selects another
approved base. New repositories must be allowed to use the shared runner and
platform settings; pushing source cannot grant cluster permissions.

## Your Own Runner (One Click)

Without a shared runner, double-click `Start-Release-Runner.cmd` after the
first push. With Docker Desktop running (it prompts if not), it builds a runner
image, registers it to this repository's `origin` with label `soims-release`
using your Git login (repository admin), and keeps it running with Docker.
On first use it asks once for your Rancher kubeconfig; it is kept in
`%USERPROFILE%\.vg2c-runner`, never in the repository. Defaults: context
`cn01-linux`, domain `GAR`, owner = your Windows username. Override with
`runner\start-runner.ps1 -Owner <id> -Context <ctx> -Domain <domain> -Recreate`.
Afterwards, every `git push` releases while the PC and Docker stay on.
Double-click `Stop-Release-Runner.cmd` to stop it; pushes then queue on GitHub
until you start it again. Stopping cancels a release in progress.
The runner controls the host Docker engine: use it only for trusted repositories.

## Run Locally (One Click)

Double-click `Run-Local.cmd` to run `main.py` on this PC. The first run creates
a project `.venv` (Python 3.13, 3.12, 3.11 or 3.14 via the `py` launcher, else
`python`) and installs `dependencies/requirements-local.txt`, which adds the
DataSyncX git source (the Docker base already provides it; first install needs
Git and mfg-github access) to `requirements.txt`. Later runs start
immediately and reinstall only after either file changes. Delete `.venv` to
rebuild it. In VS Code, select `.venv` as the interpreter to run or debug
`main.py` directly. Put local `NAME`/`PW` (and any other runtime values) in a
project `.env`; it is git- and Docker-ignored. `.venv`, `Run-Local.cmd` and
`requirements-local.txt` are excluded from the Docker image.

## Image and Runtime

The workflow pulls `amr-registry.caas.intel.com/soia/icmpcs-soims:base`, builds
Linux/amd64, and installs `dependencies/requirements.txt`. The base supplies
the SOIMS function host, DataSyncX/Oracle system dependencies and corporate
trust. Registry/package-index access is required for uncached builds.

The image contains only `LOG_LEVEL=INFO` in `/app/.env` for DataSyncX startup.
Files are readable by the non-root host worker. A network-disabled smoke checks
imports and `main.handle` without invoking SQL, AED, email or manufacturing
operations. Deployment uses an immutable digest and verifies rollout/readiness;
this does not prove a successful authenticated business run.

The base loads `main.handle(job_id, context, header, body)` from `main.py`.
Exceptions propagate rather than reporting false success. `ENV_MODE=test` and
the script's `TEST_MODE_EXCLUDED` policy remain in force. Enabled AED operations
use the shared credentials; changing credentials does not enable excluded
operations or authorize production writes.

For a local image build:

```sh
docker build -t my-job:local .
docker run --rm --network none --entrypoint python my-job:local -c "import main; assert callable(main.handle)"
```

Default output is `/app/output` and is not persistent across pod replacement.
Configure writable persistent storage there when required. Time-based triggers
are managed separately in IMServerless; this release workflow does not create
schedules. Credential files, dotenv files, build tooling and outputs are
excluded from the image; credential files and outputs are also ignored by Git.