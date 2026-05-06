@echo off
echo 🛑 Stopping Infrastructure...
docker-compose -f docker/docker-compose.yml down

echo 🧹 Cleaning up background processes...
taskkill /IM dotnet.exe /F
taskkill /IM node.exe /F

echo ✅ Done.
