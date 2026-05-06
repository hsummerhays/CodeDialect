#!/bin/bash

echo "🛑 Stopping Infrastructure..."
docker-compose -f docker/docker-compose.yml down

echo "🧹 Stopping local processes..."
pkill dotnet
pkill node

echo "✅ Done."
