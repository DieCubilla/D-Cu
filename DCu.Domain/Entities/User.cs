using DCu.Domain.Interfaces.Security;
using DCu.Domain.ValueObjects;

namespace DCu.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public Email? Email { get; private set; }
    public string Name { get; private set; } = default!;
    public Role Role { get; private set; } = default!;
    public Password Password { get; private set; } = default!;
    public bool IsActive { get; private set; }

    // EF constructor
    private User() { }

    // Crear nuevo usuario (con password plano → hasheado)
    public static User Create(string name, string email, string plainPassword, Role role, IPasswordHasher hasher)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = string.IsNullOrWhiteSpace(email) ? null : Email.Create(email),
            Password = Password.Create(plainPassword, hasher),
            Role = role,
            IsActive = true
        };
    }

    // Reconstrucción desde persistencia (hasheado ya disponible)
    public static User Rehydrate(Guid id, string name, string email, string hashedPassword, Role role, bool isActive)
    {
        return new User
        {
            Id = id,
            Name = name,
            Email = string.IsNullOrWhiteSpace(email) ? null : Email.Create(email),
            Password = Password.FromHash(hashedPassword),
            Role = role,
            IsActive = isActive
        };
    }

    public void ChangePassword(string newPlainPassword, IPasswordHasher hasher)
    {
        Password = Password.Create(newPlainPassword, hasher);
    }

    public bool VerifyPassword(string plainPassword, IPasswordHasher hasher)
    {
        return Password.Verify(plainPassword, hasher);
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}

