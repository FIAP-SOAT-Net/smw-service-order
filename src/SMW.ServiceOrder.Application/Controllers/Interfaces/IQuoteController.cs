using Microsoft.AspNetCore.Mvc;
using SMW.ServiceOrder.Domain.ValueObjects;

namespace SMW.ServiceOrder.Application.Controllers.Interfaces;

public interface IQuoteController
{
    Task<IActionResult> PatchQuoteAsync(Guid id, Guid quoteId, QuoteStatus status, CancellationToken cancellationToken);
}
