# Contributing to CodeDialect

We love your input! We want to make contributing to CodeDialect as easy and transparent as possible, whether it's:

- Reporting a bug
- Discussing the current state of the code
- Submitting a fix
- Proposing new features
- Adding new coding challenges

## Development Process

1. **Fork the repo** and create your branch from `main`.
2. **Setup your environment** following the `README.md`.
3. **Write clean code** adhering to Clean Architecture principles.
4. **Add tests** if you're adding new functionality.
5. **Issue a pull request**.

## Architecture Guidelines

- **Keep the Domain pure**: Do not add dependencies to external libraries in the Domain project.
- **Feature-First**: If adding a major new feature, consider creating a dedicated folder in the Application layer.
- **CQRS**: All data modifications should go through MediatR Commands. All data retrieval should go through Queries.

## Code Style

- Use **PascalCase** for classes and methods.
- Use **camelCase** for private fields and local variables.
- Use **Prettier** for frontend code and **EditorConfig** for backend.

## Community

Join our Discord server to discuss the roadmap and get help with development!
