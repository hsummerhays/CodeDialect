@echo off
echo 🚀 Starting CodeDialect Infrastructure...
docker-compose -f docker/docker-compose.yml up -d

echo 🔥 Starting Backend API...
start powershell -NoExit -Command "cd apps/api/CodeDialect.WebAPI; dotnet watch run"

echo ⚛️ Starting Frontend Web...
start powershell -NoExit -Command "cd apps/web; npm run dev"

echo ✅ Services are starting!
echo API: http://localhost:5187/swagger
echo Web: http://localhost:5173
