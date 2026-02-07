using System.Diagnostics.CodeAnalysis;

namespace SMW.ServiceOrder.Domain.DTOs;

[ExcludeFromCodeCoverage]
public record VehicleDto(Guid Id, string LicensePlate, int ManufactureYear, string Brand, string Model, Guid PersonId);
