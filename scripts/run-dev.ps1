# CodeDialect Development Start Script

Write-Host "🚀 Starting CodeDialect Infrastructure..." -ForegroundColor Cyan
docker-compose -f docker/docker-compose.yml up -d

Write-Host "🔥 Starting Backend API (Hot Reload)..." -ForegroundColor Yellow
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd apps/api/CodeDialect.WebAPI; dotnet watch run"

Write-Host "⚛️ Starting Frontend Web..." -ForegroundColor Blue
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd apps/web; npm run dev"

Write-Host "✅ All services are starting!" -ForegroundColor Green
Write-Host "API: http://localhost:5187/swagger"
Write-Host "Web: http://localhost:5173"
