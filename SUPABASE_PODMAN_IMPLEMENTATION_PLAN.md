# Local Supabase Authentication + Podman Migration Implementation Plan

## 0. Scope, baseline, and non-negotiable invariant

This is the reviewed implementation plan for adding fully local Supabase authentication with strict per-user persisted-data isolation and making Podman the primary container runtime for the current PYTHONPathFinder web deployment.

Primary security invariant:

> An authenticated user must never be able to read, modify, enumerate, overwrite, delete, download, archive, or otherwise access another user's persisted workspace or data.

The enforcement boundary is the backend filesystem workspace root selected from the authenticated Supabase user identity. The browser never chooses a user ID or ownership prefix.

This plan intentionally does **not** add Supabase Storage. The current compiler/editor architecture is filesystem/`Path` based, and the existing `WorkspaceManager` + `DocumentStore` already provide the right containment boundary. Introducing object storage would add cache/synchronization logic without a demonstrated need. Supabase is used for the capability that clearly benefits from it: authentication and session/user identity.

Likewise, this plan does **not** add PostgREST, Realtime, Studio, Edge Functions, Supavisor, an API gateway, or an application metadata database merely because they are part of the full Supabase distribution. The minimum runtime target is:

```text
Browser
  |-- Supabase Auth JS --------------------> local Supabase Auth
  |                                           |
  |                                           v
  |                                      local Postgres
  |
  |-- Bearer access token --> FastAPI vg2c-ui
                                |
                                |-- verify token/current user with local Auth
                                |
                                v
                         WorkspaceManager
                         /data/users/<auth-user-uuid>/
                                |
                                v
                          DocumentStore
                    existing compiler/editor flow
```

Non-goals for this milestone:

- Supabase Cloud or any other cloud-hosted authentication/storage service.
- Anonymous application access after migration.
- Social login, phone login, MFA, SSO, OAuth providers, password-reset email infrastructure, or external SMTP.
- Sharing workspaces between users.
- Multiple named workspaces/projects per authenticated user.
- Executing generated Python.
- Introducing application DB tables when the filesystem already represents the required state.
- A repository/service abstraction around compiler semantics already owned by `DocumentStore`.
- Maintaining Docker- and Podman-specific Compose files in parallel.

---

## 1. Current repository state

### Remote baseline verified

Planning baseline: GitHub `main` at:

```text
5d7df476ecd7edcf7f5150a09179bced02bfb26b
Add Supabase auth and storage implementation plan
```

The preceding executable-code commit is:

```text
07f9670b70fe2b85c84291b1ef365d4a980814b9
Add Docker LAN workspace deployment
```

The `5d7df476...` commit added only `SUPABASE_AUTH_STORAGE_PLAN.md`; the executable application/container code remains the anonymous workspace implementation introduced by `07f9670...`.

A literal `git pull` could not be performed in the ChatGPT execution container because this session has no checked-out copy of the repository and outbound GitHub DNS/network access is unavailable there. The connected GitHub repository was queried directly instead, and the remote `main` head was rechecked immediately before writing this plan. The Phase 2 local-agent procedure begins with a real `git pull --ff-only` and must stop if the local checkout does not match the expected reviewed baseline or a newer remote commit requires re-review.

### Relevant repository files inspected

Container/configuration:

- `compose.yaml`
- `Dockerfile`
- `.env.docker.example`
- `docs/docker-lan.md`
- `start-vg2c-ui.bat`
- `pyproject.toml`

Backend/persistence:

- `src/vg2c_ui/app.py`
- `src/vg2c_ui/services/workspaces.py`
- `src/vg2c_ui/services/document_store.py`
- `src/vg2c_ui/api/workspace.py`
- `src/vg2c_ui/api/documents.py`
- `src/vg2c_ui/api/translation.py`
- corresponding change/SQL APIs through the shared `DocumentStore` dependency

Frontend/session boundary:

- `src/vg2c_ui/frontend/src/api.ts`
- `src/vg2c_ui/frontend/src/App.tsx`
- `src/vg2c_ui/frontend/src/useWorkspace.ts`
- `src/vg2c_ui/frontend/package.json`

Tests:

- `tests/ui/test_workspace_sessions.py`
- existing `DocumentStore` and UI/compiler regression tests

### Current dependencies that matter to container builds

The Python package has a private `datasyncx` Git dependency and uses an internal Python package index. Container builds therefore already depend on access to Intel internal services plus corporate certificate/proxy configuration. The existing build secret `git_credentials` is specifically intended to avoid placing Git credentials into image layers.

This is important on Windows + Podman Machine: the Linux VM must be able to reach the corporate/VPN resources and trust the required CA chain during the image build.

---

## 2. Current Docker/container architecture

`compose.yaml` currently defines exactly one service, `vg2c-ui`.

Current behavior:

- Builds the multi-stage `Dockerfile`.
- Publishes `${APP_PORT:-8765}:8765`.
- Bind-mounts `./data:/data`.
- Runs read-only except `/data` and a `/tmp` tmpfs.
- Drops all Linux capabilities and sets `no-new-privileges`.
- Uses `init: true`.
- Has an HTTP healthcheck against `/api/health`.
- Restarts `unless-stopped`.
- Passes workspace retention/quota settings by environment variable.
- Passes a file-backed build secret named `git_credentials`.

The `Dockerfile`:

- builds the React frontend with Node 22;
- builds the Python environment with Python 3.13 + `uv`;
- uses `RUN --mount=type=secret,id=git_credentials` for the private Git dependency;
- runs as non-root UID/GID 10001;
- copies the compiled frontend into the Python package;
- sets `VG2C_DATA_DIR=/data`.

### Docker-specific or compatibility-sensitive assumptions

The repository does not use the Docker Engine API directly. There is no Docker socket mount, Docker SDK, Swarm feature, or Docker-only application code.

The compatibility-sensitive Compose/build features are:

1. `build.secrets` plus Dockerfile `RUN --mount=type=secret`.
2. `depends_on: condition: service_healthy` once Supabase services are added.
3. healthchecks.
4. bind mounts and named volumes.
5. `tmpfs`.
6. `read_only`.
7. `security_opt: no-new-privileges:true`.
8. `cap_drop: ALL`.
9. `init: true`.
10. restart policies.
11. environment-file interpolation.
12. service-name DNS on the default Compose network.

Podman itself supports the underlying runtime/build capabilities used here, including build secrets, `--init`, `no-new-privileges`, volumes, healthchecks, rootless operation, and DNS-backed container networking. The variable part is the Compose provider used by `podman compose`; therefore the provider/version is an explicit host prerequisite and must be tested rather than assumed.

### Single Compose definition decision

Keep one repository-standard `compose.yaml` and make it work through:

```text
compose.yaml
    |
    +--> podman compose up -d --build --wait
```

Do not add `podman-compose.yml` or keep a second Docker-specific stack.

The existing Dockerfile can remain named `Dockerfile`; OCI/Podman builds accept it and there is no benefit in renaming it to `Containerfile` merely to signal the runtime change.

---

## 3. Current persistence/workspace architecture

### Request identity today

`src/vg2c_ui/app.py` has HTTP middleware that, for every `/api/*` request except `/api/health`:

1. reads the `vg2c_workspace` cookie;
2. asks `WorkspaceManager.resolve_or_create()` for an anonymous random workspace;
3. places the resulting `Workspace` on `request.state`;
4. constructs `DocumentStore(workspace.root, expose_relative_paths=True)` and places it on `request.state`;
5. renews the workspace cookie on the response.

The browser cookie is therefore the current identity mechanism. There is no authenticated user model.

### `WorkspaceManager` today

`src/vg2c_ui/services/workspaces.py` already owns the filesystem operations that should continue to define the security boundary:

- workspace root creation;
- safe relative-path validation;
- symlink/out-of-root rejection;
- upload extension allow-list;
- exclusive-create upload semantics;
- per-file upload limit;
- file-count limit;
- total workspace-byte limit;
- listing under the workspace root;
- sidecar filtering from user-visible lists;
- file resolution under a single root.

The parts that are anonymous-session specific are:

- random UUID-like workspace generation;
- cookie validation;
- inactivity-based workspace `touch()`;
- 24-hour retention cleanup.

Those parts should be removed/replaced rather than duplicated.

### `DocumentStore` today

`src/vg2c_ui/services/document_store.py` is already the correct compiler/editor persistence boundary.

Every document operation is rooted at one constructor-supplied workspace path. `_resolve()` refuses a path outside that root. Translation, generated output, sidecars, editing, projection, SQL inspection/actions, and CSV preview therefore all inherit the same filesystem containment rule.

This class should remain filesystem-based and should not become Supabase-aware.

### Current durable/temporary data split

Current workspace contents:

```text
/data/workspaces/<anonymous-id>/
  inputs/...
  generated/...
  generated/*.vg2c-ui.json   # sidecar state where applicable
```

The current archive API creates a ZIP with `tempfile.NamedTemporaryFile`, streams it in the response, and deletes it with a background task. That is temporary processing data and should remain outside durable user storage.

---

## 4. Authentication target architecture

### Minimum local services

Use only:

```text
supabase-db     local PostgreSQL used by Supabase Auth
supabase-auth   Supabase Auth / GoTrue
vg2c-ui         existing FastAPI + React application
```

No cloud endpoints participate in normal operation.

### Why direct Supabase Auth instead of the full Supabase gateway stack

Current Supabase Auth documentation explicitly describes GoTrue as a self-standing service, and the official Auth package supports direct clients. The application needs only authentication, not PostgREST, Storage, Realtime, Studio, Functions, or a database API.

Therefore:

- frontend uses official `@supabase/auth-js` `AuthClient` directly against the local Auth service;
- backend validates a Bearer access token by calling local Auth's authenticated `/user` endpoint;
- Auth persists users/sessions/refresh tokens in local Postgres;
- the application persists user files in its existing local filesystem boundary.

This is materially smaller than embedding the entire self-hosted Supabase distribution and remains based on official Supabase Auth behavior.

### Request flow

```text
1. Browser -> local Auth: signUp/signInWithPassword
2. Auth -> local Postgres: users/sessions
3. Browser receives access + refresh session
4. auth-js persists/refreshes session
5. Browser -> FastAPI: Authorization: Bearer <access token>
6. FastAPI -> local Auth /user: validate token + resolve canonical user UUID
7. FastAPI derives /data/users/<user UUID>/
8. Existing WorkspaceManager + DocumentStore operate only inside that root
```

### Failure behavior

Protected application APIs fail closed:

- missing Bearer token -> `401`;
- malformed/invalid/expired token rejected by Auth -> `401`;
- local Auth unreachable/timeout/5xx -> `503`, not `401` and not anonymous fallback;
- workspace filesystem failure -> appropriate `5xx`/`4xx`, never fallback to another user's directory.

The app must never continue with a stale client-supplied user ID if Auth verification fails.

---

## 5. Local Supabase services

### 5.1 PostgreSQL

Purpose:

- persistent backing store for Supabase Auth users, identities, sessions, refresh tokens, and Auth migrations.

Recommended initial implementation:

- a pinned PostgreSQL 17 image compatible with the pinned Supabase Auth release;
- one named volume, e.g. `supabase-db-data`, for `/var/lib/postgresql/data`;
- DB port not published to the Windows/LAN host;
- a long random local password in `.env`, never committed;
- `pg_isready` healthcheck.

For this auth-only stack, do not import all of Supabase's DB initialization scripts for Realtime, Storage, PostgREST, Supavisor, Analytics, etc. They are not used. Auth performs its own migrations automatically when it starts.

Using the standard Postgres image is acceptable because Supabase Auth's upstream self-host documentation requires PostgreSQL, not the rest of the Supabase database extensions for basic email/password Auth. Pin the image rather than using `latest`.

### 5.2 Supabase Auth / GoTrue

Use a pinned `supabase/gotrue` image. At the time of review, the current official self-host Compose source uses `supabase/gotrue:v2.189.0`; implementation should re-check the official self-host release/changelog and pin the selected tested version.

Required behavior/configuration:

- listen on `0.0.0.0:9999` inside the container;
- publish `${SUPABASE_AUTH_PORT:-9999}:9999` so the LAN browser can reach it;
- connect only to the internal DB service name;
- email/password signup enabled;
- email autoconfirm enabled for this local milestone;
- anonymous Supabase users disabled;
- phone signup disabled;
- external OAuth providers disabled;
- no external SMTP dependency;
- access-token lifetime explicit, e.g. 3600 seconds;
- long random JWT signing secret in `.env`;
- Auth `SITE_URL` points at the app URL;
- Auth `API_EXTERNAL_URL` points at the **direct Auth public URL**, not `/auth/v1`, because this plan intentionally does not place an API gateway in front of Auth;
- healthcheck on `http://localhost:9999/health`.

Relevant environment variables include the current GoTrue forms such as:

```text
GOTRUE_API_HOST
GOTRUE_API_PORT
API_EXTERNAL_URL
GOTRUE_DB_DRIVER
GOTRUE_DB_DATABASE_URL
GOTRUE_SITE_URL
GOTRUE_URI_ALLOW_LIST
GOTRUE_DISABLE_SIGNUP
GOTRUE_JWT_SECRET
GOTRUE_JWT_EXP
GOTRUE_JWT_AUD
GOTRUE_JWT_DEFAULT_GROUP_NAME
GOTRUE_EXTERNAL_EMAIL_ENABLED
GOTRUE_MAILER_AUTOCONFIRM
GOTRUE_EXTERNAL_ANONYMOUS_USERS_ENABLED
GOTRUE_EXTERNAL_PHONE_ENABLED
```

Do not configure social providers, phone/SMS, hooks, SMTP, MFA, SAML, or passkeys in this milestone.

### 5.3 Services explicitly not required

Do not start these unless a future feature specifically requires them:

- Supabase Storage
- PostgREST
- Realtime
- Studio
- postgres-meta
- Edge Runtime / Functions
- imgproxy
- Supavisor
- Kong/Envoy API gateway
- Analytics/Logflare/Vector

The official self-host guide explicitly permits removing unused services such as Storage, Realtime, imgproxy, and Functions. Using standalone Auth goes further by avoiding service dependencies the application does not consume.

### 5.4 Supabase CLI

Not required for normal operation or implementation of this containerized self-hosted Auth stack.

Do not add the Supabase CLI as a host prerequisite just to start Auth/Postgres. Database/Auth startup is handled by the containers themselves.

It may be used optionally by a developer for experimentation, but repository scripts and documented operation must not depend on it.

---

## 6. Per-user ownership and isolation

### Canonical ownership identity

The owner is the UUID returned by local Supabase Auth for the validated access token.

The backend must convert/validate it as a UUID and derive the workspace root itself:

```text
/data/users/<canonical-auth-user-uuid>/
```

Example:

```text
/data/users/6d4b8ec2-4dd9-4b31-9f44-d495f822cfad/
  inputs/
  generated/
```

No API request body, URL, query, header, cookie, uploaded filename, or client state may specify the user root or another user's UUID.

### Workspace model

Keep one logical workspace per authenticated user for this milestone.

Refactor `WorkspaceManager.resolve_or_create(cookie_id)` into a user-bound method such as:

```python
resolve_user(user_id: UUID) -> Workspace
```

The returned `Workspace.id` may be the canonical string UUID, but the critical field is `Workspace.root`, which is derived only from the validated identity.

### Required path guarantees

Retain and strengthen existing checks:

- reject absolute paths;
- reject `.` and `..` components;
- normalize Windows separators to forward slash before `PurePosixPath` validation;
- resolve target paths before use;
- require the resolved target to remain beneath the current authenticated user's root;
- reject symlink traversal;
- never follow a path into another user directory;
- keep sidecars hidden from normal listing;
- use exclusive creation for uploads unless a future explicit overwrite endpoint exists.

### Important authorization property

Knowing another user's UUID or relative file path must not help an attacker. Direct-ID/guessed-path tests must still resolve only below the attacker's own user root.

There should be no endpoint equivalent to:

```text
/api/users/{user_id}/files/...
```

for this milestone.

---

## 7. Database schema and RLS

### Application data

No new application tables are required for the target architecture.

Supabase Auth owns its internal Auth schema/tables and migrations. The application's files, generated Python, and sidecars remain filesystem data.

### RLS decision

No application RLS policies are required because no application data is exposed through PostgREST/SQL APIs.

RLS would be the correct boundary if a future feature adds shared relational metadata or Supabase Storage, but adding tables/policies now would duplicate the ownership already represented by the backend-derived filesystem root.

This is deliberate, not an omission.

### Database exposure

Do not publish Postgres to the LAN host by default. Only Auth should reach it over the internal Compose network.

The FastAPI application does not need DB credentials and should not receive them.

---

## 8. Storage design

### 8.1 User-owned persisted data

Durable user-owned data stays under:

```text
/data/users/<user UUID>/
```

Subdirectories preserve the current concepts:

```text
inputs/       uploaded VG2/source/data files
generated/    translated Python and generated output
```

Sidecar state remains adjacent to generated output using the existing `.vg2c-ui.json` mechanism.

### 8.2 Temporary processing data

Keep transient archive files and similar response-generation artifacts under the container's `/tmp` tmpfs.

Do not place temporary ZIPs in a shared persistent data directory.

If future translation steps require scratch files, use either:

- `/tmp` for request-scoped scratch data that need not survive restart; or
- a clearly named temporary directory beneath the authenticated user's root only when the operation must be user-scoped and recoverable.

### 8.3 Shared application/configuration resources

These are not user data:

- application source/static assets inside the image;
- environment configuration;
- build credential secret on the host;
- Supabase Auth/Postgres configuration;
- Postgres named volume containing Auth state.

User APIs must never enumerate these.

### 8.4 Why Supabase Storage is deferred

The compiler and `DocumentStore` require local `Path`s. If Supabase Storage became durable source of truth, implementation would need upload/download synchronization, dirty tracking, conflict handling, restart reconciliation, and a local cache policy.

That is additional statefulness and an additional authorization surface with no present requirement for object URLs, remote object APIs, multi-node application servers, or RLS-backed browser-direct uploads.

If those requirements appear later, Storage can be evaluated as a separate migration.

---

## 9. Backend authentication flow

### 9.1 Add a small Auth adapter

Create a focused module, for example:

```text
src/vg2c_ui/services/auth.py
```

Responsibilities:

- parse/require the Bearer header;
- call the internal local Auth URL's `/user` endpoint with that Bearer token;
- map successful response to a small immutable user object;
- validate/canonicalize the returned UUID;
- distinguish invalid credentials (`401`) from Auth availability failure (`503`);
- use short explicit connect/read timeouts;
- never accept a user ID from the client.

Do not create a general Supabase service/repository layer.

Recommended runtime dependency:

```text
httpx
```

Reason: FastAPI middleware is asynchronous, and `httpx.AsyncClient` provides a small standard async HTTP client with explicit timeout/error handling. Do not install the full Supabase Python client solely to call one Auth endpoint.

Create one reusable `AsyncClient` during app lifespan rather than one connection pool per request.

### 9.2 Authentication middleware

Replace the current anonymous workspace-cookie middleware with a single protected-API middleware.

Public allow-list:

```text
/api/health
/api/ready
/api/config
/static/application routes
```

For every other `/api/*` request:

1. require Bearer token;
2. resolve current user through `AuthService`;
3. resolve that user's workspace through `WorkspaceManager`;
4. set `request.state.current_user`;
5. set `request.state.workspace`;
6. set `request.state.document_store = DocumentStore(workspace.root, expose_relative_paths=True)`;
7. invoke the route.

Do not set/renew `vg2c_workspace`.

### 9.3 Auth availability behavior

- Auth returns 401/403 for token -> application returns 401.
- Auth connection timeout/refused/5xx -> application returns 503 with a generic message.
- Never convert Auth unavailability into an anonymous workspace.
- Log operational Auth failures without logging access/refresh tokens.

### 9.4 Health endpoints

Keep `/api/health` as application-process liveness and do not make it depend on Auth.

Add `/api/ready` for dependency readiness:

- confirms local Auth health endpoint is reachable within a short timeout;
- returns non-2xx when Auth is unavailable.

Compose service ordering uses the Auth container's healthcheck directly; `/api/ready` is for operator/runtime verification.

---

## 10. Frontend/session flow

### 10.1 Use official standalone Auth client

Add the current compatible pinned `@supabase/auth-js` package to the frontend.

Current upstream documentation supports:

```ts
import { AuthClient } from '@supabase/auth-js'
const auth = new AuthClient({ url: 'http://localhost:9999' })
```

This is a better fit than `@supabase/supabase-js` because the application intentionally does not expose PostgREST, Storage, Realtime, or a Supabase API gateway.

Use exactly one `AuthClient` instance in the browser.

### 10.2 Runtime browser-safe config

Add:

```text
GET /api/config
```

Return only public configuration, initially:

```json
{
  "auth_url": "http://<host>:9999"
}
```

The URL comes from an application environment variable such as `VG2C_AUTH_PUBLIC_URL`.

Do not bake `localhost` into the Vite build because LAN clients would interpret it as their own machine.

No DB password/JWT signing secret is ever exposed by this endpoint.

### 10.3 Auth UI

Add a small `AuthGate`/auth hook rather than a routing framework.

Signed-out state:

- email field;
- password field;
- Sign in;
- Sign up;
- clear validation/error state.

Signed-in state:

- render existing `App`;
- show current email or minimal account indicator if useful;
- Sign out action.

### 10.4 Session lifecycle

Use Auth client's standard methods/events:

- `signUp()`;
- `signInWithPassword()`;
- `signOut()`;
- initial session restoration;
- `onAuthStateChange`;
- built-in persisted session + auto refresh;
- `refreshSession()` only for an explicit one-time retry path if a protected API request returns 401 due to token timing.

Do not implement refresh-token storage manually.

### 10.5 Centralize API authorization

Refactor `frontend/src/api.ts` so every protected API call goes through one `authorizedFetch()` helper.

Responsibilities:

1. obtain current Auth session;
2. require an access token;
3. add `Authorization: Bearer <token>`;
4. preserve current JSON/FormData behavior;
5. preserve one error-parsing implementation;
6. optionally perform exactly one standard `refreshSession()` + retry after an authentication failure, then fail/sign out rather than loop.

`useWorkspace.ts` should not acquire auth responsibilities; it should continue to call API functions.

### 10.6 Authenticated downloads

Current generated-file and archive downloads use raw anchors, which cannot attach a Bearer header.

Replace them with API helpers that:

1. authenticated-fetch the file/archive;
2. read a `Blob`;
3. create a temporary object URL;
4. trigger download;
5. revoke the object URL.

Never put access tokens in query strings.

---

## 11. Migration from unauthenticated state

### Clean cutover

After authentication ships:

- remove the `vg2c_workspace` cookie behavior;
- remove anonymous workspace creation;
- remove retention cleanup for authenticated durable user roots;
- require authentication for every user-data API;
- do not run anonymous and authenticated storage modes side-by-side.

### Existing data

Existing directories under:

```text
/data/workspaces/<anonymous-random-id>/
```

have no trustworthy mapping to a Supabase user. They must **not** be automatically claimed by whichever account logs in first.

Recommended migration:

1. before cutover, move/rename the old tree to a non-served location such as:
   ```text
   /data/legacy-workspaces/
   ```
2. runtime code never searches or serves this directory;
3. if specific old data must be preserved, use an explicit one-time operator migration that takes both:
   - a legacy workspace ID;
   - a target authenticated user UUID;
4. validate both paths, require destination does not already conflict, copy/move into `/data/users/<uuid>/`, then record/log what was migrated;
5. remove the migration tool/path after the transition if it is not needed operationally.

No compatibility lookup from old cookies should remain in production code.

### Retention semantics change

The current 24-hour inactivity cleanup must be removed for authenticated user data. Authentication implies persistent ownership; silently deleting user files after 24 hours would violate expected persistence.

Existing quota controls remain useful and should become per-user quotas.

---

## 12. Podman feasibility assessment

### Conclusion

Podman can cleanly become the primary runtime for this repository, with one important qualification: `podman compose` delegates to an external Compose provider, so exact compatibility depends on the provider/version.

Current official facts relevant to this project:

- Podman 6.x runs containers/builds on Windows through Podman Machine.
- Podman Desktop 1.29 added Podman 6.0 integration.
- On Windows, Podman requires a Linux VM through WSL2 or Hyper-V.
- `podman compose` is a wrapper around an external provider such as Docker Compose or `podman-compose`.
- `podman-compose` 1.6.0 added nested interpolation, build-secret environment support, `--wait`, start interval support, and multiple compatibility fixes.
- The current official Supabase self-host Compose source itself contains Podman-specific comments and states that nested interpolation needs `podman-compose >= 1.6.0`.
- Podman supports build secrets used by Dockerfile `RUN --mount=type=secret`.
- Podman supports `no-new-privileges`, `--init`, bind/named volumes, tmpfs, healthchecks, and service networking/DNS.

The proposed auth-only stack is considerably less likely to encounter Compose incompatibility than importing the full Supabase stack.

### Recommended runtime baseline

For a new Windows setup:

- Podman Engine 6.x, preferably the current patched 6.x build shipped/supported by the current Podman Desktop release;
- Podman Desktop 1.29 or newer if a GUI/onboarding workflow is desired;
- if `podman-compose` is the provider, require `podman-compose >= 1.6.0`;
- verify the active provider with `podman compose --help` / startup warning and explicitly pin/configure it if necessary.

Do not rely on whatever provider happens to win PATH precedence without documenting it.

### Rootless recommendation

Use a rootless Podman machine unless an actual incompatibility is found.

This stack does not need privileged ports, device access, Kind, or host-level capabilities. Rootful mode would broaden privileges without a requirement.

Note: Podman CLI machine creation defaults to rootless, while Podman Desktop UI defaults can differ. Phase 2 must explicitly verify the connection is rootless.

### Windows/VPN networking

On Windows/WSL, user-mode networking is particularly relevant when container builds must reach corporate/VPN-only package/Git services. Podman Desktop documents it as required for resources behind some VPN configurations.

Because this repository builds against Intel-internal Git/PyPI services, Phase 2 should enable/test user-mode networking when normal WSL networking cannot reach those endpoints.

---

## 13. Compose/Podman changes

### Target `compose.yaml`

Keep one Compose-compatible file containing approximately:

```text
services:
  supabase-db:
    image: <pinned postgres>
    volume: supabase-db-data
    healthcheck: pg_isready

  supabase-auth:
    image: <pinned supabase/gotrue>
    depends_on:
      supabase-db: service_healthy
    ports:
      - ${SUPABASE_AUTH_PORT:-9999}:9999
    environment: auth configuration
    healthcheck: /health

  vg2c-ui:
    build: existing Dockerfile + build secret
    depends_on:
      supabase-auth: service_healthy
    ports:
      - ${APP_PORT:-8765}:8765
    environment:
      VG2C_AUTH_INTERNAL_URL=http://supabase-auth:9999
      VG2C_AUTH_PUBLIC_URL=${VG2C_AUTH_PUBLIC_URL}
      existing quota values
    volumes:
      - ./data:/data
    existing hardening/healthcheck where provider-compatible

volumes:
  supabase-db-data:

secrets:
  git_credentials:
    file: ${VG2C_GIT_CREDENTIALS_FILE:-./secrets/git-credentials}
```

### Service DNS

FastAPI uses `http://supabase-auth:9999` over the Compose default network. Podman's Netavark/Aardvark DNS registers container/service aliases on DNS-enabled networks, so no fixed container IPs should be used.

### Persistence

- Postgres Auth state -> named Podman volume.
- Application user data -> existing `./data:/data` bind mount.
- App `/tmp` -> tmpfs.

A named volume is preferred for Postgres on Windows because it remains inside the Linux VM/container storage rather than placing PostgreSQL's live data directory on a Windows bind mount.

### Build secret

Retain the current file-backed `git_credentials` build secret and Dockerfile `RUN --mount=type=secret`.

`podman build` supports build secrets, and `podman-compose` 1.6.0 handles file-backed build secrets. Do not convert the credential into an image ARG or environment variable.

### Compose features to validate in Phase 2

Run a smoke matrix for:

- `podman compose config`;
- `podman compose build vg2c-ui` with the secret;
- `podman compose up -d --wait`;
- `depends_on` health order;
- `read_only`;
- `/tmp` tmpfs;
- `cap_drop`;
- `no-new-privileges`;
- `init`;
- restart policy;
- `./data` write access by UID 10001;
- named-volume persistence;
- service-name DNS.

Only add a Podman-specific workaround if one of these concrete tests fails.

### Avoid Compose `include`

The previous Supabase plan proposed embedding the full official stack with Compose `include`. The reviewed design no longer needs it because only two local Supabase services are required.

Avoiding `include` removes a provider-sensitive feature and keeps the stack easier to reason about.

---

## 14. Host-machine prerequisites

This section distinguishes repository dependencies from Windows host setup.

### 14.1 Podman Engine — mandatory for Podman workflow

What: OCI container engine.

Why: builds and runs `vg2c-ui`, Supabase Auth, and Postgres.

Recommended source: official Podman/Podman Desktop distribution.

Recommended compatibility: Podman 6.x current patched release; keep host client and Podman Machine engine compatible.

Verify:

```powershell
podman version
podman info
```

Admin/restart: installation itself may not require permanent admin use, but enabling Windows virtualization features may.

### 14.2 Podman Machine — mandatory on Windows

What: Linux VM that actually runs Podman containers.

Why: Podman containers are Linux workloads; Windows client connects to the VM.

Recommended provider: WSL2 by default; Hyper-V only for a concrete environment reason.

Verify:

```powershell
podman machine list
podman system connection list
```

Create/start:

```powershell
podman machine init
podman machine start
```

Do not create rootful unless required.

### 14.3 WSL2 — mandatory when using WSL provider

What: Windows Subsystem for Linux 2 virtualization backend.

Why: hosts the default Podman Machine on Windows.

Current Podman Desktop requirements include supported 64-bit Windows, admin rights to enable WSL, and restart after feature enablement.

Verify:

```powershell
wsl --status
wsl --version
```

Typical enable/update commands from official Podman Desktop guidance:

```powershell
wsl --update
wsl --install --no-distribution
```

A Windows restart may be required.

Important version note: Podman 6.0 dropped Windows 10 support upstream. On a Windows 10 machine, use a currently supported Podman 5.8 LTS-compatible path instead of blindly installing 6.x. Phase 2 must check the actual Windows version first.

### 14.4 Hyper-V — optional alternative

Mandatory only if Hyper-V is chosen instead of WSL2.

Requires supported Windows Pro/Enterprise edition and admin rights to enable the feature; restart required.

Verify:

```powershell
Get-Service vmcompute
systeminfo
```

Do not enable Hyper-V just because Podman supports it if WSL2 works.

### 14.5 Podman Desktop — optional but recommended on Windows

What: GUI/onboarding and Podman Machine/Compose management.

Why: simplifies installing/configuring Podman, machines, Compose CLI, and certificate/network settings.

Not required for headless CLI operation.

Recommended source: official Podman Desktop.

Recommended version: 1.29+ for Podman 6.0-aware setup; use the current stable release at installation time.

### 14.6 Compose provider — mandatory

`podman compose` needs an external provider.

Preferred approach: use the Compose CLI installed/configured by Podman Desktop, then verify exactly which provider is active.

If choosing `podman-compose`, require >= 1.6.0.

Verify:

```powershell
podman compose --help
podman compose version
podman compose config
```

Optional diagnostic:

```powershell
$env:PODMAN_COMPOSE_PROVIDER
```

If multiple providers are installed, explicitly configure `PODMAN_COMPOSE_PROVIDER` or Podman `containers.conf` rather than depending on implicit precedence.

### 14.7 Git — mandatory for local-agent repository work

What: source control.

Why: pull reviewed repository state and run implementation workflow.

Recommended source: Git for Windows/current organization-approved distribution.

Verify:

```powershell
git --version
git status
git remote -v
```

### 14.8 Node.js — not mandatory for normal container operation

The container build stage already provides Node 22.

Host Node is needed only when the local agent intentionally runs frontend tests/builds outside containers.

If installed for native checks, use Node >=22 per `frontend/package.json`.

Verify:

```powershell
node --version
npm --version
```

### 14.9 Python / uv — not mandatory for normal container operation

The container build includes Python 3.13 and `uv`.

Host Python is useful for native repository tests and the current `start-vg2c-ui.bat` workflow, but Podman deployment does not require a separately installed Python if all tests/builds are containerized.

Repository supports Python >=3.11. Existing local `.venv` can be reused for native tests.

Verify if used:

```powershell
python --version
uv --version
```

### 14.10 Supabase CLI — optional, not required

No install required for this plan.

### 14.11 Corporate network / CA / proxy — mandatory for building this repository in the target environment

The builder must reach the private Git dependency and internal package index.

Phase 2 must verify:

- Intel internal Git hostname resolves/reaches from Podman Machine;
- internal Python index resolves/reaches;
- corporate CA is trusted in the build path;
- proxy variables, if required, reach Podman build;
- `secrets/git-credentials` exists and is readable only as appropriate.

Podman Desktop 1.29 supports CA certificate synchronization, but actual corporate policy/certificate behavior must be tested in the target machine.

### 14.12 Filesystem sharing

WSL2 exposes Windows drives under `/mnt/<drive>`; Podman Machine handles host mounts through its VM integration.

Verify the repository's `./data` bind mount actually persists to the intended Windows checkout directory.

Do not add manual machine volume sharing unless it is actually required; official Podman Machine documentation notes that WSL already mounts Windows drives.

---

## 15. Repository changes by module

### `compose.yaml`

- Add `supabase-db`.
- Add `supabase-auth`.
- Add DB named volume.
- Add healthchecks and dependency order.
- Add app internal/public Auth URLs.
- Remove anonymous workspace retention/cleanup environment variables.
- Keep quota environment variables.
- Keep existing hardening/build secret unless a verified provider issue requires a minimal change.

### `.env.docker.example`

Replace/rename with a runtime-neutral example, preferably:

```text
.env.example
```

Include placeholders for:

```text
APP_PORT
SUPABASE_AUTH_PORT
VG2C_AUTH_PUBLIC_URL
POSTGRES_PASSWORD
GOTRUE_JWT_SECRET
VG2C_GIT_CREDENTIALS_FILE
VG2C_MAX_UPLOAD_BYTES
VG2C_MAX_FILE_COUNT
VG2C_MAX_WORKSPACE_BYTES
```

Remove:

```text
VG2C_WORKSPACE_RETENTION_SECONDS
VG2C_CLEANUP_INTERVAL_SECONDS
VG2C_COOKIE_SECURE
```

Do not commit populated secrets.

### `Dockerfile`

Expected to remain almost unchanged.

- keep `Dockerfile` name;
- keep file-backed build secret;
- keep non-root app runtime;
- keep frontend/build stages;
- do not add Supabase tooling to the app image.

If Podman reports a concrete parser/build-secret incompatibility, fix only that incompatibility and preserve secret non-persistence.

### `src/vg2c_ui/services/auth.py` — new

- small `AuthenticatedUser` value object;
- `AuthService` using reusable async HTTP client;
- bearer validation/current-user lookup;
- UUID canonicalization;
- clean invalid-token vs unavailable exceptions.

### `src/vg2c_ui/app.py`

- add app lifespan for Auth HTTP client if appropriate;
- replace anonymous cookie middleware with protected API auth middleware;
- derive user workspace from Auth identity;
- add `/api/config`;
- add `/api/ready`;
- keep `/api/health` simple;
- stop issuing `vg2c_workspace` cookie.

### `src/vg2c_ui/services/workspaces.py`

Refactor existing class instead of adding a parallel authenticated manager:

- root becomes `/data/users` from app setup;
- `resolve_user(UUID)` creates/returns that user's root;
- retain safe path/upload/list/quota logic;
- remove random anonymous ID creation;
- remove cookie name;
- remove retention/touch/cleanup logic;
- preserve `inputs/` and `generated/` creation;
- canonicalize UUID internally before joining path.

Optional naming cleanup: keep `WorkspaceManager` because it still manages a user's application workspace; no rename is needed unless the implementation becomes materially clearer.

### `src/vg2c_ui/services/document_store.py`

Keep semantics unchanged.

Possible cleanup:

- remove the legacy fallback to `request.app.state.document_store` once all HTTP routes are guaranteed to use request-scoped stores;
- preserve a direct constructor for unit tests rather than maintaining a production fallback global store.

Do not add authentication code here.

### `src/vg2c_ui/api/workspace.py`

No ownership parameter should be added.

Existing `get_workspace(request)` continues to return the request-scoped authenticated workspace.

Add explicit deletion/update endpoints only if they already exist as requirements; do not invent them solely for auth. If delete/overwrite behavior is added as part of acceptance testing, it must use the same manager/root boundary.

### Documents/translation/changes/SQL APIs

Expected logic remains unchanged because they already obtain `DocumentStore` from request scope.

Regression tests must prove they cannot escape the authenticated user's root.

### `frontend/src/auth.ts` or equivalent — new

- fetch `/api/config`;
- create exactly one `AuthClient`;
- expose auth/session subscription primitives.

### `frontend/src/AuthGate.tsx` or equivalent — new

- initial session loading;
- signup/signin form;
- signed-in app rendering;
- signout;
- no routing framework.

### `frontend/src/api.ts`

- one `authorizedFetch` path;
- Bearer header injection;
- optional one-time refresh/retry;
- authenticated upload/list/JSON calls;
- authenticated blob downloads/archive.

### `frontend/src/App.tsx`

- remove raw protected `<a href>` downloads;
- use download actions/helpers;
- expose logout/account indicator through Auth wrapper or small prop/context;
- existing editor behavior otherwise unchanged.

### `frontend/package.json` / lockfile

- add pinned compatible `@supabase/auth-js`;
- do not add full `@supabase/supabase-js` unless a later requirement needs other Supabase products.

### Tests

Update anonymous isolation tests to authenticated-user-root tests; add auth middleware tests and two-user attack tests.

### Documentation

Replace Docker-specific operation docs with Podman-primary docs while keeping Compose terminology runtime-neutral.

Recommended document:

```text
docs/podman-lan.md
```

Remove `docs/docker-lan.md` after equivalent information is preserved; do not document two primary workflows.

`start-vg2c-ui.bat` is a native development convenience, not container deployment. Either:

- keep it explicitly documented as native unaffiliated development only if it is adapted to authentication dependencies; or
- remove it if it can no longer start a functional authenticated app by itself.

Do not leave a launcher that starts an unusable half-stack.

---

## 16. Code/configuration to refactor or remove

Remove after cutover:

- `vg2c_workspace` cookie.
- `WorkspaceManager.cookie_name`.
- `_valid_workspace_id()` for anonymous 32-char IDs.
- `resolve_or_create(cookie_workspace_id)`.
- anonymous UUID generation.
- `touch()` based on session activity.
- `maybe_cleanup()` and retention scheduler.
- `VG2C_WORKSPACE_RETENTION_SECONDS`.
- `VG2C_CLEANUP_INTERVAL_SECONDS`.
- `VG2C_COOKIE_SECURE`.
- browser-session isolation documentation.
- old anonymous-session tests after replacing their security intent with user-isolation tests.
- full Supabase Storage/cache design in `SUPABASE_AUTH_STORAGE_PLAN.md`; this reviewed plan supersedes it.
- Docker-only commands/docs when Podman acceptance is complete.

Do not add:

- storage repository interfaces;
- object-cache synchronization;
- user-profile tables;
- per-user buckets;
- client-provided ownership fields;
- separate Docker and Podman compose files;
- reverse-proxy/gateway service solely to make Auth look like a full Supabase project.

---

## 17. Implementation stages

### Stage A — baseline and tests before refactor

1. `git pull --ff-only` on local machine/implementation environment.
2. record current `main` SHA.
3. ensure worktree is understood/clean enough for controlled edits.
4. run existing Python test suite.
5. run frontend `npm test`/typecheck/build if host dependencies are available.
6. add/retain characterization tests for current `WorkspaceManager` path containment and quotas.

### Stage B — user-bound persistence refactor without frontend Auth yet

1. Refactor `WorkspaceManager` to resolve a supplied validated UUID.
2. remove anonymous retention logic.
3. update unit tests for two explicit users.
4. verify `DocumentStore` behavior unchanged.
5. add traversal/symlink/guessed-path tests.

This isolates persistence correctness from Auth networking.

### Stage C — backend Auth boundary

1. Add `httpx` UI/runtime dependency.
2. Add `AuthService`.
3. replace cookie middleware with Bearer/Auth middleware.
4. add `/api/config` and `/api/ready`.
5. add mocked Auth tests for valid/invalid/expired/unavailable cases.
6. verify no endpoint accepts user ID.

### Stage D — frontend Auth/session integration

1. add `@supabase/auth-js`.
2. create singleton Auth client from runtime config.
3. add `AuthGate`.
4. add sign up/sign in/sign out/session restoration.
5. refactor `api.ts` to inject access token.
6. convert protected downloads to authenticated blob fetch.
7. update UI tests/typecheck/build.

### Stage E — Compose auth-only Supabase stack

1. add DB and Auth services to the existing `compose.yaml`.
2. add healthchecks/dependency order.
3. add `.env.example` secrets/config.
4. pin images.
5. keep existing app build hardening/secret.
6. run Compose syntax/static validation available in implementation environment.

### Stage F — legacy migration/docs cleanup

1. define/move `legacy-workspaces` out of runtime lookup.
2. add one-time explicit migration procedure only if data preservation is required.
3. remove anonymous compatibility code.
4. replace Docker-primary documentation with Podman-primary documentation.
5. remove/retire launchers/docs that cannot start the authenticated stack.

### Stage G — Phase 2 host validation

Execute the local-agent handoff in section 21.

---

## 18. Migrations, startup, and health checks

### Fresh startup order

```text
1. supabase-db starts
2. DB healthcheck passes
3. supabase-auth starts
4. Auth automatically applies Auth migrations
5. Auth /health passes
6. vg2c-ui starts
7. app /api/health passes
8. app /api/ready confirms Auth reachable
```

### Restart behavior

- Postgres named volume preserves users/sessions/Auth state.
- `./data` preserves per-user application files.
- app restart does not remap ownership; roots are UUID-derived.
- Auth restart may briefly cause protected app requests to return 503.
- browser access tokens remain client-side; refresh resumes when Auth returns.

### Database migrations

Use Auth's upstream automatic migrations for its own schema.

Do not create an application migration framework when there are no application DB tables.

If the pinned Auth version changes later, treat that as an explicit container dependency upgrade with release-note review and backup/restart testing.

### Healthchecks

DB:

```text
pg_isready
```

Auth:

```text
GET http://localhost:9999/health
```

App liveness:

```text
GET http://localhost:8765/api/health
```

App readiness:

```text
GET http://localhost:8765/api/ready
```

Do not expose DB health publicly through credentials/details.

---

## 19. Testing strategy

### 19.1 Unit tests — persistence

For two explicit UUIDs A and B:

- `resolve_user(A)` and `resolve_user(B)` produce distinct roots.
- A upload visible only in A listing.
- B upload visible only in B listing.
- traversal `../...` rejected.
- absolute path rejected.
- mixed Windows separators cannot escape.
- symlink file/path escape rejected.
- source, generated file, and sidecar stay below current user's root.
- quotas are applied per user rather than globally.
- existing translation/editor behavior still passes against a user root.

### 19.2 Unit/API tests — authentication

Mock local Auth responses to test:

- valid Bearer token resolves expected UUID;
- missing header -> 401;
- malformed Bearer header -> 401;
- invalid credentials -> 401;
- expired token rejected by Auth -> 401;
- Auth timeout -> 503;
- Auth connection refused -> 503;
- Auth 5xx -> 503;
- `/api/health` remains public;
- `/api/config` remains public and contains no secret values;
- protected API never uses a supplied fake user ID.

### 19.3 Frontend tests

At minimum:

- signed-out gate renders before workspace API requests;
- sign-up flow handles success/error;
- sign-in handles invalid credentials;
- restored session renders app;
- auth-state signout returns to gate;
- authorized API helper adds Bearer header;
- FormData path still works;
- download helper authenticates and revokes object URL;
- refresh/retry occurs at most once if implemented.

Do not add a heavy frontend test framework solely for Auth if current lightweight testing can cover state/helpers. Add a focused tool only if DOM interaction becomes untestable otherwise.

### 19.4 Full integration — authentication

Against real local Auth/Postgres:

- sign up;
- sign in;
- invalid credentials;
- sign out;
- session restoration after browser reload;
- token refresh;
- expired/invalid token;
- unauthenticated API request.

### 19.5 Full integration — strict two-user isolation

Create Users A and B.

A:

1. upload/create resource A;
2. translate/edit/save it;
3. record relative resource paths/document IDs.

B:

1. upload/create resource B;
2. translate/edit/save it.

Verify:

```text
A lists A and never B
B lists B and never A
A opens/downloads/updates/deletes A
B opens/downloads/updates/deletes B
A cannot access B by reusing B relative paths/IDs
B cannot access A by reusing A relative paths/IDs
```

Attack variants:

- direct document reference body from the other user;
- guessed identical relative file name;
- traversal attempts;
- URL-encoded traversal;
- Windows slash/backslash variants;
- generated output path from other user;
- CSV preview path from other user;
- change/apply payload referencing other user's document path;
- SQL inspect/action payload referencing other user's document path;
- archive endpoint contains only caller data;
- file listing cannot enumerate `users/` or another UUID;
- sidecar cannot be fetched through path tricks.

### 19.6 Persistence/restart

- create user A and files;
- stop stack without deleting volumes/data;
- restart stack;
- sign in again;
- A data remains;
- B still cannot access it;
- Auth user persists;
- generated output/sidecar persistence remains valid.

### 19.7 Container/Podman integration

- fresh `podman compose up -d --build --wait`;
- all healthchecks healthy;
- startup order correct;
- service DNS works;
- build secret works and is not present in resulting image/history/filesystem;
- DB named volume persists restart;
- `./data` bind persists restart;
- `podman compose down` then up preserves data;
- `podman compose down -v` is documented as destructive for Auth DB;
- rootless runtime confirmed;
- app remains non-root inside container;
- read-only rootfs/tmpfs behavior works;
- LAN browser reaches app and Auth port.

### 19.8 Regression

Run the existing compiler/editor test suite and explicitly exercise:

- upload folder;
- translate batch;
- open document;
- CSV preview;
- change preview/apply;
- structured SQL inspect/action;
- workspace projection;
- generated Python download;
- workspace ZIP;
- revision conflict behavior;
- path presentation remains relative and does not leak host filesystem paths.

---

## 20. Security acceptance criteria

Implementation is not complete until all are true:

1. No unauthenticated request can use a user-data API.
2. Backend derives ownership exclusively from a token validated by local Auth.
3. No client-provided `user_id` affects filesystem root selection.
4. Every compiler/editor file operation is still constrained by `DocumentStore` to the authenticated workspace.
5. Every upload/list/download/archive operation is constrained by `WorkspaceManager` to the authenticated workspace.
6. User A cannot read B.
7. User A cannot enumerate B.
8. User A cannot update/overwrite B.
9. User A cannot delete B.
10. Guessed IDs/relative paths do not cross ownership boundaries.
11. Path traversal and symlink escape remain rejected.
12. Access/refresh tokens are not placed in URLs, logs, filenames, or persistent server-side user directories.
13. JWT signing secret and DB password are not exposed to the frontend/app container unnecessarily.
14. Postgres is not published to the LAN by default.
15. Auth unavailability fails closed with 503.
16. Signout does not leave the UI able to make authorized workspace calls.
17. Existing legacy anonymous workspaces are not implicitly served to authenticated users.
18. No Supabase Cloud hostname is required for normal startup or operation.
19. No external SMTP/OAuth/Storage dependency is required.
20. Container build credentials are not persisted into image layers.

---

## 21. Phase 2 local-agent handoff

This section is intended to be directly executable by the local machine agent after Phase 1 repository implementation is complete.

### Step 1 — verify Windows and repository state

```powershell
winver
wsl --status
git -C C:\Users\yeuchuan\project\SQLPathFinder_PY_Migration status
git -C C:\Users\yeuchuan\project\SQLPathFinder_PY_Migration switch main
git -C C:\Users\yeuchuan\project\SQLPathFinder_PY_Migration pull --ff-only
git -C C:\Users\yeuchuan\project\SQLPathFinder_PY_Migration log -1 --oneline
```

Stop and report if:

- checkout contains unexpected changes that would be overwritten;
- remote main contains implementation-affecting commits not included in the plan/implementation review;
- Windows version is incompatible with the intended Podman major version.

### Step 2 — install/verify virtualization backend

Prefer WSL2.

If WSL2 is not enabled, from an elevated shell follow current official Podman Desktop guidance:

```powershell
wsl --update
wsl --install --no-distribution
```

Restart Windows if requested, then:

```powershell
wsl --status
```

Do not enable Hyper-V in parallel unless WSL2 is unsuitable and the reason is documented.

### Step 3 — install/verify Podman

Install from the current official Podman/Podman Desktop source.

Verify:

```powershell
podman version
podman info
```

For Windows 11/new setup prefer current patched Podman 6.x. For Windows 10, do not install 6.x because upstream 6.0 dropped Windows 10 support; use the currently supported 5.8 maintenance line/compatible Desktop path.

### Step 4 — initialize Podman Machine

```powershell
podman machine list
```

If none exists:

```powershell
podman machine init
podman machine start
```

Then:

```powershell
podman machine list
podman system connection list
podman info
```

Confirm rootless connection.

Allocate enough VM resources for Postgres + Auth + app build. The full Supabase 4GB minimum is not directly applicable because this plan does not run the full stack, but use a practical machine allocation (for example >=4 GB RAM and multiple CPUs) rather than a minimal VM that makes Node/Python builds unreliable.

### Step 5 — configure VPN/corporate networking if needed

Test internal endpoints from a disposable container/build context.

If WSL/Podman cannot reach resources behind the VPN, enable Podman Machine user-mode networking according to current Podman Desktop guidance, then restart the machine and retest.

Do not change application code to work around a host VPN routing problem.

### Step 6 — verify corporate CA/proxy/build credentials

Ensure repository-local ignored file exists:

```text
secrets/git-credentials
```

Verify permissions according to local policy.

Ensure any required proxy/CA configuration reaches Podman/Buildah.

Run:

```powershell
podman build --help
```

and later the actual Compose build to validate the secret path.

### Step 7 — install/verify Compose provider

Use Podman Desktop onboarding/CLI tools or an explicitly selected provider.

Verify:

```powershell
podman compose --help
podman compose version
```

If provider is `podman-compose`, require >=1.6.0.

If multiple providers exist, set/configure the desired provider explicitly and record it in the validation report.

### Step 8 — create runtime `.env`

From repository root:

1. copy `.env.example` to `.env`;
2. generate strong random `POSTGRES_PASSWORD`;
3. generate strong random `GOTRUE_JWT_SECRET` (at least 32 random characters; prefer substantially more entropy);
4. set `VG2C_AUTH_PUBLIC_URL` to the host's reachable Auth URL, e.g. `http://<LAN-IP>:9999`;
5. set app/Auth ports as needed;
6. verify `.env` remains Git-ignored.

Do not reuse example/default Supabase secrets.

### Step 9 — Compose static validation

```powershell
cd C:\Users\yeuchuan\project\SQLPathFinder_PY_Migration
podman compose config
```

Inspect resolved output for:

- expected services only;
- no accidental cloud URLs;
- no secret values rendered into frontend config;
- correct bind/named volumes;
- correct service dependencies.

### Step 10 — build app image

```powershell
podman compose build vg2c-ui
```

If private dependency fetch fails, diagnose network/CA/credential forwarding first.

Do not replace build secret with plaintext ARG as a workaround.

### Step 11 — start complete stack

```powershell
podman compose up -d --wait
podman compose ps
```

If the selected provider does not support `--wait` despite documented version expectations, start detached and explicitly poll `podman compose ps`/health status; report that provider incompatibility before introducing repository-specific scripts.

### Step 12 — inspect health/logs

```powershell
podman compose ps
podman compose logs supabase-db
podman compose logs supabase-auth
podman compose logs vg2c-ui
```

Verify from host:

```powershell
Invoke-WebRequest http://127.0.0.1:8765/api/health
Invoke-WebRequest http://127.0.0.1:8765/api/ready
Invoke-WebRequest http://127.0.0.1:9999/health
```

### Step 13 — browser Auth test

From the host and an intended LAN client where applicable:

- open app URL;
- verify auth gate appears before workspace calls;
- sign up user A;
- sign out;
- sign in user A;
- reload browser and verify session restoration;
- test invalid password;
- verify signout removes application access.

### Step 14 — two-user isolation test

Use two separate browser profiles/private profiles.

Create A and B and execute the full isolation matrix from section 19.5.

Attempt direct requests with copied relative paths/IDs from the opposite user.

Capture exact HTTP status and verify no foreign file content/metadata is returned.

### Step 15 — restart persistence test

```powershell
podman compose down
podman compose up -d --wait
```

Verify:

- users still sign in;
- A files persist;
- B files persist;
- ownership remains isolated.

Do not use `-v` during normal restart test.

### Step 16 — Podman compatibility report

Record:

```text
Windows version
Podman version
Podman Desktop version (if used)
Podman Machine provider
rootless/rootful
Compose provider + version
user-mode networking enabled/disabled
corporate VPN/CA/proxy notes
all Compose features tested
all failures/workarounds
```

Only after concrete incompatibilities are observed should repository changes be proposed.

---

## 22. Risks and limitations

### Open signup + autoconfirm

Email autoconfirm is what removes SMTP/cloud dependency. It also means anyone who can reach the exposed Auth service can create an account while signup is enabled.

For an internal LAN deployment this may be acceptable for the milestone, but it is a real policy decision. If account creation must be restricted, choose a later explicit mechanism (admin-created accounts, signup disabled after provisioning, allow-list/hook, or real email verification) rather than assuming autoconfirm validates ownership of an email address.

### Plain HTTP on LAN

The current deployment is plain HTTP. Bearer access tokens sent over an untrusted network could be intercepted.

For controlled internal testing this may match current constraints, but production or broader LAN exposure should add TLS/reverse proxy as a separate hardening milestone. Do not expand scope silently.

### Auth availability

Backend token validation calls local Auth per protected request. This is simple and authoritative but makes Auth availability part of the application request path.

The local network hop is cheap. If performance measurements later show it is a bottleneck, evaluate standard local JWT/JWKS verification with proper key caching as a separate optimization. Do not add that complexity preemptively.

### Signout vs already-issued access token

JWT access tokens have a finite lifetime. Server verification through Auth's `/user` endpoint is used because Supabase documents it as an authentic current-user check. Integration tests should verify the exact selected Auth version's behavior for signed-out/revoked sessions and set a reasonable short access-token expiry.

### Podman Compose provider variance

`podman compose` does not itself implement Compose. Provider differences remain a risk. This is why the plan pins/verifies the provider and minimizes advanced Compose features.

### Windows bind mount semantics

The application data bind mount must be tested for permission/performance behavior with the non-root UID 10001 inside Podman Machine. If Windows/WSL mount permissions prevent writes, fix the mount/user mapping in the smallest provider-compatible way rather than making the app container root.

### Corporate network

The private build dependency may fail inside Podman Machine even when it works on Windows host due to VPN routing, corporate proxy, or CA trust. Treat this as host configuration first.

### No multi-node storage

Filesystem persistence assumes one application host. If the service later scales to multiple application hosts, object/shared storage becomes relevant and this storage decision must be revisited.

---

## 23. Deliberately deferred work

- Supabase Storage.
- PostgREST/RLS application tables.
- Supabase Studio.
- Realtime.
- Edge Functions.
- Supavisor.
- Envoy/Kong gateway.
- SMTP/email verification/password-reset delivery.
- OAuth/social login.
- MFA/SSO/passkeys.
- user invitations/admin account management.
- user-to-user sharing.
- multiple named workspaces per user.
- storage quotas persisted in DB/audit billing data.
- multi-host application storage.
- HTTPS/reverse proxy beyond current LAN-testing scope.
- generated-code execution.
- container orchestration beyond Compose.

Each should be introduced only after a concrete requirement appears.

---

## 24. Second self-review and revisions made

The first existing plan (`SUPABASE_AUTH_STORAGE_PLAN.md`) was re-read against the latest repository code and current Supabase/Podman documentation. A second architecture review then rechecked the relevant request/persistence call sites, current Compose file, current Supabase self-host configuration, current Supabase Auth standalone documentation, Podman Machine/Compose behavior, and `podman-compose` release notes.

### Question: Did I misunderstand an existing abstraction?

Revision: yes, the first plan underused the strength of `WorkspaceManager` + `DocumentStore` by treating local files as a cache. The code already has a strong root-scoped filesystem boundary. The reviewed plan keeps it as durable persistence and changes only how the root is selected.

### Question: Am I creating something that already exists?

Revision: removed the proposed Supabase Storage adapter/cache synchronization layer. It duplicated persistence responsibilities already implemented safely in `WorkspaceManager`/`DocumentStore`.

### Question: Is Supabase Storage actually necessary?

Answer: no for the current architecture. There is no browser-direct object-upload requirement, multi-node backend, public object URL requirement, or need for object-store RLS. It is deliberately deferred.

### Question: Can the persistence layer be adapted more simply?

Revision: yes. Replace anonymous random workspace identity with canonical authenticated user UUID and use `/data/users/<uuid>` as the existing root. Remove anonymous retention rather than creating a second manager.

### Question: Is authorization enforced server-side everywhere?

Reviewed result: yes in the target design. One global protected-API middleware authenticates first and injects a root-scoped `DocumentStore`. Workspace APIs still use request-scoped `WorkspaceManager`/`Workspace`. Frontend filtering is never relied on for ownership.

### Question: Is any user ID accepted from the client?

Revision: explicitly prohibited. Backend uses only Auth `/user` response UUID. No ownership field is added to API models.

### Question: Are any Supabase services unnecessary?

Revision: yes. The first plan proposed DB + Auth + Storage + PostgREST + gateway and possibly supporting services. The reviewed minimum is Postgres + Supabase Auth only. This is supported by Supabase Auth's documented self-standing mode and official `@supabase/auth-js` direct client.

### Question: Is any Podman-specific change avoidable?

Revision: yes. Keep ordinary Compose syntax and current Dockerfile. Do not add `x-podman` settings unless a Phase 2 compatibility test proves one is required.

### Question: Are Docker and Podman configurations duplicated?

Reviewed result: no. One `compose.yaml` is the target. Docker-only deployment docs should be retired after Podman acceptance instead of maintained as a second primary workflow.

### Question: Did the original Compose integration overcomplicate Podman compatibility?

Revision: yes. The prior plan proposed Compose `include` for a full Supabase bundle. With an auth-only stack there is no need for `include`, and removing it eliminates a provider-sensitive feature.

### Question: Did I miss a Windows host requirement?

Revision: added explicit WSL2/Hyper-V distinction, Podman Machine, rootless verification, Compose provider/version verification, Windows 10 vs Podman 6 compatibility, VPN user-mode networking, corporate CA/proxy/private dependency checks, and Windows bind-mount verification.

### Question: Are there security gaps in user isolation?

Revisions/checks added:

- no implicit migration/claim of anonymous data;
- no client user ID;
- fail-closed 503 on Auth outage;
- path/symlink tests across all document/SQL/CSV/change APIs;
- archive/list enumeration tests;
- removal of 24-hour deletion semantics for persistent authenticated data;
- Postgres not host-published;
- app does not receive DB/JWT signing secret;
- access tokens never moved into query strings for downloads.

### Question: Is any planned abstraction overengineered?

Revision: use one small `AuthService`, one existing `WorkspaceManager`, and one existing `DocumentStore`. No general Supabase repository, storage adapter, user repository, DB migration framework, reverse proxy, or additional service layer is introduced.

### Final reviewed architecture

```text
local Postgres <--- local Supabase Auth <--- official auth-js browser session
                         ^
                         |
                  token verification
                         |
Browser -------- Bearer token --------> FastAPI
                                          |
                                  validated Auth UUID
                                          |
                                  WorkspaceManager
                                          |
                               /data/users/<uuid>/
                                          |
                                    DocumentStore
                                          |
                              current compiler/editor
```

This is the implementation target unless Phase 2 produces a concrete incompatibility or a new application requirement invalidates one of the stated assumptions.

---

## Official references checked during planning

Supabase:

- https://supabase.com/docs/guides/self-hosting/docker
- https://supabase.com/docs/guides/self-hosting/auth/config
- https://supabase.com/docs/reference/self-hosting-auth
- https://supabase.com/docs/reference/javascript/auth-getuser
- https://supabase.com/docs/guides/auth/jwts
- https://github.com/supabase/auth
- https://github.com/supabase/supabase-js/tree/master/packages/core/auth-js
- https://github.com/supabase/supabase/tree/master/docker
- https://github.com/supabase/supabase/blob/master/docker/CHANGELOG.md

Podman / Compose:

- https://docs.podman.io/en/stable/markdown/podman.1.html
- https://docs.podman.io/en/stable/markdown/podman-compose.1.html
- https://docs.podman.io/en/stable/markdown/podman-machine-init.1.html
- https://docs.podman.io/en/stable/markdown/podman-run.1.html
- https://docs.podman.io/en/stable/markdown/podman-build.1.html
- https://docs.podman.io/en/stable/markdown/podman-network.1.html
- https://podman-desktop.io/docs/installation/windows-install
- https://podman-desktop.io/docs/podman/creating-a-podman-machine
- https://podman-desktop.io/tutorial/getting-started-with-compose
- https://github.com/containers/podman-compose/releases
- https://github.com/compose-spec/compose-spec

Re-check version-sensitive image tags and host-tool versions immediately before implementation/installation.