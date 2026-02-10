using MediatR;
using SMW.ServiceOrder.Application.Adapters.Gateways.Repositories;
using SMW.ServiceOrder.Domain.Shared;
using System.Net;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Get;

public sealed class GetServiceOrderByIdHandler(IServiceOrderRepository repository) : IRequestHandler<GetServiceOrderByIdQuery, Response<Domain.Entities.ServiceOrder>>
{
    public async Task<Response<Domain.Entities.ServiceOrder>> Handle(GetServiceOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetDetailedAsync(request.Id, cancellationToken);
        return entity != null
            ? ResponseFactory.Ok(entity)
            : ResponseFactory.Fail<Domain.Entities.ServiceOrder>("Service Order Not Found", HttpStatusCode.NotFound);
    }
}
