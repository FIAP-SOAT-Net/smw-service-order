namespace SMW.ServiceOrder.Domain.ValueObjects;

public enum ServiceOrderStatus
{
    Received, UnderDiagnosis, WaitingApproval, InProgress, Completed, Delivered, Cancelled, Rejected
}
