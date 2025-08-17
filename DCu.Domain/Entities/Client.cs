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
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;


    // Constructor para EF
    private Client() { 
        NameOrCompany = default!;
        Phone = default!;
        Type = default!;
    }

    private Client(Guid id, string nameOrCompany, string? documentNumber, ClientType type, 
        string phone, Email? email, bool isActive, Guid companyId)
    {

        Id = id;
        NameOrCompany = nameOrCompany;
        DocumentNumber = documentNumber;
        Type = type;
        Phone = phone;
        Email = email;
        IsActive = isActive;
        CompanyId = companyId;
    }

    /// <summary>
    /// Crea un nuevo cliente.
    /// </summary>
    public static Client Create(string nameOrCompany, string? documentNumber,
        ClientType type, string phone, string? email, Guid companyId)
    {
        Validate(nameOrCompany, phone, companyId);

        return new(
            Guid.NewGuid(),
            nameOrCompany.Trim(),
            documentNumber,
            type,
            phone.Trim(),
            string.IsNullOrWhiteSpace(email) ? null : Email.Create(email.Trim()),
            true,
            companyId
        );
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;

    public static void Validate(string nameOrCompany, string phone, Guid companyId)
    {
        ValidateNameOrCompany(nameOrCompany);
        ValidatePhone(phone);
        if (companyId == Guid.Empty)
            throw new ArgumentException("El cliente debe pertenecer a una empresa.", nameof(companyId));
    }


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
