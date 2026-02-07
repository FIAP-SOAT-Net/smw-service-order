namespace SMW.ServiceOrder.Domain.DTOs;

public interface IPaginate<out T>
{
    IEnumerable<T> Items { get; }
    int TotalCount { get; }
    int PageSize { get; }
    int CurrentPage { get; }
    int TotalPages { get; }
}
