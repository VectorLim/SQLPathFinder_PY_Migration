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
COPY pyproject.toml README.md aed_updater.py ./
COPY src/scripthost_portable ./src/scripthost_portable
COPY install_packages/aed_client_apikey-6.2.0-py3-none-any.whl ./install_packages/
COPY ICMPCS.txt CSR_IAM_v2.txt ./jobs/
RUN python -m pip install --no-cache-dir -e . \
    ./install_packages/aed_client_apikey-6.2.0-py3-none-any.whl
USER scripthost
ENTRYPOINT ["python", "-m", "scripthost_portable.launcher"]
CMD ["--help"]

FROM runtime AS validation
USER root
RUN python -m pip install --no-cache-dir pytest
COPY tests ./tests
USER scripthost
ENTRYPOINT ["python", "-m", "pytest"]
CMD ["tests/scripthost_portable", "-q", "-p", "no:cacheprovider"]

FROM runtime AS final
