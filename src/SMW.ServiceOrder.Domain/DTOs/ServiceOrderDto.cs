using SMW.ServiceOrder.Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace SMW.ServiceOrder.Domain.DTOs;

[ExcludeFromCodeCoverage]
public record ServiceOrderDto
{
    public Guid Id { get; init; }
    public ServiceOrderStatus Status { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Guid ClientId { get; init; }
    public Guid VehicleId { get; init; }
    public PersonDto Client { get; init; } = null!;
    public VehicleDto Vehicle { get; init; } = null!;
    public ICollection<AvailableServiceDto> AvailableServices { get; init; } = [];
    public ICollection<ServiceOrderEventDto> Events { get; init; } = [];
    public ICollection<QuoteDto> Quotes { get; init; } = [];
}
