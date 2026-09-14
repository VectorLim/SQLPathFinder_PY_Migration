# syntax=docker/dockerfile:1.7

FROM node:22-bookworm-slim AS frontend
WORKDIR /frontend
COPY src/vg2c_ui/frontend/package.json src/vg2c_ui/frontend/package-lock.json ./
RUN npm ci
COPY src/vg2c_ui/frontend/ ./
# Contracts are generated and committed by the Python project before image builds.
RUN npx tsc -b && npx vite build

FROM python:3.13-slim AS builder
WORKDIR /app
RUN apt-get update \
    && apt-get install --no-install-recommends -y ca-certificates git \
    && rm -rf /var/lib/apt/lists/*
COPY --from=ghcr.io/astral-sh/uv:0.10.7 /uv /uvx /bin/
COPY pyproject.toml uv.lock README.md ./
COPY src ./src
# git-credentials is a BuildKit secret in git credential-store format. It is never copied
# into an image layer. Corporate proxy and CA configuration are supplied by the host.
RUN --mount=type=secret,id=git_credentials,required=false \
    git config --global credential.helper 'store --file=/run/secrets/git_credentials' \
    && uv sync --locked --extra ui --no-dev

FROM python:3.13-slim AS runtime
WORKDIR /app
ENV PATH="/app/.venv/bin:$PATH" \
    PYTHONUNBUFFERED=1 \
    VG2C_DATA_DIR=/data
RUN groupadd --gid 10001 vg2c \
    && useradd --uid 10001 --gid vg2c --create-home --home-dir /app vg2c \
    && mkdir /data \
    && chown -R vg2c:vg2c /app /data
COPY --from=builder --chown=vg2c:vg2c /app/.venv /app/.venv
COPY --chown=vg2c:vg2c src ./src
COPY --from=frontend --chown=vg2c:vg2c /static ./src/vg2c_ui/static
USER vg2c
EXPOSE 8765
CMD ["python", "-m", "vg2c_ui", "--host", "0.0.0.0", "--port", "8765", "--data-dir", "/data"]
