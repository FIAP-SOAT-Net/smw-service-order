using System.Diagnostics.CodeAnalysis;

namespace SMW.ServiceOrder.Domain.DTOs;

[ExcludeFromCodeCoverage]
public record AvailableServiceDto(Guid Id, string Name, decimal Price, IReadOnlyList<SupplyDto> Supplies);
