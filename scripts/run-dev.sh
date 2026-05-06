#!/bin/bash

echo "🚀 Starting CodeDialect Infrastructure..."
docker-compose -f docker/docker-compose.yml up -d

echo "🔥 Starting Backend API..."
(cd apps/api/CodeDialect.WebAPI && dotnet watch run) &

echo "⚛️ Starting Frontend Web..."
(cd apps/web && npm run dev) &

echo "✅ Services started in background."
