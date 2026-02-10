using System.Diagnostics.CodeAnalysis;

namespace SMW.ServiceOrder.Domain.DTOs;

[ExcludeFromCodeCoverage]
public record ServiceOrderExecutionTimeReportDto(
    int TotalOrders,
    TimeSpan AverageTotalTime,
    TimeSpan AverageAttendanceTime,
    TimeSpan AverageExecutionTime,
    TimeSpan AverageDeliveryTime);
