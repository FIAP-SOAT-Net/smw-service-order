using MediatR;
using SMW.ServiceOrder.Application.Adapters.Gateways.Repositories;
using SMW.ServiceOrder.Application.UseCases.ServiceOrders.Update;
using SMW.ServiceOrder.Domain.Entities;
using SMW.ServiceOrder.Domain.ValueObjects;

namespace SMW.ServiceOrder.Application.UseCases.Quotes.Create;

public sealed class CreateQuoteHandler(IQuoteRepository quoteRepository) : INotificationHandler<UpdateServiceOrderStatusNotification>
{
    public async Task Handle(UpdateServiceOrderStatusNotification notification, CancellationToken cancellationToken)
    {
        var serviceOrder = notification.ServiceOrder;
        if (serviceOrder.Status != ServiceOrderStatus.WaitingApproval) return;
        if (!serviceOrder.AvailableServices.Any()) return;

        var quote = new Quote(serviceOrder.Id);
        serviceOrder.AvailableServices.ToList().ForEach(availableService => quote.AddService(availableService.Id, availableService.Price));
        serviceOrder.AvailableServices.SelectMany(x => x.AvailableServiceSupplies).ToList().ForEach(availableServiceSupply => quote.AddSupply(availableServiceSupply.SupplyId, availableServiceSupply.Supply.Price, availableServiceSupply.Supply.Quantity));
        _ = await quoteRepository.AddAsync(quote, cancellationToken);
    }
}
