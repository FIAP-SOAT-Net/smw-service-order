using SMW.ServiceOrder.Domain.Shared;
using SMW.ServiceOrder.Domain.ValueObjects;

namespace SMW.ServiceOrder.Domain.States;

public sealed class DeliveredState : ServiceOrderState
{
    public override ServiceOrderStatus Status => ServiceOrderStatus.Delivered;

    public override void ChangeStatus(Entities.ServiceOrder serviceOrder, ServiceOrderStatus status)
    {
        if (status != Status)
        {
            throw new DomainException("Uma ordem de serviço entregue não pode ser alterada para outro status.");
        }
    }
}
