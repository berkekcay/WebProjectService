# WebProjectService

Gym management backend service built with ASP.NET Core and Entity Framework Core.

## Submission Quick Links
- Backend GitHub Repository URL: `https://github.com/berkekcay/WebProjectService`
- Frontend GitHub Repository URL: `PASTE_FRONTEND_GITHUB_URL_HERE`

## Requirement Coverage Matrix (Instructor-Approved Scope)
| Requirement | Status | Notes |
|---|---|---|
| .NET backend | Met | ASP.NET Core Web API with layered architecture. |
| MSSQL database | Met | SQL Server with EF Core migrations. |
| Backend validation | Met | DataAnnotations + ApiController model validation. |
| Frontend as separate repository | Met | Frontend is maintained in a separate repo link above. |
| At least 5 entities | Met | 11+ entities exist in the domain model. |
| More than 10 entities / advanced relationships | Met | Includes M:N relationship via `WorkoutProgramExercise`. |

## Tech Stack
- .NET 10 (ASP.NET Core Web API)
- Entity Framework Core
- SQL Server (local/docker)
- JWT authentication and role-based authorization
- Swagger / OpenAPI

## Frontend Repository Note
The frontend is intentionally versioned in a separate repository per course agreement.
This backend exposes REST endpoints that are consumed by that frontend.

## Core Domain Entities
- User
- Member
- Trainer
- MembershipPlan
- Subscription
- WorkoutProgram
- Exercise
- WorkoutProgramExercise (M:N join entity)
- WorkoutSession
- Measurement
- Product

## Security, Validation, and Robustness
- Global exception handling middleware with standardized `ProblemDetails` responses
- Request validation with DataAnnotations and automatic 400 responses
- Role-based authorization policies (Admin, Trainer, Member)
- JWT validation with strict lifetime checks and zero clock skew

## API Contract for Frontend Integration
- Auth: register/login/staff creation
- Memberships: member status and freeze operations
- Subscriptions: plan list/create/select/assign/renew
- Workouts: program creation, session scheduling/completion, member session list
- Progress: measurement add and chart data
- Notifications/Finance: admin and trainer operational endpoints

## How to Run
### Option 1: Local Development
1. Update connection string and JWT values in `appsettings.json`.
2. Run:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

### Option 2: Docker Compose
1. Set environment variables (`SA_PASSWORD`, `JWT_KEY`).
2. Run:
   ```bash
   docker compose up --build
   ```
3. API is available on `http://localhost:8080`.


## Presentation Checklist
- Keep presentation duration near **3:00**.
- Cover backend architecture, MSSQL schema, validation flow, entities, and relationships.
- Mention frontend is in separate repository and show integrated flow.
- Use technical terms naturally and avoid reading from slides.
- Upload one PDF or PPTX and include the correct GitHub URL.
