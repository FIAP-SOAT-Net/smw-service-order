using MediatR;
using Microsoft.AspNetCore.Mvc;
using SMW.ServiceOrder.Application.Adapters.Controllers.Interfaces;
using SMW.ServiceOrder.Application.Adapters.Presenters;
using SMW.ServiceOrder.Application.Mappers;
using SMW.ServiceOrder.Application.Models;
using SMW.ServiceOrder.Application.UseCases.ServiceOrders.Create;
using SMW.ServiceOrder.Application.UseCases.ServiceOrders.Delete;
using SMW.ServiceOrder.Application.UseCases.ServiceOrders.Get;
using SMW.ServiceOrder.Application.UseCases.ServiceOrders.List;
using SMW.ServiceOrder.Application.UseCases.ServiceOrders.Update;
using SMW.ServiceOrder.Domain.Shared;

namespace SMW.ServiceOrder.Application.Adapters.Controllers;

public sealed class ServiceOrdersController(IMediator mediator) : IServiceOrdersController
{
    public async Task<IActionResult> GetOneAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetServiceOrderByIdQuery(id), cancellationToken);
        var result = ResponseMapper.Map(response, ServiceOrderPresenter.ToDto);
        return ActionResultPresenter.ToActionResult(result);
    }

    public async Task<IActionResult> GetAllAsync(PaginatedRequest paginatedRequest, Guid? personId, CancellationToken cancellationToken)
    {
        var query = new ListServiceOrdersQuery(paginatedRequest.PageNumber, paginatedRequest.PageSize, personId);
        var response = await mediator.Send(query, cancellationToken);
        var result = ResponseMapper.Map(response, ServiceOrderPresenter.ToDto);
        return ActionResultPresenter.ToActionResult(result);
    }

    public async Task<IActionResult> GetAverageExecutionTime(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAverageExecutionTimeCommand(startDate, endDate), cancellationToken);
        return ActionResultPresenter.ToActionResult(response);
    }

    public async Task<IActionResult> CreateAsync(CreateServiceOrderRequest request, CancellationToken cancellationToken)
    {
        CreateServiceOrderCommand command = new(request.ClientId, request.VehicleId, request.ServiceIds, request.Title, request.Description);
        var response = await mediator.Send(command, cancellationToken);
        var result = ResponseMapper.Map(response, ServiceOrderPresenter.ToDto);
        return ActionResultPresenter.ToActionResult(result);
    }

    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new DeleteServiceOrderCommand(id), cancellationToken);
        return ActionResultPresenter.ToActionResult(response);
    }

    public async Task<IActionResult> UpdateAsync(Guid id, UpdateOneServiceOrderRequest request, CancellationToken cancellationToken)
    {
        UpdateServiceOrderCommand command = new(id, request.Title, request.Description, request.ServiceIds);
        var response = await mediator.Send(command, cancellationToken);
        var result = ResponseMapper.Map(response, ServiceOrderPresenter.ToDto);
        return ActionResultPresenter.ToActionResult(result);
    }

    public async Task<IActionResult> PatchAsync(Guid id, PatchServiceOrderRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new UpdateServiceOrderStatusCommand(id, request.Status), cancellationToken);
        var result = ResponseMapper.Map(response, ServiceOrderPresenter.ToDto);
        return ActionResultPresenter.ToActionResult(result);
    }
}
