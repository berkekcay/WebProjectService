using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProjectService.Dtos.Workouts;
using WebProjectService.Entities;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutsController(IWorkoutService workoutService) : ControllerBase
{
    [HttpGet("programs")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)},{nameof(Role.Member)}")]
    public async Task<IActionResult> GetPrograms(
        [FromQuery] int? memberId,
        [FromQuery] int? trainerId,
        CancellationToken cancellationToken)
    {
        var programs = await workoutService.GetWorkoutProgramsAsync(memberId, trainerId, cancellationToken);
        return Ok(programs);
    }

    [HttpPost("programs")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> CreateProgram([FromBody] CreateWorkoutProgramRequest request, CancellationToken cancellationToken)
    {
        var workoutProgram = new WorkoutProgram
        {
            MemberId = request.MemberId,
            TrainerId = request.TrainerId,
            ProgramName = request.ProgramName,
            DifficultyLevel = request.DifficultyLevel
        };

        var createdProgram = await workoutService.CreateWorkoutProgramAsync(workoutProgram, request.ExerciseIds, cancellationToken);
        return CreatedAtAction(nameof(GetProgramDetail), new { workoutProgramId = createdProgram.Id }, createdProgram);
    }

    [HttpGet("programs/{workoutProgramId:int}")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)},{nameof(Role.Member)}")]
    public async Task<IActionResult> GetProgramDetail(int workoutProgramId, CancellationToken cancellationToken)
    {
        var detail = await workoutService.GetWorkoutProgramDetailAsync(workoutProgramId, cancellationToken);
        return detail is null ? NotFound() : Ok(detail);
    }

    [HttpPost("exercises")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> AddExercise([FromBody] AddExerciseRequest request, CancellationToken cancellationToken)
    {
        var exercise = new Exercise
        {
            Name = request.Name,
            MuscleGroup = request.MuscleGroup,
            Description = request.Description,
            VideoUrl = request.VideoUrl
        };

        var createdExercise = await workoutService.AddExerciseLibraryItemAsync(exercise, cancellationToken);
        return Ok(createdExercise);
    }

    [HttpGet("exercises")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)},{nameof(Role.Member)}")]
    public async Task<IActionResult> GetExercises(CancellationToken cancellationToken)
    {
        var exercises = await workoutService.GetExercisesAsync(cancellationToken);
        return Ok(exercises);
    }

    [HttpPost("sessions")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> ScheduleSession([FromBody] ScheduleWorkoutSessionRequest request, CancellationToken cancellationToken)
    {
        var createdSession = await workoutService.ScheduleWorkoutSessionAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetMemberSessions), new { memberId = createdSession.MemberId }, createdSession);
    }

    [HttpPut("sessions/{workoutSessionId:int}/complete")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> CompleteSession(
        int workoutSessionId,
        [FromBody] CompleteWorkoutSessionRequest request,
        CancellationToken cancellationToken)
    {
        var session = await workoutService.CompleteWorkoutSessionAsync(
            workoutSessionId,
            request.DurationMinutes,
            request.Notes,
            cancellationToken);

        return Ok(session);
    }

    [HttpGet("members/{memberId:int}/sessions")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)},{nameof(Role.Member)}")]
    public async Task<IActionResult> GetMemberSessions(int memberId, CancellationToken cancellationToken)
    {
        var sessions = await workoutService.GetMemberWorkoutSessionsAsync(memberId, cancellationToken);
        return Ok(sessions);
    }

    [HttpGet("sessions")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> GetSessions(
        [FromQuery] int? memberId,
        [FromQuery] int? trainerId,
        [FromQuery] WorkoutSessionStatus? status,
        CancellationToken cancellationToken)
    {
        var sessions = await workoutService.GetWorkoutSessionsAsync(memberId, trainerId, status, cancellationToken);
        return Ok(sessions);
    }
}