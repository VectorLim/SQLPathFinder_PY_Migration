# imserverless image (Linux). The base already provides func.json (functionlib main.py),
# Oracle Instant Client, tnsnames/krb5 and DataSyncX; it calls /app/main.py:handle().
# Not BASE_IMAGE: soims build_push_rancher.ps1 passes that arg for dockerfile_base.
ARG SOIMS_BASE_IMAGE=amr-registry.caas.intel.com/soia/soims:base
FROM ${SOIMS_BASE_IMAGE}
USER root
WORKDIR /app
ENV PYTHONUNBUFFERED=1 ENV_MODE=test SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT=1 \
    SCRIPT_PATH=/app/jobs/test_long.txt
COPY pyproject.toml README.md main.py ./
COPY tests/fixtures/test_long.txt ./jobs/test_long.txt
COPY src/scripthost_portable ./src/scripthost_portable
RUN python -m pip install --no-cache-dir . \
    && chmod -R 777 /app
