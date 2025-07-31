namespace DCu.Domain.Entities;

/// <summary>
/// Representa una dirección física asociada a una ciudad.
/// </summary>
public class Address
{
    public string Reference { get; private set; }
    public string Street { get; private set; }
    public string? StreetNumber { get; private set; }
    public Guid CityId { get; private set; }
    public City City { get; private set; }

    // Constructor para EF
    private Address() { }

    private Address(string reference, string street, string? streetNumber, Guid cityId)
    {
        if (string.IsNullOrWhiteSpace(reference))
            throw new ArgumentException("La referencia es obligatoria.", nameof(reference));
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("La calle es obligatoria.", nameof(street));
        if (cityId == Guid.Empty)
            throw new ArgumentException("El identificador de ciudad es obligatorio.", nameof(cityId));

        Reference = reference;
        Street = street;
        StreetNumber = streetNumber;
        CityId = cityId;
    }

    /// <summary>
    /// Crea una nueva dirección validando sus invariantes.
    /// </summary>
    public static Address Create(string reference, string street, string? streetNumber, Guid cityId)
        => new Address(reference, street, streetNumber, cityId);
}
