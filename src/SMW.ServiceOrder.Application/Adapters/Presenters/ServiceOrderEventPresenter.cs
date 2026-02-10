using SMW.ServiceOrder.Domain.DTOs;
using SMW.ServiceOrder.Domain.Entities;

namespace SMW.ServiceOrder.Application.Adapters.Presenters;

public sealed class ServiceOrderEventPresenter
{
    public static ServiceOrderEventDto ToDto(ServiceOrderEvent entity) =>
        new ServiceOrderEventDto(entity.Id, entity.Status, entity.CreatedAt);
}
