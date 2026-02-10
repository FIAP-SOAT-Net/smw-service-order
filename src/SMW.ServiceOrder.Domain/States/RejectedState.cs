using SMW.ServiceOrder.Domain.Shared;
using SMW.ServiceOrder.Domain.ValueObjects;

namespace SMW.ServiceOrder.Domain.States;

public sealed class RejectedState : ServiceOrderState
{
    public override ServiceOrderStatus Status => ServiceOrderStatus.Rejected;

    public override void ChangeStatus(Entities.ServiceOrder serviceOrder, ServiceOrderStatus status)
    {
        if (status != ServiceOrderStatus.WaitingApproval)
        {
            throw new DomainException("Uma ordem de serviço rejeitada só pode ser alterada para aguardando aprovação.");
        }

        _ = serviceOrder.SetState(new WaitingApprovalState());
    }
}
