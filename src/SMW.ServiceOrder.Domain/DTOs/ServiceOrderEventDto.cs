using SMW.ServiceOrder.Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace SMW.ServiceOrder.Domain.DTOs;

[ExcludeFromCodeCoverage]
public record ServiceOrderEventDto(Guid Id, ServiceOrderStatus Status, DateTime CreatedAt);
