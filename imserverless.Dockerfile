# imserverless image (Linux). The base already provides func.json (functionlib main.py),
# Oracle Instant Client, tnsnames/krb5 and DataSyncX; it calls /app/main.py:handle().
ARG BASE_IMAGE=gar-registry.caas.intel.com/opedaweb/soims:base
FROM ${BASE_IMAGE}
USER root
WORKDIR /app
ENV PYTHONUNBUFFERED=1 ENV_MODE=test SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT=1
COPY pyproject.toml README.md main.py ./
COPY src/scripthost_portable ./src/scripthost_portable
COPY scripthost-utilities-decompiled/SPSQL3_py ./scripthost-utilities-decompiled/SPSQL3_py
# Editable install: runtime.py locates SPSQL3_py relative to the source tree.
RUN python -m pip install --no-cache-dir -e . \
    && chmod -R 777 /app
