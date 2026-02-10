using MediatR;
using SMW.ServiceOrder.Domain.Shared;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Create;

public record CreateServiceOrderCommand(Guid ClientId, Guid VehicleId, IReadOnlyList<Guid> ServiceIds, string Title, string Description)
    : IRequest<Response<Domain.Entities.ServiceOrder>>;
