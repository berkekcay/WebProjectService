using WebProjectService.Dtos.Measurements;
using WebProjectService.Models;

namespace WebProjectService.Services.Interfaces;

public interface IProgressService
{
    Task AddMeasurementAsync(MeasurementAddRequest request, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<ProgressDataPoint>> GetProgressChartDataAsync(int memberId, CancellationToken cancellationToken);
}