namespace SMW.ServiceOrder.Domain.Entities;

public class Vehicle : Entity
{
    private Vehicle() { }

    public Vehicle(string model, string brand, int manufactureYear, string licensePlate, Guid personId) : this()
    {
        Model = model;
        Brand = brand;
        ManufactureYear = manufactureYear;
        LicensePlate = licensePlate;
        PersonId = personId;
    }

    public string LicensePlate { get; private set; } = string.Empty;
    public int ManufactureYear { get; private set; }
    public string Brand { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public Guid PersonId { get; private set; }

    public Person Person { get; private set; } = null!;
}
