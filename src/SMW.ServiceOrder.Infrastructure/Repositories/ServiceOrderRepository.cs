using Microsoft.EntityFrameworkCore;
using SMW.ServiceOrder.Application.Adapters.Gateways.Repositories;
using SMW.ServiceOrder.Domain.Entities;
using SMW.ServiceOrder.Infrastructure.Data;

namespace SMW.ServiceOrder.Infrastructure.Repositories;

public sealed class ServiceOrderRepository(AppDbContext appDbContext) : Repository<Domain.Entities.ServiceOrder>(appDbContext), IServiceOrderRepository
{
    public override async Task<Domain.Entities.ServiceOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await base.GetByIdAsync(id, cancellationToken);
        return result?.SyncState();
    }

    public async Task<Domain.Entities.ServiceOrder?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Query()
            .Include(x => x.AvailableServices)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Domain.Entities.ServiceOrder?> GetDetailedAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Query()
            .Include(x => x.AvailableServices).ThenInclude(item => item.AvailableServiceSupplies).ThenInclude(x => x.Supply)
            .Include(x => x.Client)
            .Include(x => x.Vehicle)
            .Include(x => x.Quotes)
            .Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Domain.Entities.ServiceOrder> UpdateAsync(Guid id, string title, string description, IReadOnlyList<AvailableService> services, CancellationToken cancellationToken)
    {
        var entity = await Query(false)
            .Include(x => x.AvailableServices)
            .SingleAsync(x => x.Id == id, cancellationToken);

        _ = entity.Update(title, description, services);
        _ = await UpdateAsync(entity, cancellationToken);
        return entity;
    }
}
