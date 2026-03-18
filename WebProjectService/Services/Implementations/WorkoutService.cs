using Microsoft.EntityFrameworkCore;
using WebProjectService.Data;
using WebProjectService.Dtos.Workouts;
using WebProjectService.Entities;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Services.Implementations;

public class WorkoutService(AppDbContext context) : IWorkoutService
{
    public async Task<WorkoutProgram> CreateWorkoutProgramAsync(WorkoutProgram workoutProgram, IEnumerable<int> exerciseIds, CancellationToken cancellationToken)
    {
        var ids = exerciseIds.ToList();
        var exercises = await context.Exercises.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

        if (exercises.Count != ids.Count)
        {
            throw new InvalidOperationException("One or more exercises were not found.");
        }

        workoutProgram.ProgramExercises = exercises
            .Select((exercise, index) => new WorkoutProgramExercise
            {
                ExerciseId = exercise.Id,
                Order = index + 1
            })
            .ToList();

        context.WorkoutPrograms.Add(workoutProgram);
        await context.SaveChangesAsync(cancellationToken);

        return workoutProgram;
    }

    public async Task<Exercise> AddExerciseLibraryItemAsync(Exercise exercise, CancellationToken cancellationToken)
    {
        context.Exercises.Add(exercise);
        await context.SaveChangesAsync(cancellationToken);
        return exercise;
    }

    public async Task<WorkoutProgramDetailDto?> GetWorkoutProgramDetailAsync(int workoutProgramId, CancellationToken cancellationToken)
    {
        return await context.WorkoutPrograms
            .AsNoTracking()
            .Where(x => x.Id == workoutProgramId)
            .Select(x => new WorkoutProgramDetailDto
            {
                ProgramId = x.Id,
                MemberId = x.MemberId,
                TrainerId = x.TrainerId,
                ProgramName = x.ProgramName,
                DifficultyLevel = x.DifficultyLevel,
                Exercises = x.ProgramExercises
                    .OrderBy(pe => pe.Order)
                    .Select(pe => new ExerciseDto
                    {
                        Id = pe.Exercise.Id,
                        Name = pe.Exercise.Name,
                        MuscleGroup = pe.Exercise.MuscleGroup,
                        Description = pe.Exercise.Description,
                        VideoUrl = pe.Exercise.VideoUrl
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<WorkoutSessionResponse> ScheduleWorkoutSessionAsync(ScheduleWorkoutSessionRequest request, CancellationToken cancellationToken)
    {
        var memberExists = await context.Members.AsNoTracking().AnyAsync(x => x.Id == request.MemberId, cancellationToken);
        if (!memberExists)
        {
            throw new KeyNotFoundException("Member not found.");
        }

        if (request.TrainerId.HasValue)
        {
            var trainerExists = await context.Trainers.AsNoTracking().AnyAsync(x => x.Id == request.TrainerId.Value, cancellationToken);
            if (!trainerExists)
            {
                throw new KeyNotFoundException("Trainer not found.");
            }
        }

        if (request.WorkoutProgramId.HasValue)
        {
            var programExists = await context.WorkoutPrograms.AsNoTracking().AnyAsync(x => x.Id == request.WorkoutProgramId.Value, cancellationToken);
            if (!programExists)
            {
                throw new KeyNotFoundException("Workout program not found.");
            }
        }

        var session = new WorkoutSession
        {
            MemberId = request.MemberId,
            TrainerId = request.TrainerId,
            WorkoutProgramId = request.WorkoutProgramId,
            ScheduledDate = request.ScheduledDate,
            DurationMinutes = request.DurationMinutes,
            Notes = request.Notes,
            Status = WorkoutSessionStatus.Scheduled
        };

        context.WorkoutSessions.Add(session);
        await context.SaveChangesAsync(cancellationToken);

        return MapSession(session);
    }

    public async Task<WorkoutSessionResponse> CompleteWorkoutSessionAsync(int workoutSessionId, int durationMinutes, string notes, CancellationToken cancellationToken)
    {
        var session = await context.WorkoutSessions.FirstOrDefaultAsync(x => x.Id == workoutSessionId, cancellationToken)
            ?? throw new KeyNotFoundException("Workout session not found.");

        session.Status = WorkoutSessionStatus.Completed;
        session.CompletedDate = DateTime.UtcNow;
        session.DurationMinutes = durationMinutes;
        if (!string.IsNullOrWhiteSpace(notes))
        {
            session.Notes = notes;
        }

        await context.SaveChangesAsync(cancellationToken);
        return MapSession(session);
    }

    public async Task<IReadOnlyCollection<WorkoutSessionResponse>> GetMemberWorkoutSessionsAsync(int memberId, CancellationToken cancellationToken)
    {
        return await context.WorkoutSessions
            .AsNoTracking()
            .Where(x => x.MemberId == memberId)
            .OrderByDescending(x => x.ScheduledDate)
            .Select(x => new WorkoutSessionResponse
            {
                Id = x.Id,
                MemberId = x.MemberId,
                TrainerId = x.TrainerId,
                WorkoutProgramId = x.WorkoutProgramId,
                ScheduledDate = x.ScheduledDate,
                CompletedDate = x.CompletedDate,
                DurationMinutes = x.DurationMinutes,
                Notes = x.Notes,
                Status = x.Status
            })
            .ToListAsync(cancellationToken);
    }

    private static WorkoutSessionResponse MapSession(WorkoutSession session)
    {
        return new WorkoutSessionResponse
        {
            Id = session.Id,
            MemberId = session.MemberId,
            TrainerId = session.TrainerId,
            WorkoutProgramId = session.WorkoutProgramId,
            ScheduledDate = session.ScheduledDate,
            CompletedDate = session.CompletedDate,
            DurationMinutes = session.DurationMinutes,
            Notes = session.Notes,
            Status = session.Status
        };
    }
}