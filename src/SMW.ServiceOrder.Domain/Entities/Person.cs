using SMW.ServiceOrder.Domain.ValueObjects;
using System.Reflection.Metadata;

namespace SMW.ServiceOrder.Domain.Entities;

public class Person : Entity
{
    public string Fullname { get; private set; } = string.Empty;
    public PersonType PersonType { get; private set; }
    public EmployeeRole? EmployeeRole { get; private set; }
    public Guid AddressId { get; private set; }
    public ICollection<Vehicle> Vehicles { get; private set; } = [];
}
