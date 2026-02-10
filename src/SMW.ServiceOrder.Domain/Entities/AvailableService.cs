namespace SMW.ServiceOrder.Domain.Entities;

public class AvailableService : Entity
{
    private AvailableService() { }

    public AvailableService(string name, decimal price)
        : this()
    {
        Name = name;
        Price = price;
    }

    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public ICollection<ServiceOrder> ServiceOrders { get; private set; } = [];
    public ICollection<AvailableServiceSupply> AvailableServiceSupplies { get; private set; } = [];
}
