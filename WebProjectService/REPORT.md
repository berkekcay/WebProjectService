# REPORT

## 1. Project Summary
This project addresses a gym operations problem: tracking members, subscriptions, workout programs, workout sessions, and body measurements in a centralized service.
It is evaluated under the instructor-approved stack agreement: .NET backend, MSSQL database, and .NET-native validation.

## 2. Business Problem
Gyms often manage memberships, training plans, and member progress manually or across disconnected tools. This causes:
- inconsistent membership status updates,
- poor trainer-member coordination,
- weak visibility into workout history and progress trends.

The goal of this project is to provide one backend service for membership lifecycle, workout planning, and progress tracking.

## 3. Scope and Capabilities
Implemented API capabilities include:
- member registration and authentication,
- admin/trainer staff creation,
- membership plan management,
- subscription assignment and renewal,
- workout program creation with exercises,
- workout session scheduling/completion,
- measurement tracking and progress chart data,
- basic financial and notification endpoints.

## 4. Architecture
- API Layer: Controllers for Auth, Members, Subscriptions, Workouts, Progress, Notifications, Finance.
- Service Layer: Business logic in dedicated service implementations.
- Data Layer: Entity Framework Core `AppDbContext` with explicit model configuration.
- Security Layer: JWT authentication and role-based authorization.
- Frontend Layer: Implemented in a separate repository and integrated through REST APIs.

## 5. Database and Data Modeling
Current database is SQL Server with EF Core migrations.

### Entities
The model contains more than 10 entities:
- User, Member, Trainer,
- MembershipPlan, Subscription,
- WorkoutProgram, Exercise, WorkoutProgramExercise,
- WorkoutSession, Measurement, Product.

### Relationships
- `User` 1:1 `Member`
- `User` 1:1 `Trainer`
- `Member` 1:N `Subscription`
- `Trainer` 1:N `WorkoutProgram`
- `Member` 1:N `WorkoutProgram`
- `WorkoutProgram` M:N `Exercise` via `WorkoutProgramExercise`
- `Member` 1:N `WorkoutSession`

## 6. Validation and Error Handling
- Request validation is handled via DataAnnotations on DTOs.
- Invalid requests automatically return 400 via `[ApiController]` behavior.
- A global exception handling middleware converts exceptions to consistent `ProblemDetails` responses.
- Known exception types are mapped to safe HTTP status codes (400, 401, 404, 409, 500).

## 7. Security and Robustness
- JWT token validation with issuer/audience/signature/lifetime checks.
- Role-based authorization (Admin, Trainer, Member).
- Structured error responses with trace IDs for observability.
- Defensive null checks and not-found handling in services.

## 8. Technology Inclusion Check Against Announcements
### Instructor-approved scope for this project
- Backend technology: ASP.NET Core Web API (.NET) - Implemented
- Database technology: SQL Server (MSSQL) - Implemented
- Validation strategy: DataAnnotations + ApiController model validation - Implemented
- Frontend organization: separate repository - Implemented
- At least 5 entities: Implemented
- More than 10 entities and advanced relationships: Implemented (includes M:N)

## 9. Prompt and Documentation Quality
This repository now includes:
- a clear README,
- this technical report,
- a responsibilities file per student number,
- screenshot references for presentation readiness.

## 10. Known Gaps and Improvement Plan
1. Add integration tests for role-based endpoint access paths.
2. Expand API observability with request-level structured correlation logging.
3. Add rate-limiting for abuse resistance on public authentication endpoints.
4. Extend financial/reporting endpoints with aggregation filters.
5. Keep backend/frontend API contracts versioned and documented in both repos.

## 11. Presentation Plan (3 Minutes)
- 0:00-0:30: Problem statement and architecture overview
- 0:30-1:30: Data model and relationships (especially M:N)
- 1:30-2:15: Validation, error handling, and security
- 2:15-2:45: Demo flow (auth, plan assignment, workout session)
- 2:45-3:00: Summary, limitations, and next steps

## 12. Conclusion
The project demonstrates strong backend engineering depth in .NET, robust API validation/error handling, and an advanced MSSQL-backed entity model. It aligns with the agreed evaluation scope and is ready for presentation and repository-based assessment.
