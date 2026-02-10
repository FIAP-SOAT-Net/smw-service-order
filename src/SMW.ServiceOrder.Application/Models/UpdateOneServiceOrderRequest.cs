using System.Diagnostics.CodeAnalysis;

namespace SMW.ServiceOrder.Application.Models;

[ExcludeFromCodeCoverage]
public record UpdateOneServiceOrderRequest(IReadOnlyList<Guid> ServiceIds, string Title, string Description);
