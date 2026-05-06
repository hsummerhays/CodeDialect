#!/bin/bash

echo "🚀 Setting up CodeDialect..."

echo "📦 Installing frontend dependencies..."
cd apps/web && npm install

echo "🏗️ Building backend..."
cd ../../apps/api && dotnet build

echo "✅ Setup complete! Run 'docker-compose up -d' in the docker/ folder to start infrastructure."
