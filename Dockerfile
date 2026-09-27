# syntax=docker/dockerfile:1.7
FROM python:3.12-slim AS runtime
WORKDIR /app
ENV PYTHONUNBUFFERED=1 PYTHONPATH=/app/src
RUN apt-get update \
    && apt-get install --no-install-recommends -y ca-certificates r-base-core \
    && rm -rf /var/lib/apt/lists/* \
    && groupadd --gid 10001 scripthost \
    && useradd --uid 10001 --gid scripthost --create-home scripthost \
    && chown scripthost:scripthost /app
COPY pyproject.toml README.md ./
COPY src/scripthost_portable ./src/scripthost_portable
COPY scripthost-utilities-decompiled/SPSQL3_py ./scripthost-utilities-decompiled/SPSQL3_py
RUN python -m pip install --no-cache-dir -e .
USER scripthost
ENTRYPOINT ["python", "-m", "scripthost_portable.launcher"]
CMD ["--help"]

FROM runtime AS validation
USER root
RUN python -m pip install --no-cache-dir pytest
COPY tests ./tests
COPY scripthost-utilities-decompiled/22844.spfsql ./scripthost-utilities-decompiled/22844.spfsql
USER scripthost
ENTRYPOINT ["python", "-m", "pytest"]
CMD ["tests/scripthost_portable", "-q", "-p", "no:cacheprovider"]

FROM runtime AS final
