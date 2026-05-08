# Infrastructure

This directory is reserved for production infrastructure-as-code. It is currently a placeholder.

Planned contents:

- `terraform/` — cloud resource definitions (VPC, RDS, ElastiCache, ECS/EKS)
- `k8s/` — Kubernetes manifests for API and runner deployments
- `ci/` — CI/CD pipeline definitions (GitHub Actions workflows)
- `secrets/` — secret rotation scripts and environment templates (no actual secrets committed)

---

## Docker (local development)

Docker configuration lives in `docker/` at the repo root, not here.

| File | Purpose |
|---|---|
| `docker/docker-compose.infra.yml` | PostgreSQL 15 + Redis 7 only (for local dev without running the full stack in Docker) |
| `docker/docker-compose.yml` | Full stack — API + PostgreSQL + Redis |
| `docker/Dockerfile.api` | Production image for the .NET API |
| `docker-compose.yml` (root) | Root convenience compose (full stack) |

### Start just the backing services

```bash
npm run dev:infra
# or
docker compose -f docker/docker-compose.infra.yml up -d
```

Services:

| Service | Port | Credentials |
|---|---|---|
| PostgreSQL | 5432 | `postgres` / `postgres` / db: `codedialect` |
| Redis | 6379 | no auth |

### Production environment variables

Set these outside of any committed file (e.g. via your cloud secrets manager or CI environment):

```
JwtSettings__Secret=<min 32 char random string>
ConnectionStrings__DefaultConnection=Host=...;Database=codedialect;Username=...;Password=...
ConnectionStrings__Redis=<host>:<port>
ASPNETCORE_ENVIRONMENT=Production
```
