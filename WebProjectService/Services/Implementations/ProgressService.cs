using Microsoft.EntityFrameworkCore;
using WebProjectService.Data;
using WebProjectService.Dtos.Measurements;
using WebProjectService.Entities;
using WebProjectService.Models;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Services.Implementations;

public class ProgressService(AppDbContext context) : IProgressService
{
    public async Task AddMeasurementAsync(MeasurementAddRequest request, CancellationToken cancellationToken)
    {
        var measurement = new Measurement
        {
            MemberId = request.MemberId,
            Weight = request.Weight,
            Height = request.Height,
            BodyFatPercentage = request.BodyFatPercentage,
            Chest = request.Chest,
            Waist = request.Waist,
            Arm = request.Arm,
            Leg = request.Leg,
            MeasurementDate = request.MeasurementDate
        };

        context.Measurements.Add(measurement);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<ProgressDataPoint>> GetProgressChartDataAsync(int memberId, CancellationToken cancellationToken)
    {
        return await context.Measurements
            .AsNoTracking()
            .Where(x => x.MemberId == memberId)
            .OrderBy(x => x.MeasurementDate)
            .Select(x => new ProgressDataPoint
            {
                Date = x.MeasurementDate,
                Weight = x.Weight,
                BodyFatPercentage = x.BodyFatPercentage
            })
            .ToListAsync(cancellationToken);
    }
}