using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SMW.ServiceOrder.Domain.Shared;
using System.Net;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Create;

public sealed class CreateServiceOrderHandler(
    IMapper mapper,
    // IServiceOrderRepository serviceOrderRepository,
    // IPersonRepository personRepository,
    // IAvailableServiceRepository availableServiceRepository,
    // IVehicleRepository vehicleRepository,
    // INewRelicInstrumentationService newRelicService,
    ILogger<CreateServiceOrderHandler> logger) : IRequestHandler<CreateServiceOrderCommand, Response<Domain.Entities.ServiceOrder>>
{
    public async Task<Response<Domain.Entities.ServiceOrder>> Handle(CreateServiceOrderCommand request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.UtcNow;

        try
        {
            var entity = mapper.Map<Domain.Entities.ServiceOrder>(request);
            // if (!await personRepository.AnyAsync(x => x.Id == entity.ClientId, cancellationToken))
            // {
            //     return ResponseFactory.Fail<Domain.Entities.ServiceOrder>("Person not found", HttpStatusCode.NotFound);
            // }
            //
            // if (!await vehicleRepository.AnyAsync(x => x.Id == entity.VehicleId, cancellationToken))
            // {
            //     return ResponseFactory.Fail<Domain.Entities.ServiceOrder>("Vehicle not found", HttpStatusCode.NotFound);
            // }
            //
            // foreach (var serviceId in request.ServiceIds)
            // {
            //     var availableService = await availableServiceRepository.GetByIdAsync(serviceId, cancellationToken);
            //     if (availableService is null)
            //     {
            //         return ResponseFactory.Fail<Domain.Entities.ServiceOrder>($"Service with Id {serviceId} not found",
            //             HttpStatusCode.NotFound);
            //     }
            //
            //     _ = entity.AddAvailableService(availableService);
            // }
            //
            // var createdEntity = await serviceOrderRepository.AddAsync(entity, cancellationToken);

            var duration = DateTime.UtcNow - startTime;

            // Record custom event in New Relic
            // newRelicService.RecordServiceOrderEvent(
            //     action: "created",
            //     orderId: createdEntity.Id,
            //     status: createdEntity.Status.ToString(),
            //     customerId: createdEntity.ClientId,
            //     duration: duration,
            //     additionalAttributes: new Dictionary<string, object>
            //     {
            //         { "vehicleId", createdEntity.VehicleId.ToString() },
            //         { "servicesCount", request.ServiceIds.Count }
            //     });

            // logger.LogInformation(
            //     "Service order {OrderId} created successfully for customer {CustomerId} with status {Status}",
            //     createdEntity.Id, createdEntity.ClientId, createdEntity.Status);

            // return ResponseFactory.Ok(createdEntity, HttpStatusCode.Created);
            return ResponseFactory.Ok(entity, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating service order for customer {CustomerId}", request.ClientId);
            // newRelicService.NoticeError(ex, new Dictionary<string, object>
            // {
            //     { "customerId", request.ClientId.ToString() },
            //     { "operation", "CreateServiceOrder" }
            // });
            throw;
        }
    }
}
