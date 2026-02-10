using MediatR;
using SMW.ServiceOrder.Domain.Shared;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.List;

public record ListServiceOrdersQuery(int PageNumber, int PageSize, Guid? PersonId) : IRequest<Response<Paginate<Domain.Entities.ServiceOrder>>>;
