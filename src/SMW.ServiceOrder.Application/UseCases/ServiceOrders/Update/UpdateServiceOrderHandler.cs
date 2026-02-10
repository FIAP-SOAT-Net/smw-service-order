using MediatR;
using Microsoft.Extensions.Logging;
using SMW.ServiceOrder.Application.Adapters.Gateways.Repositories;
using SMW.ServiceOrder.Domain.Entities;
using SMW.ServiceOrder.Domain.Shared;
using System.Net;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Update;

public sealed class UpdateServiceOrderHandler(
    IServiceOrderRepository serviceOrderRepository,
    // IAvailableServiceRepository availableServiceRepository,
    ILogger<UpdateServiceOrderHandler> logger) : IRequestHandler<UpdateServiceOrderCommand, Response<Domain.Entities.ServiceOrder>>
{
    public async Task<Response<Domain.Entities.ServiceOrder>> Handle(UpdateServiceOrderCommand request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.UtcNow;

        try
        {
            var entity = await serviceOrderRepository.GetAsync(request.Id, cancellationToken);
            if (entity is null)
            {
                return ResponseFactory.Fail<Domain.Entities.ServiceOrder>("Service Order not found", HttpStatusCode.NotFound);
            }

            var services = new List<AvailableService>();
            foreach (var service in request.ServiceIds)
            {
                // var foundService = await availableServiceRepository.GetByIdAsync(service, cancellationToken);
                // if (foundService is null)
                // {
                //     return ResponseFactory.Fail<Domain.Entities.ServiceOrder>($"Available Service with ID {service} not found",
                //         HttpStatusCode.NotFound);
                // }

                // services.Add(foundService);
            }

            var updatedEntity = await serviceOrderRepository.UpdateAsync(request.Id, request.Title, request.Description, services, cancellationToken);

            logger.LogInformation(
                "Service order {OrderId} modified successfully with {ServicesCount} services",
                updatedEntity.Id, request.ServiceIds.Count);

            return ResponseFactory.Ok(updatedEntity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating service order {OrderId}", request.Id);
            throw;
        }
    }
}
