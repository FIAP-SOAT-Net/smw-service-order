using MediatR;
using SMW.ServiceOrder.Domain.DTOs;
using SMW.ServiceOrder.Domain.Shared;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Get;

public record GetAverageExecutionTimeCommand(DateOnly StartDate, DateOnly EndDate) : IRequest<Response<ServiceOrderExecutionTimeReportDto>>;
