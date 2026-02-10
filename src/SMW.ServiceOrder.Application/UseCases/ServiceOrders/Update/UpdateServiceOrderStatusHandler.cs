using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SMW.ServiceOrder.Application.Adapters.Gateways.Repositories;
using SMW.ServiceOrder.Application.Adapters.Gateways.Services;
using SMW.ServiceOrder.Application.UseCases.Quotes.Update;
using SMW.ServiceOrder.Domain.Shared;
using System.Net;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Update;

public sealed class UpdateServiceOrderStatusHandler(
    IMapper mapper,
    IMediator mediator,
    IServiceOrderRepository serviceOrderRepository,
    INewRelicInstrumentationService newRelicService,
    ILogger<UpdateServiceOrderStatusHandler> logger)
    : IRequestHandler<UpdateServiceOrderStatusCommand, Response<Domain.Entities.ServiceOrder>>, INotificationHandler<UpdateQuoteStatusNotification>
{
    public async Task<Response<Domain.Entities.ServiceOrder>> Handle(UpdateServiceOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var startTime = DateTime.UtcNow;

        try
        {
            var entity = await serviceOrderRepository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
            {
                return ResponseFactory.Fail<Domain.Entities.ServiceOrder>("Service Order not found", HttpStatusCode.NotFound);
            }

            var previousStatus = entity.Status.ToString();
            _ = entity.ChangeStatus(request.Status);
            _ = await serviceOrderRepository.UpdateAsync(entity, cancellationToken);
            var response = (await serviceOrderRepository.GetDetailedAsync(request.Id, cancellationToken))!;

            var duration = DateTime.UtcNow - startTime;

            // Record custom event in New Relic
            newRelicService.RecordServiceOrderEvent(
                action: "updated",
                orderId: response.Id,
                status: response.Status.ToString(),
                customerId: response.ClientId,
                duration: duration,
                additionalAttributes: new Dictionary<string, object>
                {
                    { "previousStatus", previousStatus },
                    { "newStatus", response.Status.ToString() }
                });

            logger.LogInformation(
                "Service order {OrderId} status updated from {PreviousStatus} to {NewStatus}",
                response.Id, previousStatus, response.Status);

            await mediator.Publish(new UpdateServiceOrderStatusNotification(request.Id, response), cancellationToken);
            return ResponseFactory.Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating service order {OrderId} status to {Status}", request.Id, request.Status);
            newRelicService.NoticeError(ex, new Dictionary<string, object>
            {
                { "orderId", request.Id.ToString() },
                { "targetStatus", request.Status.ToString() },
                { "operation", "UpdateServiceOrderStatus" }
            });
            throw;
        }
    }

    public Task Handle(UpdateQuoteStatusNotification notification, CancellationToken cancellationToken) =>
        mediator.Send(new UpdateServiceOrderStatusCommand(notification.Quote.ServiceOrderId, Domain.Entities.ServiceOrder.GetNextStatus(notification.Quote.Status)), cancellationToken);
}
