using SMW.ServiceOrder.Domain.DTOs;
using SMW.ServiceOrder.Domain.Entities;

namespace SMW.ServiceOrder.Application.Adapters.Gateways.Repositories;

public interface IServiceOrderEventRepository : IRepository<ServiceOrderEvent>
{
    Task<ServiceOrderExecutionTimeReportDto> GetAverageExecutionTimesAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
}
