using MediatR;
using SMW.ServiceOrder.Domain.Shared;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Delete;

public record DeleteServiceOrderCommand(Guid Id) : IRequest<Response>;
