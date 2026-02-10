using MediatR;
using SMW.ServiceOrder.Domain.Entities;
using SMW.ServiceOrder.Domain.Shared;
using SMW.ServiceOrder.Domain.ValueObjects;

namespace SMW.ServiceOrder.Application.UseCases.Quotes.Update;

public record UpdateQuoteStatusCommand(Guid Id, QuoteStatus Status, Guid ServiceOrderId) : IRequest<Response<Quote>>;
