using SMW.ServiceOrder.Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace SMW.ServiceOrder.Domain.DTOs;

[ExcludeFromCodeCoverage]
public record QuoteDto(Guid Id, decimal Total, QuoteStatus Status, Guid ServiceOrderId);
