using WebProjectService.Dtos.Workouts;
using WebProjectService.Entities;

namespace WebProjectService.Services.Interfaces;

public interface IWorkoutService
{
    Task<WorkoutProgram> CreateWorkoutProgramAsync(WorkoutProgram workoutProgram, IEnumerable<int> exerciseIds, CancellationToken cancellationToken);
    Task<Exercise> AddExerciseLibraryItemAsync(Exercise exercise, CancellationToken cancellationToken);
    Task<WorkoutProgramDetailDto?> GetWorkoutProgramDetailAsync(int workoutProgramId, CancellationToken cancellationToken);
    Task<WorkoutSessionResponse> ScheduleWorkoutSessionAsync(ScheduleWorkoutSessionRequest request, CancellationToken cancellationToken);
    Task<WorkoutSessionResponse> CompleteWorkoutSessionAsync(int workoutSessionId, int durationMinutes, string notes, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<WorkoutSessionResponse>> GetMemberWorkoutSessionsAsync(int memberId, CancellationToken cancellationToken);
}