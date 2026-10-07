# imserverless image (Linux). The base already provides func.json (functionlib main.py),
# Oracle Instant Client, tnsnames/krb5 and DataSyncX; it calls /app/main.py:handle().
# Not BASE_IMAGE: soims build_push_rancher.ps1 passes that arg for dockerfile_base.
ARG SOIMS_BASE_IMAGE=amr-registry.caas.intel.com/soia/icmpcs-soims:base
FROM ${SOIMS_BASE_IMAGE}
USER root
WORKDIR /app
ENV PYTHONUNBUFFERED=1 ENV_MODE=test SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT=1 \
    SCRIPT_PATH=/app/jobs/test_long.txt
COPY pyproject.toml README.md main.py aed_updater.py ./
COPY install_packages/aed_client_apikey-6.2.0-py3-none-any.whl ./install_packages/
COPY tests/fixtures/test_long.txt ./jobs/test_long.txt
COPY ICMPCS.txt CSR_IAM_v2.txt ./jobs/
COPY output/aed-migration/CSR_IAM_v2.aed.txt ./jobs/
COPY output/clean-python/*.py ./jobs/
COPY src/scripthost_portable ./src/scripthost_portable
COPY src/vg2c ./src/vg2c
RUN python -m pip install --no-cache-dir \
    ./install_packages/aed_client_apikey-6.2.0-py3-none-any.whl . \
    && chmod -R 777 /app
