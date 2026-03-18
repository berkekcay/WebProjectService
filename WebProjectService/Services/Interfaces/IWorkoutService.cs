using WebProjectService.Dtos.Workouts;
using WebProjectService.Entities;

namespace WebProjectService.Services.Interfaces;

public interface IWorkoutService
{
    Task<WorkoutProgram> CreateWorkoutProgramAsync(WorkoutProgram workoutProgram, IEnumerable<Guid> exerciseIds, CancellationToken cancellationToken);
    Task<Exercise> AddExerciseLibraryItemAsync(Exercise exercise, CancellationToken cancellationToken);
    Task<WorkoutProgramDetailDto?> GetWorkoutProgramDetailAsync(Guid workoutProgramId, CancellationToken cancellationToken);
    Task<WorkoutSessionResponse> ScheduleWorkoutSessionAsync(ScheduleWorkoutSessionRequest request, CancellationToken cancellationToken);
    Task<WorkoutSessionResponse> CompleteWorkoutSessionAsync(Guid workoutSessionId, int durationMinutes, string notes, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<WorkoutSessionResponse>> GetMemberWorkoutSessionsAsync(Guid memberId, CancellationToken cancellationToken);
}