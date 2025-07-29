using DCu.Domain.Interfaces.Security;
using System.Text.RegularExpressions;

namespace DCu.Domain.ValueObjects;

public sealed class Password
{
    private readonly string _hashedValue;

    public string HashedValue => _hashedValue;

    private Password(string hashedValue)
    {
        _hashedValue = hashedValue;
    }

    // Para reconstruir desde base de datos
    public static Password FromHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Hash cannot be null or empty.");

        return new Password(hash);
    }

    // Para crear desde una password en texto plano
    public static Password Create(string plainTextPassword, IPasswordHasher hasher)
    {
        ValidateStrength(plainTextPassword);
        var hashed = hasher.Hash(plainTextPassword);
        return new Password(hashed);
    }

    // Validaciones de seguridad
    private static void ValidateStrength(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.");

        if (password.Length < 12)
            throw new ArgumentException("Password must be at least 12 characters long.");

        if (!Regex.IsMatch(password, @"[a-z]"))
            throw new ArgumentException("Password must contain at least one lowercase letter.");

        if (!Regex.IsMatch(password, @"[A-Z]"))
            throw new ArgumentException("Password must contain at least one uppercase letter.");

        if (!Regex.IsMatch(password, @"[0-9]"))
            throw new ArgumentException("Password must contain at least one digit.");

        if (!Regex.IsMatch(password, @"[\W_]"))
            throw new ArgumentException("Password must contain at least one special character.");
    }

    // Para verificar durante login
    public bool Verify(string plainTextPassword, IPasswordHasher hasher)
    {
        return hasher.Verify(plainTextPassword, _hashedValue);
    }

    public override string ToString() => _hashedValue;

    public override bool Equals(object? obj) =>
        obj is Password other && _hashedValue == other._hashedValue;

    public override int GetHashCode() => _hashedValue.GetHashCode();
}

