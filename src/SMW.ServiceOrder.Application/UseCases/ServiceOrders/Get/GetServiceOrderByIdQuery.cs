using MediatR;
using SMW.ServiceOrder.Domain.Shared;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Get;

public record GetServiceOrderByIdQuery(Guid Id) : IRequest<Response<Domain.Entities.ServiceOrder>>;
