 <div align="center">

# 🚗 DVLD
### Driving & Vehicle License Department

**A Modern Web-Based Driving License Management System**

An evolution of the original desktop application, redesigned around modern web technologies, clean architecture, and scalable software engineering practices.

<p>
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 9" />
  <img src="https://img.shields.io/badge/C%23-Language-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core Web API" />
</p>

<p>
  <img src="https://img.shields.io/badge/Entity_Framework-Core-68217A?style=for-the-badge&logo=dotnet&logoColor=white" alt="Entity Framework Core" />
  <img src="https://img.shields.io/badge/SQL_Server-Database-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="Microsoft SQL Server" />
  <img src="https://img.shields.io/badge/REST-API-008080?style=for-the-badge" alt="REST API" />
</p>

<p>
  <img src="https://img.shields.io/badge/Architecture-Clean_Architecture-orange?style=for-the-badge" alt="Clean Architecture" />
  <img src="https://img.shields.io/badge/Design-Dependency_Injection-blue?style=for-the-badge" alt="Dependency Injection" />
  <img src="https://img.shields.io/badge/Status-Active_Development-success?style=for-the-badge" alt="Active Development" />
</p>

</div>

---

## 📖 About The Project

**DVLD (Driving & Vehicle License Department)** is a web-based system designed to simulate the operations of a driving license department, managing applicants, licensing applications, examinations, and license-related services.

Building upon the original desktop application, this version expands the system through a modern Web API and a more extensible architecture, with an emphasis on maintainability, reusable components, and well-defined business rules.

The project focuses not only on implementing functionality, but also on applying professional software engineering practices to a complex, business-oriented application.

## ✨ Core Features

- **Applicant Management** — Manage personal information and applicant records.
- **Application Management** — Create and track licensing applications.
- **License Management** — Handle license issuance, renewal, and replacement.
- **Examination Management** — Schedule examinations and record test results.
- **International Licenses** — Manage international licensing services.
- **Detained Licenses** — Manage license detention and release procedures.
- **Business Rule Validation** — Enforce eligibility requirements and workflow constraints.
- **Centralized Data Management** — Maintain relational data through SQL Server and Entity Framework Core.

## 🏗️ Architecture & Engineering

The system follows **Clean Architecture principles**, organizing responsibilities into distinct layers and reducing unnecessary coupling between business logic and infrastructure.

### Key Design Principles

- **Separation of Concerns** — Keep business logic, data access, and API responsibilities clearly separated.
- **Dependency Inversion** — Rely on abstractions to improve flexibility and reduce coupling.
- **Dependency Injection** — Manage dependencies through ASP.NET Core's built-in DI container.
- **Middleware Pipeline** — Organize cross-cutting HTTP request processing in a centralized and reusable manner.
- **Maintainability & Extensibility** — Structure components to support future changes without unnecessary architectural disruption.
- **Reusable Contracts & DTOs** — Define clear boundaries between application layers and API request/response models.

### Architecture Overview

```text
                ASP.NET Core Web API
                         │
                         ▼
                 Middleware Pipeline
                         │
                         ▼
                  Application Layer
                 Contracts & Services
                         │
                         ▼
                    Domain Layer
                  Business Rules
                         │
                         ▼
                Infrastructure Layer
              EF Core & Repositories
                         │
                         ▼
                  SQL Server
```

*Conceptual overview of the system's architectural responsibilities.*

## 🛠️ Technology Stack

| Category | Technology |
|---|---|
| Language | C# |
| Framework | .NET 9 |
| Backend | ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Database | Microsoft SQL Server |
| API Design | RESTful APIs |
| Architecture | Clean Architecture |
| Request Pipeline | ASP.NET Core Middleware |
| Dependency Management | Dependency Injection |

## 🔐 Security & Authentication

Security enhancements are part of the project's development roadmap.

Planned improvements include:

- **JWT Authentication** — Enable token-based authentication for API clients.
- **Role-Based Authorization** — Control access according to user roles and permissions.
- **Protected Endpoints** — Apply authentication and authorization requirements to sensitive operations.

These features will be incorporated as development progresses.

## 🚧 Development Status

The web version is actively under development, extending the functionality of the original desktop application while introducing a more flexible architectural foundation.

Current development focuses on API implementation, application workflows, data access, and refining the system's architecture. Authentication enhancements and additional capabilities will follow as the project evolves.

## 🎯 Project Objectives

DVLD is an ongoing software engineering project focused on translating complex real-world workflows into a maintainable application.

Its main objectives are to:

- Apply Clean Architecture in a practical, business-oriented system.
- Design and implement a structured ASP.NET Core Web API.
- Enforce business rules consistently across licensing workflows.
- Build a maintainable foundation for future features and integrations.
- Strengthen expertise in backend engineering, database design, and system architecture.

---

<div align="center">

**Built with C#, .NET 9, ASP.NET Core Web API, Entity Framework Core, and SQL Server.**

*Designed with a focus on clean code, architectural clarity, and long-term maintainability.*

</div>
