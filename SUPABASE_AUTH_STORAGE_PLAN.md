# Supabase Authentication and Per-User Storage Implementation Plan

## 1. Scope and invariant

This plan adds local-only, self-hosted Supabase authentication and durable per-user storage to the existing Docker deployment.

Core invariant:

> A user can only access, modify, or list their own stored application data.

The implementation should preserve the current compiler/editor architecture and make the existing workspace/persistence boundary user-aware. It should not introduce a second authenticated implementation beside the current anonymous one.

Non-goals for this milestone:

- Supabase Cloud.
- Social login, phone login, MFA, SSO, or external identity providers.
- Multiple named workspaces/projects per user.
- Sharing data between users.
- Executing generated Python.
- New application database tables unless a concrete requirement appears that cannot be represented by Auth + Storage metadata.
- A new repository/service layer around compiler semantics that are already owned by `DocumentStore`.

## 2. Repository state reviewed

Plan baseline: `main` at commit `07f9670b70fe2b85c84291b1ef365d4a980814b9` (`Add Docker LAN workspace deployment`, 2026-09-13).

Current relevant design:

- `compose.yaml` runs one `vg2c-ui` service with `./data:/data`.
- `src/vg2c_ui/app.py` creates a `WorkspaceManager`, uses an anonymous `vg2c_workspace` cookie, and injects a workspace-scoped `DocumentStore` into each API request.
- `src/vg2c_ui/services/workspaces.py` already owns workspace path validation, upload limits, file listing, local file resolution, and inactive-workspace cleanup.
- `src/vg2c_ui/services/document_store.py` already constrains every document path below one workspace root and owns generated Python + sidecar persistence semantics.
- `src/vg2c_ui/api/workspace.py` already funnels file upload/list/download/archive behavior through the current workspace.
- `src/vg2c_ui/frontend/src/api.ts` is already the centralized browser API boundary, but currently sends no authorization header.
- `src/vg2c_ui/frontend/src/App.tsx` starts directly in the workspace and currently downloads through raw `<a href>` URLs.
- `tests/ui/test_workspace_sessions.py` already verifies anonymous workspace separation and path traversal protection.

The current separation is useful: the authentication change should replace anonymous workspace identity, not replace the compiler/editor storage boundary.

## 3. Current official mechanisms to reuse

Implementation should be based on the current Supabase self-hosting release rather than copied from an old blog/example. At planning time, the official self-hosting Docker guide points to the `self-hosted/v0.8.1` bundle and the current compose uses Envoy as the API gateway.

Use these mechanisms rather than recreating them:

- Supabase Auth (GoTrue) for sign-up, sign-in, refresh tokens, and access JWTs.
- Supabase Storage API for durable object operations.
- Supabase Postgres for Auth/Storage metadata.
- Storage RLS policies for user ownership enforcement.
- Supabase's local filesystem Storage backend for fully local persistence.
- Envoy/API gateway for the standard `/auth/v1`, `/storage/v1`, and `/rest/v1` endpoints.
- `@supabase/supabase-js` in the React frontend for Auth session lifecycle.
- Docker Compose healthchecks and `depends_on: condition: service_healthy`.
- Docker Compose `include` to keep the official/pinned Supabase compose bundle separate while still allowing one root `docker compose up`.

Official references to re-check immediately before implementation:

- https://supabase.com/docs/guides/self-hosting/docker
- https://supabase.com/docs/guides/self-hosting/self-hosted-envoy
- https://supabase.com/docs/guides/self-hosting/auth/config
- https://supabase.com/docs/guides/auth/jwts
- https://supabase.com/docs/guides/storage/security/ownership
- https://supabase.com/docs/guides/storage/schema/design
- https://supabase.com/docs/guides/database/postgres/row-level-security
- https://docs.docker.com/compose/how-tos/multiple-compose-files/include/

Pin the imported self-hosted Supabase version. Do not track `master`/`main` implicitly.

## 4. Proposed compact architecture

```text
Browser
  |
  |-- Supabase JS Auth ---------------------------> local Supabase Envoy
  |                                                  /auth/v1
  |<------------- access + refresh session --------|
  |
  |-- Authorization: Bearer <access JWT> --> FastAPI vg2c-ui
                                                   |
                                                   |-- validate token/current user
                                                   |   against local Supabase Auth
                                                   |
                                                   |-- resolve local working root
                                                   |   /data/workspaces/<user_uuid>/
                                                   |
                                                   |-- existing DocumentStore/compiler
                                                   |   operates on local Paths
                                                   |
                                                   |-- durable reads/writes via
                                                       Supabase Storage API using
                                                       the same user's JWT
                                                              |
                                                              v
                                                        Storage RLS
                                                              |
                                                              v
                                                     local Supabase filesystem
```

There is one logical application workspace per authenticated user for this milestone.

Supabase Storage is the durable source of truth. `/data/workspaces/<user_uuid>` remains a local bounded working copy/cache because the compiler/editor APIs are intentionally filesystem/`Path` based. This avoids rewriting the compiler or creating a parallel storage-aware `DocumentStore`.

## 5. Supabase services

### Functional services required by the application

- `db`: Postgres backing Auth and Storage metadata/RLS.
- `auth`: Supabase Auth / GoTrue.
- `rest`: PostgREST, required by the standard Storage stack and useful for standard Supabase policy behavior.
- `storage`: Supabase Storage API using the local filesystem backend.
- `api-gw`: the official Envoy gateway exposed to the browser and used by the app for standard Supabase endpoints.

### Supporting services from the pinned official self-host bundle

Start with the official dependency graph rather than reimplementing it. The current official Storage compose includes `imgproxy`, and the current gateway/Studio wiring includes `studio`/`meta`. Keep these if required by the pinned compose. Disable/remove only services that the official self-host documentation explicitly allows to be removed and that are not dependencies of the retained services.

Candidates to omit for this milestone when dependency-safe:

- Realtime.
- Edge Functions.
- Analytics/log aggregation/vector services.
- Supavisor connection pooling when not needed for this single local deployment.

Do not spend implementation effort hand-building a smaller gateway topology merely to save a few containers. Correctness and staying close to the supported self-host stack are more important than theoretical minimum container count.

## 6. Docker/Compose integration

### Layout

Recommended structure:

```text
compose.yaml
infra/
  supabase/
    compose.yaml          # pinned from official self-host release
    .env.example          # retained/adapted official settings reference
    volumes/
      api/...
      db/...
      storage/...
      ...
```

Root `compose.yaml` should use Compose `include` to include `infra/supabase/compose.yaml`, then define `vg2c-ui` as today. This keeps Supabase's supported files coherent instead of flattening hundreds of lines into the application compose.

The result must still be one operator command from repository root:

```bash
docker compose up -d --build --wait
```

### `vg2c-ui` dependencies

`vg2c-ui` should not accept authenticated API traffic until the local Supabase gateway/Auth/Storage path is ready. Use health-based dependencies where the retained compose exposes healthchecks.

Minimum app-facing environment:

- `SUPABASE_INTERNAL_URL=http://api-gw:8000`
- `SUPABASE_PUBLIC_URL=http://<LAN-host>:8000`
- `SUPABASE_PUBLISHABLE_KEY=...`
- `VG2C_STORAGE_BUCKET=user-data`
- existing upload/workspace limits.

Do **not** give the application container the Supabase secret/service-role key or Postgres password for normal user operations. Normal storage calls must execute as the current user so RLS remains a real enforcement boundary.

### Persistence volumes

Keep data in local Docker/bind volumes only:

- Supabase Postgres volume for Auth/Storage metadata.
- Supabase local Storage volume for object bytes.
- Existing app `./data:/data` only as working cache.

After migration, deleting the app cache must not delete durable user data. Deleting Supabase DB/Storage volumes is the destructive reset.

## 7. Local Auth configuration

For the initial milestone, use only email + password:

- Email signup enabled.
- Email autoconfirm enabled for this local deployment so signup does not require SMTP or another cloud service.
- Phone signup disabled.
- Anonymous Supabase users disabled.
- OAuth/social providers disabled.
- No external SMTP dependency.

This provides fully local signup/signin while keeping the milestone small.

`SITE_URL` should point at the application's LAN URL, e.g. `http://<host>:8765`. Set the Auth external/public URLs to the local Supabase gateway LAN URL.

If password reset/email verification is later required, add a local SMTP solution as a separate approved milestone rather than quietly introducing an external mail dependency.

## 8. Frontend sign-up/sign-in/session handling

### Add a minimal Supabase client

Add `@supabase/supabase-js` to the React app. Create one small client module, e.g.:

```text
src/vg2c_ui/frontend/src/supabase.ts
```

It should initialize exactly one client instance from runtime configuration.

Do not hardcode `localhost` into the Vite build, because the same Docker image is accessed from other LAN devices.

### Runtime configuration

Add a public, unauthenticated application endpoint such as:

```text
GET /api/config
```

Return only browser-safe values:

- `supabase_url` = public LAN gateway URL.
- `supabase_publishable_key`.

The publishable key is intentionally public. Never return the secret/service-role key.

The frontend loads this once before creating the Supabase client.

### Auth UI

Add a small auth gate around the existing application:

- signed out -> email/password sign-up + sign-in form;
- signed in -> existing `App`;
- signed in -> sign-out action in the existing top bar/header.

Avoid introducing a routing framework solely for login. A small `AuthGate`/auth hook is sufficient.

### Session lifecycle

Use `supabase.auth` APIs:

- `signUp()`
- `signInWithPassword()`
- `getSession()` / auth state subscription
- `signOut()`

Let the official client own access-token refresh and browser session persistence. Do not add an application session cookie or duplicate refresh-token logic.

## 9. Browser-to-backend authentication

Refactor `src/vg2c_ui/frontend/src/api.ts` into a single authenticated request path.

Recommended shape:

- one `authorizedFetch()` helper;
- obtain the current Supabase session immediately before a request;
- add `Authorization: Bearer <access_token>`;
- preserve existing JSON/FormData handling;
- keep common error parsing in one place.

All application data APIs require the token. `/api/health` and `/api/config` remain public.

### Downloads require a small frontend refactor

Current generated-file/archive downloads use plain `<a href>` URLs. A normal anchor cannot attach a Bearer header.

Replace those download links with authenticated fetches through `authorizedFetch()`, then create a temporary object URL from the returned `Blob` and trigger the browser download. Do this in the API module so App UI does not duplicate token/header logic.

Do not move access tokens into query strings or cookies just to preserve anchor downloads.

## 10. Backend authentication/current user

Remove the anonymous workspace-cookie middleware as the source of identity.

Add one request-scoping authentication boundary in the FastAPI layer, preferably a dependency or middleware that:

1. Reads `Authorization: Bearer ...`.
2. Rejects missing/invalid tokens with 401.
3. Validates the token/current user through the self-hosted Auth service using the standard authenticated user endpoint (`/auth/v1/user`) via the internal Supabase URL.
4. Uses the returned Supabase user UUID as the only user identity.
5. Places the user identity/token into request state or a small immutable request context.
6. Resolves that user's workspace and injects the existing scoped `DocumentStore`.

Using Auth `/user` is deliberately preferred over implementing custom JWT parsing/refresh/revocation behavior. It also stays correct if the pinned self-hosted stack moves from the legacy symmetric signing key toward the current asymmetric/JWKS configuration.

The backend must never accept a `user_id` from URL/query/body and then trust it for path selection.

A minimal immutable context is enough, for example conceptually:

```text
AuthenticatedUser(id, access_token)
```

Do not add role/profile/session database models for this milestone.

## 11. Storage model and object structure

Create one private Storage bucket:

```text
user-data
```

Object names:

```text
<supabase_user_uuid>/inputs/<relative-user-path>
<supabase_user_uuid>/generated/<relative-user-path>
```

Examples:

```text
user-data/
  f7d7...-user-uuid/
    inputs/job/source.txt
    inputs/job/data.csv
    generated/job/source.py
    generated/job/source.py.vg2c-ui.json
```

The first path segment is always derived by the backend from the authenticated user UUID. The client only supplies the relative application path below `inputs/` where applicable.

Keep the existing sidecar concept. Sidecars remain internal and continue to be filtered from user file listings.

Do not create one Storage bucket per user. One private bucket + user-prefix RLS is simpler and follows Supabase's intended multi-user Storage model.

## 12. Storage authorization / RLS

Provision the bucket and policies through checked-in SQL/bootstrap migration files under the Supabase infra directory. Do not rely on manually clicking policies in Studio.

The exact SQL should be validated against the pinned Storage schema during implementation. Policy intent:

### SELECT

Only authenticated users may read their own objects:

```sql
bucket_id = 'user-data'
and (storage.foldername(name))[1] = auth.uid()::text
and owner_id = auth.uid()::text
```

### INSERT

Only authenticated users may insert below their own prefix:

```sql
bucket_id = 'user-data'
and (storage.foldername(name))[1] = auth.uid()::text
```

Storage assigns object ownership from the JWT `sub`. Also provide the corresponding SELECT permission needed by current Storage upload behavior where `INSERT ... RETURNING` requires it.

### UPDATE

Require both existing ownership and destination prefix ownership:

- `USING`: bucket, user prefix, and `owner_id = auth.uid()::text`.
- `WITH CHECK`: same user bucket/prefix constraints.

### DELETE

Require bucket, user prefix, and `owner_id = auth.uid()::text`.

No `anon` policies. The bucket remains private.

All application file operations must go through the Storage API. Never write directly into Supabase's storage filesystem volume and never manipulate `storage.objects` directly from application code.

## 13. Backend Storage integration

Add one narrow Supabase integration module, e.g.:

```text
src/vg2c_ui/services/supabase.py
```

Responsibilities only:

- validate/access current Auth user through the documented Auth endpoint;
- list user objects under a supplied already-derived prefix;
- download object bytes;
- upload object bytes;
- delete object;
- translate Storage/Auth errors into small application-level exceptions.

Use the documented local HTTP APIs through a normal HTTP client. This is an adapter to Supabase, not a reimplementation of Auth or Storage. If the official Python client available in the project's package environment materially reduces this code without introducing extra abstractions, use it; otherwise a small `httpx` adapter to the official endpoints is sufficient.

For normal Storage calls, forward the same user's Bearer token and publishable key. Do not use the service-role key, because that would bypass RLS and make application bugs capable of crossing user boundaries.

## 14. Adapt `WorkspaceManager` instead of replacing it

`WorkspaceManager` should become user-aware and stop creating random anonymous workspace IDs.

### Identity/root

Replace:

```text
resolve_or_create(cookie_workspace_id)
```

with conceptually:

```text
resolve_user(user_uuid, access_token)
```

Local root:

```text
/data/workspaces/<user_uuid>/
```

Keep these existing responsibilities in `WorkspaceManager`:

- safe relative path validation;
- allowed upload suffixes;
- per-file size limit;
- per-user file count/total working-set limit;
- local path containment;
- listing/filtering semantics;
- inactive local cache cleanup.

Do not duplicate these checks in a second authenticated manager.

### Durable sync/cache behavior

Because `DocumentStore` and the compiler intentionally operate on local `Path`s, retain the local workspace as a working copy.

On first resolution of a user workspace after cache creation/expiry:

1. Start from an empty user cache root.
2. List the authenticated user's objects from Storage under `<user_uuid>/`.
3. Enforce existing count/size limits while hydrating.
4. Download the files into `inputs/` and `generated/` preserving relative paths.
5. Mark the local cache hydrated.

After hydration, the existing document/compiler flow reads local files as today.

The app currently runs as one `vg2c-ui` service and owns all mutation routes, so a one-time hydration per local cache lifetime is enough for this milestone. Do not add distributed locks, sync daemons, background queues, or a database-backed cache index.

Inactive cleanup now removes only local cache directories. It no longer deletes durable user data.

## 15. Adapt uploads and generated persistence

### Uploads

Keep `WorkspaceManager.save_upload()` as the application entry point, but make it write-through:

1. validate relative path, extension, count, and size exactly as today;
2. derive local target under `inputs/`;
3. derive remote object name `<user_uuid>/inputs/...` internally;
4. upload through Supabase Storage using the user's JWT/RLS;
5. materialize/update the local working copy;
6. preserve current no-silent-overwrite behavior unless explicitly changed later.

Order the local/remote operation so a failed remote write does not leave the API reporting success. Clean up partial local files on failure as the current implementation already does for interrupted uploads.

### Generated files and sidecars

Do not create a parallel `SupabaseDocumentStore`.

Keep `DocumentStore` as the owner of:

- translation;
- atomic local output writes;
- sidecar validity/hashes;
- change preview/apply;
- SQL inspection/actions;
- workspace projection;
- CSV preview;
- path containment.

Introduce the smallest persistence hook/callback necessary for writes/deletes made by `DocumentStore`:

- generated output write -> upload/update corresponding Storage object;
- sidecar write -> upload/update corresponding Storage object;
- sidecar clear/delete -> delete corresponding Storage object.

A small protocol such as `WorkspacePersistence` is acceptable only if it removes direct coupling and is used by the one `DocumentStore`; do not build a repository hierarchy.

If implementation inspection finds that centralizing all generated writes in `WorkspaceManager` is simpler than a protocol, prefer the simpler option.

## 16. API route behavior after cutover

All existing data routes keep their current shape unless authentication requires a response change.

Examples:

- `GET /api/workspace/files`: list only current user's hydrated workspace.
- `POST /api/workspace/files`: save only to current user's `inputs/` prefix.
- `GET /api/workspace/download/...`: resolve only within current user's local root.
- `GET /api/workspace/archive`: archive only current user's files.
- document/translation/change/SQL routes: receive a `DocumentStore` rooted at current user's workspace exactly as they do today.

Unauthenticated access to any of these returns 401 instead of silently creating a new workspace.

Keep `/api/health` public. Keep `/api/config` public and browser-safe.

## 17. Local configuration and secrets

Use one ignored root `.env` as the operator entry point for the application + included Supabase stack. Update `.env.docker.example` with safe placeholders and comments.

Keep secret values out of Git.

Supabase self-host variables will include the current official set such as:

- Postgres password.
- JWT/signing configuration required by the pinned self-host release.
- publishable/anon key configuration required by that release.
- secret/service-role key configuration required internally by Supabase.
- dashboard credentials if Studio is retained.
- local public/external URLs.

Application container gets only:

- internal Supabase gateway URL;
- public Supabase URL;
- publishable key;
- storage bucket id;
- existing workspace/upload limits.

It should not receive:

- `SUPABASE_SECRET_KEY` / service-role key;
- Postgres password;
- dashboard password;
- direct DB credentials.

Keep the existing Docker build-only Git credential secret unchanged.

## 18. Startup and health behavior

### Compose

Use healthchecks from the pinned Supabase compose. Gate `vg2c-ui` startup on the required local Supabase services being healthy, not merely created.

### Application health

Split liveness from dependency readiness if it remains simple:

- `/api/health`: process liveness, always lightweight.
- `/api/ready`: verify the internal Supabase Auth and Storage/gateway endpoints respond successfully.

Point the Compose healthcheck at readiness if the intended meaning is "ready for users".

If keeping one endpoint is materially simpler, `/api/health` may perform these readiness checks instead; avoid building a health subsystem.

### Operator verification

After `docker compose up -d --build --wait`:

1. Supabase gateway healthy.
2. Auth healthy.
3. Storage healthy.
4. `vg2c-ui` ready.
5. Browser signup/signin succeeds using only local services.
6. Restart `vg2c-ui`; signed-in user's durable data rehydrates from Storage.
7. Restart the full stack without deleting volumes; Auth users and objects remain.

## 19. Migration from anonymous cookie workspaces

Current storage directories are keyed by random browser workspace IDs. There is no trustworthy mapping from those IDs to future Supabase user accounts. Therefore the application must **not** guess ownership during migration.

Recommended cutover stages:

### Stage A - Add self-hosted Supabase infrastructure

- Vendor/pin the official self-host Docker bundle.
- Add Compose include.
- Add local environment/secrets documentation.
- Add bucket + RLS bootstrap/migration.
- Verify Auth/Storage locally before touching application behavior.

### Stage B - Add Auth boundary

- Add frontend Supabase client + auth gate.
- Add centralized Bearer injection.
- Add backend current-user validation.
- Make application data APIs require authentication.

### Stage C - Make existing workspace persistence user-aware

- Convert `WorkspaceManager` from cookie IDs to Supabase user UUIDs.
- Add Storage-backed hydration/write-through.
- Add minimal `DocumentStore` persistence hook where generated files/sidecars need durable synchronization.
- Refactor authenticated downloads.

### Stage D - Handle existing anonymous data explicitly

Before cutover, stop the service and snapshot `./data/workspaces` if any old data matters.

Two safe choices:

1. **Default:** do not auto-migrate old anonymous workspaces; retain a backup for a defined period, then delete it.
2. **Explicit one-time import:** an operator maps a specific legacy workspace ID to a specific already-created Supabase user UUID and runs an import utility once.

Never infer ownership from filenames, browser state, IP address, or creation time.

### Stage E - Remove anonymous compatibility

Once authenticated storage is verified:

- remove `vg2c_workspace` cookie behavior;
- remove `VG2C_COOKIE_SECURE` and anonymous retention semantics;
- rename/document workspace retention as local-cache retention;
- update `docs/docker-lan.md` to state that authentication is mandatory and Supabase volumes are durable state.

Do not keep an unauthenticated fallback path. It doubles the security surface and undermines the invariant.

## 20. Tests

### Existing unit tests to adapt

`tests/ui/test_workspace_sessions.py` already covers valuable behavior. Convert it from random workspace A/B IDs to deterministic authenticated user A/B roots while preserving:

- user A file not visible in user B workspace;
- path traversal rejection;
- extension rejection;
- host paths not exposed;
- size/count limits.

Keep `tests/ui/test_document_store.py` as the regression suite for compiler/editor persistence semantics. Add tests only where the persistence hook changes behavior.

### Backend authentication tests

- no Authorization header -> 401 on all protected APIs;
- malformed/invalid/expired token -> 401;
- valid token -> current Supabase UUID attached;
- client body/query/path cannot override current user UUID;
- public health/config endpoints remain available;
- a 401/403 from Supabase is not converted to an anonymous workspace.

Use a small fake Auth/Storage adapter for fast unit tests; do not mock internals of Supabase itself.

### Storage/write-through tests

- upload produces expected local path and expected remote object key;
- generated Python and sidecar persist remotely;
- sidecar deletion removes remote sidecar;
- failed Storage write does not report success or leave an inconsistent local partial file;
- local cache deletion followed by resolve rehydrates the same data;
- list/archive continue to hide sidecars where current behavior expects that.

### Required local integration tests against real self-hosted Supabase

Use the actual Docker services for the security invariant:

1. Create/sign in user A and user B through local Auth.
2. User A uploads files through the application.
3. User A can list/download/translate them.
4. User B sees an empty/different workspace.
5. User B cannot download a guessed user-A application path.
6. User B directly calls local Storage API for user A's object key with B's JWT and receives denial/not-found per Storage semantics.
7. User B cannot update/delete user A objects.
8. User A can update/delete own generated objects as allowed by the app.
9. Restart `vg2c-ui`, remove its local user-A cache, and prove user A data rehydrates while user B remains isolated.
10. Sign out or invalidate the session and prove protected APIs stop working.

The direct Storage API cross-user denial test is important: it proves RLS, not only application path validation.

### Frontend tests

Without adding a heavy frontend test framework solely for this milestone, cover the central behavior at the existing test level where practical:

- authenticated request helper sends Bearer token;
- no session produces authentication failure rather than an anonymous request;
- download helper uses authenticated fetch rather than token-bearing URLs;
- auth state transition clears/does not leak previous user's in-memory workspace UI state.

The last point is required: when user A signs out and user B signs in in the same browser, reset the React workspace reducer state before loading B's files.

## 21. Expected refactoring by file/module

### `compose.yaml`

- include pinned Supabase compose;
- wire `vg2c-ui` dependencies/environment;
- keep existing hardening where compatible.

### `.env.docker.example`

- add Supabase local URLs/keys/settings placeholders;
- remove obsolete anonymous cookie setting after cutover;
- clearly distinguish public values from secrets.

### `src/vg2c_ui/app.py`

- remove anonymous cookie workspace middleware;
- add Auth/current-user request scoping;
- inject user-rooted `DocumentStore`;
- add public runtime config and readiness check if chosen.

### `src/vg2c_ui/services/workspaces.py`

- replace anonymous ID lifecycle with authenticated user UUID lifecycle;
- retain safe paths, quotas, listing, local cache cleanup;
- add Storage hydration/write-through using the narrow Supabase adapter.

### `src/vg2c_ui/services/document_store.py`

- retain all existing path/compiler/editor behavior;
- add only the minimal persistence notification/hook needed for generated output/sidecar writes and deletes.

### new `src/vg2c_ui/services/supabase.py`

- narrow Auth/Storage HTTP adapter only;
- no business/compiler logic.

### `src/vg2c_ui/frontend/src/api.ts`

- one authorized fetch path;
- remove duplicated raw fetch error handling;
- authenticated blob download helpers.

### new small frontend Auth modules

- Supabase client bootstrap from `/api/config`;
- Auth session hook/gate;
- sign-in/sign-up/sign-out UI.

### `src/vg2c_ui/frontend/src/useWorkspace.ts` / state

- keep domain operations unchanged;
- add/reset action only if needed so switching authenticated users cannot retain previous user's open tabs/files in memory.

### `src/vg2c_ui/frontend/src/App.tsx`

- sign-out affordance;
- use authenticated download actions instead of raw protected links.

### `tests/ui/*`

- migrate isolation tests from anonymous-cookie identity to authenticated user identity;
- add adapter/auth/storage sync tests.

## 22. Security checks to preserve

Authentication must be additive to current filesystem safeguards, not a replacement for them.

Keep both enforcement layers:

1. **Application containment:** `DocumentStore._resolve()` and workspace relative-path validation ensure an authenticated user cannot escape their local root.
2. **Durable storage enforcement:** Storage RLS ensures a valid user JWT cannot operate on another user's remote objects even if an application bug constructs the wrong object key.

Also preserve:

- no executable upload types;
- existing file/count/total limits;
- no generated Python execution;
- no host absolute paths in browser responses;
- read-only/hardened app container where compatible;
- BuildKit-only private dependency credentials.

## 23. Decisions requiring approval before implementation

### A. Legacy anonymous workspace data

Recommended default: **do not auto-migrate**. Snapshot if needed; import only explicit workspace-ID -> user-UUID mappings.

Approval needed: retain/import legacy anonymous data, or treat it as disposable test data.

### B. Plain HTTP on the LAN

The current deployment intentionally uses plain HTTP. Once login credentials and Bearer tokens exist, HTTP allows anyone capable of observing LAN traffic to capture credentials/tokens.

Recommended production/internal-shared posture: terminate HTTPS before exposing authenticated use beyond a tightly trusted test machine/network.

Approval needed: keep plain HTTP temporarily for trusted local testing, or include local TLS/reverse-proxy termination in the authentication milestone.

### C. Account policy

Recommended milestone default: open local email/password signup with email autoconfirm, no SMTP.

Approval needed if signup should instead be invitation/admin-only, domain-restricted, or require real email verification/password recovery. Those requirements affect Auth configuration and possibly a local mail service.

## 24. Definition of done

The milestone is complete when:

- one root Compose command starts the application and pinned self-hosted Supabase stack with no Supabase Cloud dependency;
- a user can sign up/sign in locally and retain a session through Supabase Auth;
- unauthenticated application data APIs return 401;
- backend identity comes only from validated Supabase Auth state;
- all durable files live in one private Supabase Storage bucket below the authenticated user's UUID prefix;
- RLS prevents cross-user read/write/delete even when Storage is called directly with another valid user's JWT;
- existing compiler/editor APIs continue using one user-rooted `DocumentStore` rather than parallel auth/anon implementations;
- app-local cache can be deleted/restarted and durable data rehydrates from local Supabase Storage;
- user A can never list, download, modify, archive, or otherwise reach user B's stored application data;
- current path traversal, file type, quota, no-execution, and path-redaction protections still pass;
- the anonymous workspace cookie path is removed after cutover.