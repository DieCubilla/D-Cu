using System.Text.RegularExpressions;

namespace DCu.Domain.ValueObjects;

public sealed class Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("La contraseña no puede estar vacía.", nameof(value));
        }

        if (!IsStrong(value))
        {
            throw new ArgumentException("La contraseña no cumple con los requisitos de seguridad.", nameof(value));
        }

        Value = value;
    }

    private static bool IsStrong(string password)
    {
        if (password.Length < 12) return false;
        if (!Regex.IsMatch(password, "[a-z]")) return false;
        if (!Regex.IsMatch(password, "[A-Z]")) return false;
        if (!Regex.IsMatch(password, "[0-9]")) return false;
        if (!Regex.IsMatch(password, @"[\W_]")) return false; // Carácter especial (non-alphanumeric)

        return true;
    }
}