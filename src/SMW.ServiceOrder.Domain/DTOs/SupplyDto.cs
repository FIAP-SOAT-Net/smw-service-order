using System.Diagnostics.CodeAnalysis;

namespace SMW.ServiceOrder.Domain.DTOs;

[ExcludeFromCodeCoverage]
public record SupplyDto(Guid Id, string Name, int Quantity, decimal Price);
