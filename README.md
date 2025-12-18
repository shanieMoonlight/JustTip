JustTip — README

Overview

JustTip is a small demo application (DDD / Clean Architecture style) that models employees, shifts and tips. It contains a Web API, application layer with commands/queries/handlers, and an EF Core persistence layer. The solution is intended as a compact interview/test project showing domain modeling, repository patterns, and provider-agnostic data access strategies.

Projects

- `JustTip.API` — orchestrator / ASP.NET Core host. It wires up the application and infrastructure, exposes Swagger in development and serves the built frontend static files (see `Program.cs`). The API project itself does not implement controllers for features — those live in `JustTip.Presentation`.
- `JustTip.Presentation` — ASP.NET Core controllers that act as the presentation layer. Controllers are thin and forward requests to MediatR handlers in the application layer.
- `JustTip.Application` — application layer: commands, queries, DTOs, handlers, services, validators and unit tests. Business logic and feature handlers live here.
- `JustTip.Infrastructure` — EF Core DbContext, repositories, migrations, DB initialization and other infrastructure; it contains implementations of the repository/service abstractions declared in `JustTip.Application`.
- `Tests/*` — unit tests for application and helpers (project: `JustTip.Application.Tests`).

Quick start (run API)

1. Restore and build:

   `dotnet build`

2. Run the API (default configuration loads from appsettings / environment):

   `dotnet run --project JustTip.API`

3. In development the app exposes Scalar (Swagger) at `/api/scalar` (see Program.cs). API endpoints are under `/api`.

Running tests

- Run all tests:

  `dotnet test`

Architecture and important concepts

- Clean/DDD style separation:
  - Domain entities and value objects live in `JustTip.Application.Domain`.
  - Application services, queries and commands live in `JustTip.Application`.
  - Persistence (EF Core) lives in `JustTip.Infrastructure`.
  - Controllers in `JustTip.API` call into the application layer using MediatR.

- Handlers and queries follow the pattern `Features/<Aggregate>/Qry|Cmd/<Action>` and are implemented as small, testable units.

Key functionality

- Tips and shifts tracking for employees.
- Weekly and daily summary calculations (hours worked, tip share per employee).
- Roster APIs to get current/upcoming or arbitrary week rosters.

Notable implementation choices

- Provider-agnostic data access
  - Some operations need to clip shift intervals to a date range and sum durations. To remain DB-agnostic the repository code fetches minimal columns (start/end) and performs clipping and summing in C# when provider-specific SQL is not available.
  - Example: `ShiftRepo.GetTotalSecondsForEmployeeInRangeAsync` uses a client-side fallback that sums clipped ticks and converts to seconds.

- Time handling
  - All stored timestamps are treated as UTC. Local presentation uses `IRosterUtils.ToLocalTime(...)` to convert for DTOs.

- Tip split logic
  - Tip split calculation is encapsulated in `ITipCalculatorService` / `TipCalculatorService` to simplify testing and reuse.

Database: SQLite (default) and Postgres (optional)

- Default: SQLite (in-memory) — the infrastructure installer in this repo uses the SQLite setup by default (`PersistenceSetup_SqlLite`). That means the app runs without a Postgres server and the DB schema is created automatically at startup using `EnsureCreated()` (see `JustTip.Infrastructure/AppImps/DbInitializer.cs`).
- Optional: Postgres — a Postgres setup is available (`AddPersistenceEf_PG`) if you want a production-like backend. The Postgres registration lives in `PersistenceSetup_PG.AddPersistenceEf_PG` and can be selected by changing the infrastructure registration in `JustTip.Infrastructure/InfrastructureSetup.cs`.

Quick notes — SQLite is the default (no DB install required)

1. No change required: the solution is configured to use the in-memory SQLite provider by default (see `JustTip.Infrastructure/InfrastructureSetup.cs` which calls `AddPersistenceEf_SqlLite()`).
2. `DbInitializer.MigrateAsync()` detects SQLite and calls `EnsureCreatedAsync()` so the schema is created automatically for demo/test runs.
3. To use Postgres instead, edit `JustTip.Infrastructure/InfrastructureSetup.cs` and replace the call to `AddPersistenceEf_SqlLite()` with `AddPersistenceEf_PG(connectionString)` (pass your Postgres connection string). The Postgres setup uses `UseNpgsql(...)`.

Migrations note

- The maintenance endpoint (`POST /api/maintenance/InitializeDb`) will trigger database creation/migration for the currently configured provider — it calls the initializer which runs `Migrate()` for Postgres or `EnsureCreated()` for SQLite. Use this in lieu of manual `dotnet ef database update` during demos.

Seed and initialization

- The app uses `DbInitializer` to migrate/create the database and optionally seed employees & tips. See `JustTip.Infrastructure/AppImps/DbInitializer.cs`.

Maintenance endpoint (reseed / migrate)

- For convenience the solution includes a maintenance command/handler that seeds test data and runs migrations: `JustTip.Application.Features.Maintenance.Initialize.InitializeDbCmdHandler`.
- The presentation exposes a maintenance endpoint you can call to trigger migration and reseed: `POST /api/maintenance/InitializeDb` handled by `JustTip.Presentation.MaintenanceController.InitializeDb`.
  This is useful for demos — it will call the initializer which uses `EnsureCreated()` for SQLite or `Migrate()` for Postgres and then seed sample employees, shifts and tips.

Presentation controllers & important endpoints

Controllers live in the `JustTip.Presentation` project and forward requests to the application layer via MediatR. The host (`JustTip.API`) serves these controllers and also serves the built frontend static files.

Common controllers / routes (implemented in `JustTip.Presentation`):

- `TipsController` (presentation) exposes endpoints for:
  - `GET /api/tips/gettotalscurrentweek` — total tips for current week (handler supports week numbers)
  - `GET /api/tips/{id}` — get tip by id
  - `GET /api/tips/getall` — list all tips
  - `GET /api/tips/getalltipsbyweek/{weekNumber}` — get tips for a given week (0 current week, 1 last week, ...)

- `RosterController` (presentation) exposes roster endpoints:
  - `GET /api/roster/current-week` — current week roster
  - `GET /api/roster/upcoming-week` — upcoming week roster
  - `GET /api/roster/by-week/{weekNumber}` — roster for a given week offset

- `EmployeesController` (presentation) contains employee management endpoints and shift add/remove actions.

Note: `JustTip.API` acts as the host/orchestrator and also serves the SPA static files configured in `Program.cs` using `UsePrerenderedSpa`.

Testing approach

- Handlers and validator classes are unit-tested with Moq and Shouldly under `Tests/JustTip.Application.Tests`.
- Tests exercise both success and failure paths (NotFound, validation failures, calculation correctness).

Developer tips

- When adding DB-level aggregation that relies on SQL-specific functions (datediff, extract epoch etc.), prefer adding a provider-specific fast path and a safe client-side fallback. See `ShiftRepo` for the pattern.
- Keep datetime normalization central: use `DateExtensions.ToUtcDate(...)` before storing or comparing input datetimes.
- Encapsulate domain logic in small services (e.g., `ITipCalculatorService`) to make handlers thin and testable.

Troubleshooting

- SQLite can occasionally report a "database is locked" error (concurrent connections or long-running transactions). A common remediation is to restart the application so the in-memory/file connection is released, then call the maintenance initialize endpoint to recreate/seed the DB: `POST /api/maintenance/InitializeDb`. See https://www.beekeeperstudio.io/blog/how-to-solve-sqlite-database-is-locked-error for background and fixes.

Extending the project

- Add more endpoints by following the feature folder pattern: `Features/<Aggregate>/Qry|Cmd/<Action>` and create request/handler/validator/tests accordingly.
- Add provider-specific migrations if you need deterministic DB schema updates in production.

Contact / Author

- This demo was prepared as an interview/test project.

License

- MIT (adjust as needed)
