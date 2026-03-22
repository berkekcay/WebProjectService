using WebProjectService.Dtos.Workouts;
using WebProjectService.Entities;
using WebProjectService.Entities.Enums;

namespace WebProjectService.Services.Interfaces;

public interface IWorkoutService
{
    Task<WorkoutProgram> CreateWorkoutProgramAsync(WorkoutProgram workoutProgram, IEnumerable<int> exerciseIds, CancellationToken cancellationToken);
    Task<Exercise> AddExerciseLibraryItemAsync(Exercise exercise, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<WorkoutProgramDetailDto>> GetWorkoutProgramsAsync(int? memberId, int? trainerId, CancellationToken cancellationToken);
    Task<WorkoutProgramDetailDto?> GetWorkoutProgramDetailAsync(int workoutProgramId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<ExerciseDto>> GetExercisesAsync(CancellationToken cancellationToken);
    Task<WorkoutSessionResponse> ScheduleWorkoutSessionAsync(ScheduleWorkoutSessionRequest request, CancellationToken cancellationToken);
    Task<WorkoutSessionResponse> CompleteWorkoutSessionAsync(int workoutSessionId, int durationMinutes, string notes, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<WorkoutSessionResponse>> GetWorkoutSessionsAsync(int? memberId, int? trainerId, WorkoutSessionStatus? status, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<WorkoutSessionResponse>> GetMemberWorkoutSessionsAsync(int memberId, CancellationToken cancellationToken);
}