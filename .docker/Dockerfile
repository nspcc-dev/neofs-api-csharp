# NEO private network - Dockerfile
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS builder

# Frontend non-interactive
ENV DEBIAN_FRONTEND noninteractive

# Disable dotnet usage information collection
# https://docs.microsoft.com/en-us/dotnet/core/tools/telemetry#behavior
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

# Install system dependencies. always should be done in one line
# https://docs.docker.com/engine/userguide/eng-image/dockerfile_best-practices/#run
RUN set -x \
    && apt-get update \
    && apt-get install -y \
        unzip \
        screen \
        wget \
        curl \
        jq \
        libssl-dev \
        libunwind8 \
    # APT cleanup to reduce image size
    && rm -rf /var/lib/apt/lists/*

WORKDIR /neofs-api-csharp

COPY . /neofs-api-csharp

RUN dotnet restore -r linux-x64

RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim

# Disable dotnet usage information collection
# https://docs.microsoft.com/en-us/dotnet/core/tools/telemetry#behavior
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

WORKDIR /app

COPY --from=builder /app .

COPY .docker/example.txt /.docker/example.txt

RUN echo "cat /.docker/example.txt" >> /root/.bashrc
