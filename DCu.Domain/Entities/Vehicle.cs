namespace DCu.Domain.Entities;

/// <summary>
/// Representa un vehículo en la flota de la compañía.
/// </summary>
public class Vehicle
{
    public Guid Id { get; private set; }
    public string LicensePlate { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; } 
    public int Year { get; private set; }
    public bool IsActive { get; private set; }

    // Relación con la compañía (clave foránea)
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;

    // Constructor privado para uso de Entity Framework Core
    private Vehicle() { 
        LicensePlate = default!;
        Brand = default!;
        Model = default!;
    }

    /// <summary>
    /// Método de fábrica para crear un nuevo vehículo.
    /// </summary>
    public static Vehicle Create(
        string licensePlate,
        string brand,
        string model,
        int year,
        Guid companyId)
    {
        // Validaciones de creación
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            throw new ArgumentException("La matrícula es obligatoria.", nameof(licensePlate));
        }
        if (string.IsNullOrWhiteSpace(brand))
        {
            throw new ArgumentException("La marca es obligatoria.", nameof(brand));
        }
        if (string.IsNullOrWhiteSpace(model))
        {
            throw new ArgumentException("El modelo es obligatorio.", nameof(model));
        }
        if (year < 1900 || year > DateTime.Now.Year + 1)
        {
            throw new ArgumentOutOfRangeException(nameof(year), "El año del vehículo no es válido.");
        }
        if (companyId == Guid.Empty)
        {
            throw new ArgumentException("El vehículo debe pertenecer a una compañía.", nameof(companyId));
        }

        return new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = licensePlate.Trim(),
            Brand = brand.Trim(),
            Model = model.Trim(),
            Year = year,
            IsActive = true,
            CompanyId = companyId
        };
    }

    /// <summary>
    /// Desactiva el vehículo.
    /// </summary>
    public void Deactivate() => IsActive = false;

    /// <summary>
    /// Activa el vehículo.
    /// </summary>
    public void Activate() => IsActive = true;
}