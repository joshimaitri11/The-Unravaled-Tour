FROM ubuntu:20.04

ENV DEBIAN_FRONTEND=noninteractive
ENV PORT=5000

# Install dependencies and add official Mono repository
RUN apt-get update && apt-get install -y --no-install-recommends \
    gnupg \
    ca-certificates \
    curl \
    && curl -fsSL https://download.mono-project.com/repo/xamarin.gpg | gpg --dearmor -o /etc/apt/trusted.gpg.d/mono-official-archive.gpg \
    && echo "deb https://download.mono-project.com/repo/ubuntu stable-focal main" > /etc/apt/sources.list.d/mono-official-stable.list \
    && apt-get update \
    && apt-get install -y --no-install-recommends \
       mono-devel \
       mono-xsp4 \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY . /app

EXPOSE 5000

HEALTHCHECK --interval=30s --timeout=5s --start-period=15s --retries=3 \
  CMD curl -f http://localhost:${PORT:-5000}/Default.aspx || exit 1

CMD ["sh", "-c", "exec xsp4 --port ${PORT:-5000} --address 0.0.0.0 --nonstop"]
