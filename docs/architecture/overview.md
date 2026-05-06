# CodeDialect Architecture

## Core Principles
- **Clean Architecture**: Domain > Application > Infrastructure > API
- **CQRS**: All operations are divided into Commands (mutations) and Queries (reads).
- **Service-Oriented Frontend**: React with TanStack Query and Zustand for state.

## Folder Structure
- `apps/api`: Backend source code.
- `apps/web`: Frontend source code.
- `apps/runner`: Code execution workers.
- `docker/`: Dockerfiles and orchestration.
- `challenges/`: YAML/JSON definitions of coding challenges.
