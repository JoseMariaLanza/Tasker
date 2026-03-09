# Tasker

User-facing task management application. Manages tasks, categories, subcategories, and user assignments with status tracking.

## Architecture

3-layer architecture:

```
Tasker/                          # ASP.NET Core Web API (host)
├── Controllers/
│   ├── TaskController.cs         # CRUD for tasks
│   ├── CategoryController.cs     # CRUD for categories
│   └── UserController.cs         # User management
├── Swagger/                      # Swagger configuration
├── Program.cs                    # DI composition and middleware pipeline
└── Dockerfile                    # Multi-stage .NET 8 build

Tasker.Services/                 # Business logic layer
├── Interfaces/                   # Service abstractions
├── Services/                     # Business logic implementations
├── DTO/
│   ├── TaskDTOs/                 # Task request/response DTOs
│   ├── CategoryDTOs/             # Category DTOs
│   └── TaskItemCategoryDTOs/     # Join entity DTOs (Guid keys)
└── Mapping/
    ├── TaskProfile.cs            # AutoMapper: Task mappings
    └── CategoryProfile.cs        # AutoMapper: Category mappings

Tasker.Repositories/             # Data access layer
├── Context/                      # EF Core DbContext (SQL Server)
├── Tasks/Models/                 # Task, Category, SubCategory entities
└── Repositories/                 # Repository implementations
```

## Technology stack

| Component | Version |
|---|---|
| .NET | 8.0 |
| EF Core (SQL Server) | 8.0.12 |
| JwtBearer | 8.0.12 |
| AutoMapper | 13.0.1 |
| StackExchange.Redis | 2.8.16 |
| Swashbuckle | 6.9.0 |
| AuthOrchestrator (NuGet) | 1.1.0 |

## Commands

```bash
# Build
dotnet build

# Run
dotnet run --project Tasker

# Docker
docker build -f Tasker/Dockerfile -t tasker .
```

## Integration

```
Tasker ──uses──> AuthOrchestrator (NuGet 1.1.0)
                 ├── JWT validation
                 ├── Redis session middleware
                 └── User type authorization

Tasker ──calls──> CenterAuth (user authentication)

Tasker ──publishes──> OperationalTaskSubmitted (RabbitMQ, planned)
                      └── consumed by OpsFlow
```

- **Database**: SQL Server
- **Session store**: Redis (via AuthOrchestrator)
- **Auth**: JWT tokens issued by CenterAuth, validated via AuthOrchestrator middleware

## Status

- Core CRUD: done (tasks, categories, subcategories, users)
- .NET 8 upgrade: done
- NuGet dependency management: done (AuthOrchestrator 1.1.0)
- Status field + state machine: planned (FIN-19)
- RabbitMQ event publishing: planned (FIN-55)
- Tests: not yet implemented
