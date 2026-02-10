using SMW.ServiceOrder.Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace SMW.ServiceOrder.Application.Models;

[ExcludeFromCodeCoverage]
public record PatchServiceOrderRequest(ServiceOrderStatus Status);
