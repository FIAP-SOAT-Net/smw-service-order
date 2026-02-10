using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SMW.ServiceOrder.Application.Adapters.Gateways.Repositories;
using SMW.ServiceOrder.Domain.DTOs;
using SMW.ServiceOrder.Domain.Shared;

namespace SMW.ServiceOrder.Application.UseCases.ServiceOrders.Get;

public sealed class GetAverageExecutionTimeHandler(IServiceOrderEventRepository repository, IMemoryCache memoryCache)
    : IRequestHandler<GetAverageExecutionTimeCommand, Response<ServiceOrderExecutionTimeReportDto>>
{
    public async Task<Response<ServiceOrderExecutionTimeReportDto>> Handle(GetAverageExecutionTimeCommand request, CancellationToken cancellationToken)
    {
        var cachedValue = await memoryCache.GetOrCreateAsync(
            request.ToString(),
            async cacheEntry =>
            {
                cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(10);
                return await repository.GetAverageExecutionTimesAsync(request.StartDate.ToDateTime(TimeOnly.MinValue),
                    request.EndDate.ToDateTime(TimeOnly.MaxValue),
                    cancellationToken);
            });

        return ResponseFactory.Ok(cachedValue!);
    }
}
