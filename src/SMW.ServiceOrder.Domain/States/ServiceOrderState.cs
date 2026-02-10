using SMW.ServiceOrder.Domain.ValueObjects;

namespace SMW.ServiceOrder.Domain.States;

public abstract class ServiceOrderState
{
    public abstract ServiceOrderStatus Status { get; }
    public abstract void ChangeStatus(Entities.ServiceOrder serviceOrder, ServiceOrderStatus status);
}
