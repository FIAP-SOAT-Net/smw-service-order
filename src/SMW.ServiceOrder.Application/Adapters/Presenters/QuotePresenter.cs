using SMW.ServiceOrder.Domain.DTOs;
using SMW.ServiceOrder.Domain.Entities;

namespace SMW.ServiceOrder.Application.Adapters.Presenters;

public static class QuotePresenter
{
    public static QuoteDto ToDto(Quote entity) => new QuoteDto(entity.Id, entity.Total, entity.Status, entity.ServiceOrderId);
}
