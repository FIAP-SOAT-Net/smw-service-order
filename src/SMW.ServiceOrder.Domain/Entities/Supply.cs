namespace SMW.ServiceOrder.Domain.Entities;

public class Supply : Entity
{
    private Supply() { }

    public Supply(string name, decimal price, int quantity)
        : this()
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public string Name { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public ICollection<AvailableServiceSupply> AvailableServiceSupplies { get; private set; } = [];
}
