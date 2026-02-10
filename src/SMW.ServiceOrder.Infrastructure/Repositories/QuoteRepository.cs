using Microsoft.EntityFrameworkCore;
using SMW.ServiceOrder.Application.Adapters.Gateways.Repositories;
using SMW.ServiceOrder.Domain.Entities;
using SMW.ServiceOrder.Infrastructure.Data;

namespace SMW.ServiceOrder.Infrastructure.Repositories;

public sealed class QuoteRepository(AppDbContext appDbContext) : Repository<Quote>(appDbContext), IQuoteRepository
{
    public override Task<Quote?> GetDetailedByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Query().Where(x => x.Id == id)
            .Include(x => x.Supplies).ThenInclude(x => x.Supply)
            .Include(x => x.Services).ThenInclude(x => x.Service)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
