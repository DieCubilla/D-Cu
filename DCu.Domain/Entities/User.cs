using DCu.Domain.Interfaces.Security;
using DCu.Domain.ValueObjects;

namespace DCu.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public Email? Email { get; private set; }
    public string Name { get; private set; }
    public Role Role { get; private set; }
    public Password Password { get; private set; }
    public bool IsActive { get; private set; }

    // EF constructor
    private User() { }

    /// <summary>
    /// Crea un nuevo usuario con contraseña en texto plano.
    /// </summary>
    public static User Create(string name, string email, string plainPassword, Role role, IPasswordHasher hasher)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("El nombre es obligatorio.", nameof(name));
        if (string.IsNullOrWhiteSpace(plainPassword)) throw new ArgumentException("La contraseña es obligatoria.", nameof(plainPassword));
        if (role == null) throw new ArgumentNullException(nameof(role));
        if (hasher == null) throw new ArgumentNullException(nameof(hasher));

        return new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = CreateEmailOrNull(email),
            Password = Password.Create(plainPassword, hasher),
            Role = role,
            IsActive = true
        };
    }

    /// <summary>
    /// Reconstruye un usuario desde persistencia.
    /// </summary>
    public static User Rehydrate(Guid id, string name, string email, string hashedPassword, Role role, bool isActive)
    {
        if (id == Guid.Empty) throw new ArgumentException("Error.", nameof(id));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("El nombre es obligatorio.", nameof(name));
        if (string.IsNullOrWhiteSpace(hashedPassword)) throw new ArgumentException("La contraseña es obligatoria.", nameof(hashedPassword));
        if (role == null) throw new ArgumentNullException(nameof(role));

        return new User
        {
            Id = id,
            Name = name,
            Email = CreateEmailOrNull(email),
            Password = Password.FromHash(hashedPassword),
            Role = role,
            IsActive = isActive
        };
    }

    /// <summary>
    /// Cambia la password desde un texto plano.
    /// </summary>
    public void ChangePassword(string newPlainPassword, IPasswordHasher hasher)
    {
        if (string.IsNullOrWhiteSpace(newPlainPassword)) throw new ArgumentException("La contraseña es obligatoria.", nameof(newPlainPassword));
        if (hasher == null) throw new ArgumentNullException(nameof(hasher));
        Password = Password.Create(newPlainPassword, hasher);
    }

    /// <summary>
    /// Verifica si la contraseña en texto plano coincide con la almacenada.
    /// </summary>
    public bool VerifyPassword(string plainPassword, IPasswordHasher hasher)
    {
        if (hasher == null) throw new ArgumentNullException(nameof(hasher));
        return Password.Verify(plainPassword, hasher);
    }

    /// <summary>
    /// Elimina el usuario.
    /// </summary>
    public void Deactivate() => IsActive = false;
    /// <summary>
    /// Activa el usuario.
    /// </summary>
    public void Activate() => IsActive = true;

    private static Email? CreateEmailOrNull(string email) =>
        string.IsNullOrWhiteSpace(email) ? null : Email.Create(email);
}

