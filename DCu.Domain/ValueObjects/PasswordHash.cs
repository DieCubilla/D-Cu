using System.Text.RegularExpressions;
using DCu.Domain.Interfaces.Security;

namespace DCu.Domain.ValueObjects;

public sealed class PasswordHash
{
    public string Value { get; }

    private PasswordHash(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    // Fábrica segura con validación de fuerza y hash
    public static PasswordHash Create(string plainPassword, IPasswordHasher hasher)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
            throw new ArgumentException("Password cannot be empty.", nameof(plainPassword));

        if (!IsStrong(plainPassword))
            throw new ArgumentException("Password is not strong enough.", nameof(plainPassword));

        var hash = hasher.Hash(plainPassword);
        return new PasswordHash(hash);
    }

    // Para cargar desde base de datos (hash ya generado)
    public static PasswordHash FromHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(hash));

        return new PasswordHash(hash);
    }

    public bool Verify(string plainPassword, IPasswordHasher hasher)
    {
        return hasher.Verify(plainPassword, Value);
    }

    private static bool IsStrong(string password)
    {
        if (password.Length < 12) return false;

        var hasLower = Regex.IsMatch(password, "[a-z]");
        var hasUpper = Regex.IsMatch(password, "[A-Z]");
        var hasDigit = Regex.IsMatch(password, "[0-9]");
        var hasSpecial = Regex.IsMatch(password, "[^a-zA-Z0-9]");

        return hasLower && hasUpper && hasDigit && hasSpecial;
    }

    public override string ToString() => "[PROTECTED]";
}

