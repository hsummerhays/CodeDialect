# CodeDialect Development Stop Script

Write-Host "🛑 Stopping Infrastructure..." -ForegroundColor Red
docker-compose -f docker/docker-compose.yml down

Write-Host "🧹 Cleaning up background processes..." -ForegroundColor Yellow
Get-Process -Name dotnet -ErrorAction SilentlyContinue | Stop-Process -Force
Get-Process -Name node -ErrorAction SilentlyContinue | Stop-Process -Force

Write-Host "✅ Development environment stopped." -ForegroundColor Green
