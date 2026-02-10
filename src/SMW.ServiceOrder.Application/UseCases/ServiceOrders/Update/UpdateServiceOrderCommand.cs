using MediatR;
using SMW.ServiceOrder.Domain.Shared;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Update;

public record UpdateServiceOrderCommand(Guid Id, string Title, string Description, IReadOnlyList<Guid> ServiceIds) : IRequest<Response<Domain.Entities.ServiceOrder>>;
