using MediatR;
using SMW.ServiceOrder.Application.Adapters.Gateways.Repositories;
using SMW.ServiceOrder.Domain.Shared;
using System.Net;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Delete;

public sealed class DeleteServiceOrderHandler(IServiceOrderRepository serviceOrderRepository) : IRequestHandler<DeleteServiceOrderCommand, Response>
{
    public async Task<Response> Handle(DeleteServiceOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await serviceOrderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return ResponseFactory.Fail("Service Order not found", HttpStatusCode.NotFound);
        }

        await serviceOrderRepository.DeleteAsync(entity, cancellationToken);
        return ResponseFactory.Ok(HttpStatusCode.NoContent);
    }
}
