using SMW.ServiceOrder.Domain.ValueObjects;

namespace SMW.ServiceOrder.Domain.Entities;

public sealed class ServiceOrderEvent : Entity
{
    private ServiceOrderEvent() { }

    public ServiceOrderEvent(Guid serviceOrderId, ServiceOrderStatus status)
    {
        ServiceOrderId = serviceOrderId;
        Status = status;
    }

    public ServiceOrderStatus Status { get; private set; }
    public Guid ServiceOrderId { get; private set; }

    public ServiceOrder ServiceOrder { get; private set; } = null!;
}
