# Frontend Repository Integration Guide

This backend is designed to be consumed by a separate frontend repository.

## 1. Frontend Repository Metadata
- Frontend repo URL: PASTE_FRONTEND_GITHUB_URL_HERE
- Suggested stack: React + Vite
- API base URL (local): http://64.226.125.254

## 2. Required Frontend Environment Variables
Create a `.env` file in the frontend repo with:

VITE_API_BASE_URL=http://localhost:8080

## 3. Core API Flows to Integrate
- Authentication
  - POST /api/auth/register
  - POST /api/auth/login
- Membership and Subscription
  - GET /api/subscriptions/plans
  - POST /api/subscriptions/select-plan
  - POST /api/subscriptions/assign-by-trainer
- Workout
  - POST /api/workouts/programs
  - POST /api/workouts/sessions
  - PUT /api/workouts/sessions/{workoutSessionId}/complete
  - GET /api/workouts/members/{memberId}/sessions
- Progress
  - POST /api/progress/measurements
  - GET /api/progress/members/{memberId}/chart

## 4. Authentication Contract
- JWT token is returned by login/register endpoints.
- Frontend should store token securely and send:
  Authorization: Bearer <token>

## 5. Error Handling Contract
The backend returns RFC7807-style ProblemDetails JSON on errors.
Frontend should handle these fields when available:
- title
- detail
- status
- traceId (in extensions)

## 6. Demo Flow for Presentation
1. Login as member/trainer/admin.
2. Retrieve available membership plans.
3. Schedule a workout session.
4. Complete the session.
5. Submit and view progress data.
