using SMW.ServiceOrder.Domain.Shared;
using SMW.ServiceOrder.Domain.ValueObjects;

namespace SMW.ServiceOrder.Domain.States;

public class UnderDiagnosisState : ServiceOrderState
{
    public override ServiceOrderStatus Status => ServiceOrderStatus.UnderDiagnosis;

    public override void ChangeStatus(Entities.ServiceOrder serviceOrder, ServiceOrderStatus status)
    {
        _ = status switch
        {
            ServiceOrderStatus.WaitingApproval => serviceOrder.SetState(new WaitingApprovalState()),
            ServiceOrderStatus.Cancelled => serviceOrder.SetState(new CancelledState()),
            _ => throw new DomainException("Uma ordem de serviço sob diagnóstico só pode ser alterada para esperando aprovação ou cancelada.")
        };
    }
}
