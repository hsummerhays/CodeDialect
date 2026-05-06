# CodeDialect

> Open-source platform for cross-language coding challenges, syntax evolution, and ecosystem comparison.

**CodeDialect** helps developers understand how different programming languages, frameworks, and runtime versions express the same software concepts.

Instead of focusing only on algorithms, CodeDialect emphasizes:

* modern syntax evolution
* ecosystem migration
* idiomatic implementations
* version-to-version comparisons
* real-world software patterns

Examples include:

* C# .NET Framework → .NET 10
* Java 8 → Java 21
* React class components → Hooks
* JavaScript → TypeScript
* AngularJS → Modern Angular
* Node.js CommonJS → ES Modules

---

# 🚀 Vision

Software ecosystems evolve rapidly, and developers often struggle to:

* modernize legacy syntax
* learn newer framework patterns
* compare idiomatic implementations across languages
* understand how ecosystems solve similar problems differently

CodeDialect aims to become a platform for:

* syntax modernization
* ecosystem comparison
* developer education
* coding challenges
* language evolution tracking

---

# ✨ Core Features

## Multi-Language Coding Challenges

Solve challenges using multiple programming languages and framework ecosystems.

## Version-Aware Comparisons

Compare implementations across runtime and framework versions:

* .NET 8 vs .NET 10
* Java 8 vs Java 21
* React legacy vs modern patterns

## Side-by-Side Syntax Viewer

Visualize ecosystem differences with synchronized comparison views.

## Monaco-Powered Editor

Professional-grade editing experience using the VSCode Monaco Editor.

## Challenge Categories

Support for:

* Front-End
* Back-End
* Full Stack
* DevOps
* Databases
* APIs
* Cloud
* Security
* Architecture

## Extensible Execution Engine

Architecture designed for isolated containerized execution and benchmarking.

---

# 🏗️ Architecture

CodeDialect follows **Clean Architecture** principles with strong separation of concerns, modularity, and extensibility.

## High-Level Architecture

```mermaid
graph TD
    Web[React Frontend]
    API[.NET API]
    App[Application Layer]
    Domain[Domain Layer]
    Infra[Infrastructure]
    DB[(PostgreSQL)]
    Redis[(Redis)]

    Web --> API
    API --> App
    App --> Domain
    App --> Infra
    Infra --> DB
    Infra --> Redis
```

## Layers

### Domain

Pure business logic and entities.

* No framework dependencies
* Core models and rules
* Language/version abstractions

### Application

Application use cases and orchestration.

* CQRS Commands & Queries
* DTOs
* Validation
* Interfaces
* MediatR pipelines

### Infrastructure

External implementations and integrations.

* EF Core
* Redis
* Authentication
* Future execution runners
* External services

### WebAPI

Application entry point.

* Controllers
* Middleware
* Authentication
* OpenAPI/Swagger
* Dependency injection

---

# 🚀 Tech Stack

## Backend

* .NET 10
* ASP.NET Core Web API
* Clean Architecture
* CQRS + MediatR
* Entity Framework Core
* PostgreSQL
* Redis
* JWT Authentication
* Swagger / OpenAPI

## Frontend

* React 18+
* TypeScript
* Vite
* Tailwind CSS
* Monaco Editor
* Zustand
* TanStack Query

## Infrastructure

* Docker
* Docker Compose
* Environment-based configuration
* GitHub Actions readiness
* Kubernetes-ready architecture

---

# 🔒 Planned Code Execution Model

CodeDialect is being architected to support secure isolated execution using containerized runners.

Planned capabilities include:

* Docker-based sandboxing
* Execution timeouts
* Memory limits
* Runtime benchmarking
* Multi-language runners
* Compile/run pipelines
* Execution telemetry

The execution engine is intentionally abstracted to support future scalability and distributed execution.

---

# 🛠️ Getting Started

## Prerequisites

* .NET 10 SDK
* Node.js 18+
* Docker & Docker Compose

---

## Local Setup

### 1. Clone the Repository

```bash
git clone https://github.com/hsummerhays/CodeDialect.git
```

### 2. Start Infrastructure Services

```bash
cd docker
docker-compose up -d
```

### 3. Start Backend

```bash
cd apps/api/CodeDialect.WebAPI
dotnet run
```

### 4. Start Frontend

```bash
cd apps/web
npm install
npm run dev
```

---

# 🧩 MVP Features

* Authentication & Authorization
* Challenge Explorer
* Challenge Categories
* Syntax Comparison Viewer
* Monaco Editor Integration
* Progress Tracking Dashboard
* Challenge Difficulty Levels
* Multi-Version Challenge Support
* Clean Architecture Backend
* Docker-Based Local Development

---

# 🗺️ Roadmap

## Phase 1

* Core platform
* Authentication
* Challenge management
* Syntax comparison viewer
* Multi-language metadata

## Phase 2

* Secure code execution
* Benchmarking
* Runtime comparisons
* Scoring engine
* Submission history

## Phase 3

* AI-assisted feedback
* Idiomatic scoring
* Migration assistance
* Team/organization support
* Real-time collaboration

---

# 🤝 Contributing

Contributions are welcome.

Planned contributor areas include:

* Language runners
* Challenge packs
* Framework comparisons
* Frontend UX improvements
* Benchmarking
* Documentation

Please see:

* `CONTRIBUTING.md`
* `ROADMAP.md`
* `CODE_OF_CONDUCT.md`

---

# 📄 License

This project is licensed under the MIT License.
