using MediatR;
using SMW.ServiceOrder.Domain.Entities;

namespace SMW.ServiceOrder.Application.UseCases.Quotes.Update;

public record UpdateQuoteStatusNotification(Guid Id, Quote Quote) : INotification;
