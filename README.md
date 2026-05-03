# Tasker

Task and category management microservice with hierarchical tasks, pagination, and JWT-secured REST API.

## Architecture

3-layer architecture:

```
Tasker/                          # ASP.NET Core Web API (host)
├── Controllers/                  # Task + Category endpoints
├── Program.cs                    # DI composition and middleware
└── Dockerfile

Tasker.Services/                 # Business logic
├── Interfaces/                   # Service abstractions
├── Services/                     # Task/Category logic
├── DTO/                          # Request/response DTOs
└── Pagination/                   # Pagination helpers

Tasker.Repositories/             # Data access
├── Context/                      # Separate DbContexts (TaskDbContext, CategoryDbContext)
├── Models/                       # Entity models
├── Repositories/                 # Repository implementations
└── Migrations/                   # EF Core migrations
```

## Technology stack

| Component | Version |
|---|---|
| .NET | 6.0 |
| EF Core (SQL Server) | 6.0.21 |
| AutoMapper | 12.0.1 |
| StackExchange.Redis | 2.6.122 |
| Swashbuckle | 6.5.0 |
| AuthOrchestrator | DLL reference |

## Commands

```bash
# Build
dotnet build

# Run
dotnet run --project Tasker

# Docker
docker build -f Tasker/Dockerfile -t tasker .
```

## API endpoints

- `GET/POST/PUT/DELETE /task` — task CRUD with pagination
- `GET/POST/PUT/DELETE /category` — category CRUD

## Integration

```
Tasker ──uses──> AuthOrchestrator (DLL — JWT + Redis sessions)
Tasker ──calls──> CenterAuth (HTTP — login/register/token)
Tasker ──publishes──> OpsFlow (RabbitMQ — OperationalTaskSubmitted, planned)
```

- **Database**: SQL Server
- **Session store**: Redis
- **Auth**: JWT via AuthOrchestrator

## Status

| Area | Status |
|---|---|
| Core CRUD (tasks, categories) | Done |
| JWT auth + Redis sessions | Done |
| Pagination | Done |
| Domain work (status, state machine, assignment) | Not started |
| Event publishing to OpsFlow | Not started |
| Unit/integration tests | Not started |
| Serilog integration | Not started |
