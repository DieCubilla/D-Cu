using DCu.Domain.Interfaces.Security;
using DCu.Domain.ValueObjects;

namespace DCu.Domain.Entities;

public class User
{
    // Propiedades
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public PasswordHash PasswordHash { get; private set; } = default!; // Renombrada
    public bool IsActive { get; private set; }

    // Relaciones con otras entidades
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = default!;
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;

    // Constructor privado para EF Core
    private User() { }

    // Método de fábrica para crear una NUEVA instancia de usuario
    public static User Create(string name, string email, string plainPassword, Guid roleId, Guid companyId, IPasswordHasher hasher)
    {
        // Validaciones en el punto de entrada
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("El nombre es obligatorio.", nameof(name));
        if (string.IsNullOrWhiteSpace(plainPassword)) throw new ArgumentException("La contraseña es obligatoria.", nameof(plainPassword));
        if (roleId == Guid.Empty) throw new ArgumentException("El identificador de rol es obligatorio.", nameof(roleId));
        if (companyId == Guid.Empty) throw new ArgumentException("El usuario no pertenece a ninguna empresa.", nameof(companyId));
        if (hasher == null) throw new ArgumentNullException(nameof(hasher));

        // Creación de Value Objects: la validación se realiza en sus constructores
        var emailVo = Email.Create(email);
        var passwordVo = new Password(plainPassword);

        return new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = emailVo,
            PasswordHash = hasher.Hash(passwordVo),
            RoleId = roleId,
            CompanyId = companyId,
            IsActive = true
        };
    }

    // Método de fábrica para rehidratar una instancia EXISTENTE desde la DB
    public static User Rehydrate(Guid id, string name, string email, string hashedPassword, Guid roleId, Guid companyId, bool isActive)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id no válido.", nameof(id));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("El nombre es obligatorio.", nameof(name));
        if (string.IsNullOrWhiteSpace(hashedPassword)) throw new ArgumentException("La contraseña hasheada es obligatoria.", nameof(hashedPassword));
        if (roleId == Guid.Empty) throw new ArgumentException("El identificador de rol es obligatorio.", nameof(roleId));
        if (companyId == Guid.Empty) throw new ArgumentException("El usuario no pertenece a ninguna empresa.", nameof(companyId));

        return new User
        {
            Id = id,
            Name = name,
            Email = Email.Create(email),
            PasswordHash = PasswordHash.FromHash(hashedPassword),
            RoleId = roleId,
            CompanyId = companyId,
            IsActive = isActive
        };
    }

    // Comportamiento
    public void ChangePassword(string newPlainPassword, IPasswordHasher hasher)
    {
        if (string.IsNullOrWhiteSpace(newPlainPassword)) throw new ArgumentException("La contraseña es obligatoria.", nameof(newPlainPassword));
        if (hasher == null) throw new ArgumentNullException(nameof(hasher));

        var newPasswordVo = new Password(newPlainPassword);
        PasswordHash = hasher.Hash(newPasswordVo);
    }

    public bool VerifyPassword(string plainPassword, IPasswordHasher hasher)
    {
        if (hasher == null) throw new ArgumentNullException(nameof(hasher));

        var passwordVo = new Password(plainPassword);
        return hasher.Verify(passwordVo, PasswordHash);
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;

    public override string ToString() => $"{Name} ({Email})";
}