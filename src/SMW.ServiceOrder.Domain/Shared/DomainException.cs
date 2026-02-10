using System.Diagnostics.CodeAnalysis;

namespace SMW.ServiceOrder.Domain.Shared;

[ExcludeFromCodeCoverage]
public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
