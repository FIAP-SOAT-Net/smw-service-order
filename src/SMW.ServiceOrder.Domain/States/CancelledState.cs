using SMW.ServiceOrder.Domain.Shared;
using SMW.ServiceOrder.Domain.ValueObjects;

namespace SMW.ServiceOrder.Domain.States;

public sealed class CancelledState : ServiceOrderState
{
    public override ServiceOrderStatus Status => ServiceOrderStatus.Cancelled;

    public override void ChangeStatus(Entities.ServiceOrder serviceOrder, ServiceOrderStatus status)
    {
        if (status != ServiceOrderStatus.Delivered)
        {
            throw new DomainException("Uma ordem de serviço cancelada só pode ser alterada para entregue.");
        }

        _ = serviceOrder.SetState(new DeliveredState());
    }
}
