using DCu.Domain.ValueObjects;

namespace DCu.Domain.Entities;

/// <summary>
/// Representa un cliente, que puede ser particular o empresa.
/// </summary>
public class Client
{
    public Guid Id { get; private set; }
    public string NameOrCompany { get; private set; }
    public string? DocumentNumber { get; private set; }
    public ClientType Type { get; private set; }
    public string Phone { get; private set; }
    public Email? Email { get; private set; }
    public bool IsActive { get; private set; }

    // Constructor para EF
    private Client() { }

    private Client(Guid id, string nameOrCompany, string? documentNumber, ClientType type, string phone, Email? email, bool isActive)
    {
        ValidateNameOrCompany(nameOrCompany);
        ValidatePhone(phone);

        Id = id;
        NameOrCompany = nameOrCompany;
        DocumentNumber = documentNumber;
        Type = type;
        Phone = phone;
        Email = email;
        IsActive = isActive;
    }

    /// <summary>
    /// Crea un nuevo cliente.
    /// </summary>
    public static Client Create(string nameOrCompany, string? documentNumber, ClientType type, string phone, string? email)
        => new(
            Guid.NewGuid(),
            nameOrCompany,
            documentNumber,
            type,
            phone,
            string.IsNullOrWhiteSpace(email) ? null : Email.Create(email),
            true
        );

    /// <summary>
    /// Reconstruye un cliente desde persistencia.
    /// </summary>
    public static Client Rehydrate(Guid id, string nameOrCompany, string? documentNumber, ClientType type, string phone, string? email, bool isActive)
        => new(
            id,
            nameOrCompany,
            documentNumber,
            type,
            phone,
            string.IsNullOrWhiteSpace(email) ? null : Email.Create(email),
            isActive
        );

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;

    private static void ValidateNameOrCompany(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre o razón social es obligatorio.", nameof(value));
    }

    private static void ValidatePhone(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El teléfono es obligatorio.", nameof(value));
    }
}
