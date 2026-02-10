using MediatR;
using SMW.ServiceOrder.Application.Adapters.Gateways.Repositories;
using SMW.ServiceOrder.Domain.Shared;
using SMW.ServiceOrder.Domain.ValueObjects;
using System.Linq.Expressions;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.List;

public sealed class ListServiceOrdersHandler(IServiceOrderRepository serviceOrderRepository)
    : IRequestHandler<ListServiceOrdersQuery, Response<Paginate<Domain.Entities.ServiceOrder>>>
{
    public async Task<Response<Paginate<Domain.Entities.ServiceOrder>>> Handle(ListServiceOrdersQuery request, CancellationToken cancellationToken)
    {
        string[] includes = [nameof(Domain.Entities.ServiceOrder.Client), nameof(Domain.Entities.ServiceOrder.Vehicle), nameof(Domain.Entities.ServiceOrder.AvailableServices)];
        var paginatedRequest = new PaginatedRequest(request.PageNumber, request.PageSize);
        Func<IQueryable<Domain.Entities.ServiceOrder>, IOrderedQueryable<Domain.Entities.ServiceOrder>> orderBy = q =>
            q.OrderBy(x =>

                x.Status == ServiceOrderStatus.InProgress ? 1 :
                x.Status == ServiceOrderStatus.WaitingApproval ? 2 :
                x.Status == ServiceOrderStatus.UnderDiagnosis ? 3 :
                x.Status == ServiceOrderStatus.Received ? 4 : 5
            )
            .ThenBy(x => x.CreatedAt);

        var excludedStatuses = new[] { ServiceOrderStatus.Delivered, ServiceOrderStatus.Completed, ServiceOrderStatus.Cancelled, ServiceOrderStatus.Rejected };

        Expression<Func<Domain.Entities.ServiceOrder, bool>> predicate = request.PersonId.HasValue
        ? x => x.ClientId == request.PersonId && !excludedStatuses.Contains(x.Status)
        : x => !excludedStatuses.Contains(x.Status);

        var response = await serviceOrderRepository.GetAllAsync(includes, predicate, paginatedRequest, cancellationToken, orderBy);
        return ResponseFactory.Ok(response);
    }
}
