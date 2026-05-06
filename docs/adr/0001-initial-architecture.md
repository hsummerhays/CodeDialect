# ADR 0001: Initial Architecture Selection

## Status
Accepted

## Context
We need a scalable, maintainable foundation for the CodeDialect platform.

## Decision
We chose .NET 10 with Clean Architecture and CQRS for the backend, and React with Tailwind and Monaco for the frontend.

## Consequences
- High separation of concerns.
- Steeper learning curve for new contributors.
- Robust support for multi-language runners.
