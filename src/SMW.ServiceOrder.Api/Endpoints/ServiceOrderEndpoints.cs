using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using SMW.ServiceOrder.Application.Controllers.Interfaces;
using SMW.ServiceOrder.Domain.DTOs;
using SMW.ServiceOrder.Domain.ValueObjects;

namespace SMW.ServiceOrder.Api.Endpoints;

public static class ServiceOrderEndpoints
{
    public static IEndpointRouteBuilder MapServiceOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/serviceOrders")
            .WithTags("ServiceOrders")
            .RequireAuthorization();

        group.MapGet("/{id:guid}", (
                [FromRoute] Guid id,
                [FromServices] IServiceOrdersController controller,
                CancellationToken cancellationToken) => controller.GetOneAsync(id, cancellationToken))
            .WithName("GetServiceOrder")
            .WithMetadata(new OpenApiOperation { Summary = "Get a service order by id", Description = "Returns a single service order by its unique identifier." })
            .Produces<ServiceOrderDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet("/", (
                [AsParameters] PaginatedRequest paginatedRequest,
                [FromQuery] Guid? personId,
                [FromServices] IServiceOrdersController controller,
                CancellationToken cancellationToken) => controller.GetAllAsync(paginatedRequest, personId, cancellationToken))
            .WithName("GetAllServiceOrders")
            .WithMetadata(new OpenApiOperation { Summary = "Get all service orders (paginated)", Description = "Returns a paginated list of service orders." })
            .Produces<Paginate<ServiceOrderDto>>(StatusCodes.Status200OK);

        group.MapGet("/average-execution-time", (
                [FromQuery] DateOnly startDate,
                [FromQuery] DateOnly endDate,
                [FromServices] IServiceOrdersController controller,
                CancellationToken cancellationToken) => controller.GetAverageExecutionTime(startDate, endDate, cancellationToken))
            .WithName("GetAverageExecutionTime")
            .WithMetadata(new OpenApiOperation { Summary = "Get average execution time of service orders", Description = "Returns the average execution time for all service orders." })
            .Produces<Response<ServiceOrderExecutionTimeReportDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPost("/", (
                [FromBody] CreateServiceOrderRequest request,
                [FromServices] IServiceOrdersController controller,
                CancellationToken cancellationToken) => controller.CreateAsync(request, cancellationToken))
            .WithName("CreateServiceOrder")
            .WithMetadata(new OpenApiOperation { Summary = "Create a new service order", Description = "Creates a new service order and returns its data." })
            .Produces<ServiceOrderDto>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapDelete("/{id:guid}", (
                [FromRoute] Guid id,
                [FromServices] IServiceOrdersController controller,
                CancellationToken cancellationToken) => controller.DeleteAsync(id, cancellationToken))
            .WithName("DeleteServiceOrder")
            .WithMetadata(new OpenApiOperation { Summary = "Delete a service order", Description = "Deletes a service order by its unique identifier." })
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPut("/{id:guid}", (
                [FromRoute] Guid id,
                [FromBody] UpdateOneServiceOrderRequest request,
                [FromServices] IServiceOrdersController controller,
                CancellationToken cancellationToken) => controller.UpdateAsync(id, request, cancellationToken))
            .WithName("UpdateServiceOrder")
            .WithMetadata(new OpenApiOperation { Summary = "Update a service order", Description = "Updates an existing service order by its unique identifier." })
            .Produces<ServiceOrderDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPatch("/{id:guid}", (
                [FromRoute] Guid id,
                [FromBody] PatchServiceOrderRequest request,
                [FromServices] IServiceOrdersController controller,
                CancellationToken cancellationToken) => controller.PatchAsync(id, request, cancellationToken))
            .WithName("PatchServiceOrder")
            .WithMetadata(new OpenApiOperation { Summary = "Patch a service order", Description = "Updates an existing service order by its unique identifier with partial data." })
            .Produces<ServiceOrderDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPatch("/{id:guid}/quote/{quoteId:guid}/{status}", (
                [FromRoute] Guid id,
                [FromRoute] Guid quoteId,
                [FromRoute] QuoteStatus status,
                [FromServices] IQuoteController quoteController,
                CancellationToken cancellationToken) => quoteController.PatchQuoteAsync(id, quoteId, status, cancellationToken))
            .WithName("PatchServiceOrderQuote")
            .WithMetadata(new OpenApiOperation { Summary = "Approve a quote for a service order", Description = "Approves a quote for a service order." })
            .Produces<ServiceOrderDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        return app;
    }
}
