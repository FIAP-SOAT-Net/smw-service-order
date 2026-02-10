using SMW.ServiceOrder.Domain.Entities;

namespace SMW.ServiceOrder.Application.Adapters.Gateways.Repositories;

public interface IServiceOrderRepository : IRepository<Domain.Entities.ServiceOrder>
{
    Task<Domain.Entities.ServiceOrder?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Domain.Entities.ServiceOrder?> GetDetailedAsync(Guid id, CancellationToken cancellationToken);
    Task<Domain.Entities.ServiceOrder> UpdateAsync(Guid id, string title, string description, IReadOnlyList<AvailableService> services, CancellationToken cancellationToken);

}
