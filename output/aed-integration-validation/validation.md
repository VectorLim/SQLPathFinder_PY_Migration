# AED integration validation

Branch: `AED-integration`. Image: `sqlpathfinder-aed:validation` (local only).

- SOIMS `1.12`: AED API-key 6.2.0, DataSyncX 1.1.10, CA certificate present.
- Installed SOIMS `base`: AED API-key 6.0.0, DataSyncX 1.1.10; SMB support absent.
- New image: AED API-key 6.2.0, SMB protocol 1.17.0, DataSyncX 1.1.10.
- Final isolated Linux package checks: 23 passed. Python `-I` confirmed the
  updater imports from site-packages, with API-key client and bundled certificate.
- Broader Linux runtime checks: 134 passed, 2 R tests deselected because the
  SOIMS base has no R interpreter. Tests and expected source paths mounted read-only.
- Broader Windows run: 108 passed, 15 skipped, 9 failed in legacy transport,
  worker error categorization, and SQLite UDF checks. This suite is not green.
- Both pilot controller trees parse with exactly one AED task. All four detection
  query bodies in each pilot match the pre-edit snapshots under `baseline`.
- The focused checks use mocked SMB/AED services and the real existing updater.
  Docker test containers use `--network none`. No live SMB/AED authentication,
  attribute writes, notification delivery, deployment, or registry push was validated.
- Ruff was unavailable in the existing Windows virtual environment.

Setup instructions are in the repository README's AED IAM jobs section.
No config mount is required. Site/job configuration comes from the network
config.txt; SMB/API credentials are separate deployment secrets. History writes
require a single active writer for each job/site.
