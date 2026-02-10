using MediatR;
using SMW.ServiceOrder.Domain.Shared;
using SMW.ServiceOrder.Domain.ValueObjects;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Update;

public record UpdateServiceOrderStatusCommand(Guid Id, ServiceOrderStatus Status) : IRequest<Response<Domain.Entities.ServiceOrder>>;
