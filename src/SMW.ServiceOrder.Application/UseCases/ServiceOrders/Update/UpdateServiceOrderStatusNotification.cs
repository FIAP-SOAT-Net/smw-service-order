using MediatR;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Update;

public record UpdateServiceOrderStatusNotification(Guid Id, Domain.Entities.ServiceOrder ServiceOrder) : INotification;
