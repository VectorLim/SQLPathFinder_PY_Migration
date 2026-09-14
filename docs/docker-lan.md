# LAN Docker deployment

This deployment is an internal testing service. It serves the web editor over plain HTTP and does not authenticate users. Keep the published port restricted to the intended Intel network.

## Prerequisites

- Docker Desktop is running in Linux-container mode.
- The PC can reach `mfg-github.mfg.intel.com` and `tmg-repo.mfg.intel.com` while building the image.
- Docker can trust the applicable Intel corporate certificate and use any required proxy settings.
- Create `secrets/git-credentials` outside source control with the Git credential-store entry that permits the DataSyncX repository download. The file is mounted only while building and is not included in the image.

For example, copy `.env.docker.example` to `.env`, set `VG2C_GIT_CREDENTIALS_FILE` to that local secret file, then run:

```powershell
docker compose up -d --build
docker compose ps
docker compose logs -f vg2c-ui
```

The application stores transient user workspaces in `./data`, which is intentionally excluded from Git. Use `docker compose down` to stop it; do not delete `data` unless it is acceptable to remove every active workspace.

## Access and firewall

Find the PC's Intel-LAN IPv4 address:

```powershell
Get-NetIPAddress -AddressFamily IPv4 | Where-Object {$_.IPAddress -notlike '127.*'}
```

Users on the same allowed network open `http://<PC-IP>:8765`. Change the host port through `APP_PORT` in `.env`.

If Windows Defender Firewall blocks the connection, create a narrow inbound TCP rule for the selected port and the approved Intel network scope, for example:

```powershell
New-NetFirewallRule -DisplayName 'PYTHONPathFinder LAN test' -Direction Inbound -Action Allow -Protocol TCP -LocalPort 8765 -Profile Domain
```

Coordinate with the Intel network/firewall owner if the PC is reachable locally but not from another device. Do not expose this plain-HTTP test deployment beyond the intended network.

## Workspace behavior

Each browser receives a random, HTTP-only session cookie and a separate workspace. Users upload `.txt`, `.csv`, `.tab`, `.dat`, `.xlsx`, or `.xls` files, translate selected VG2 `.txt` files, and download generated files or a workspace archive. Paths are resolved only below that browser's workspace.

Workspaces expire after 24 hours of inactivity by default. Configure retention, cleanup frequency, per-file upload size, total workspace size, and upload count in `.env`. The application does not execute generated Python; it only translates and edits it. The internal execution-service boundary is intentionally disabled until a separate resource, credential, and audit design is approved.

## Verification and updates

After startup, verify `http://127.0.0.1:8765/api/health`, upload a known VG2 fixture from a browser, translate it, and download the resulting Python. Repeat from a second browser profile or LAN device and verify that neither browser can see the other's files.

For an update, rebuild with `docker compose up -d --build`. Docker must be able to reach the Intel dependency services for every uncached build. Inspect `docker compose logs vg2c-ui` if the health check does not become healthy.
