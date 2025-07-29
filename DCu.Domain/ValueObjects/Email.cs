using System.Text.RegularExpressions;

namespace DCu.Domain.ValueObjects;

public sealed class Email : IEquatable<Email>
{
    private static readonly Regex _emailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public string Address { get; }

    private Email(string address)
    {
        Address = address;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email no puede ser vacío.");

        if (!_emailRegex.IsMatch(email))
            throw new ArgumentException("Formato de mail inválido.");

        return new Email(email.Trim().ToLowerInvariant());
    }

    public override string ToString() => Address;

    public override bool Equals(object? obj) => Equals(obj as Email);

    public bool Equals(Email? other) => other is not null && Address == other.Address;

    public override int GetHashCode() => Address.GetHashCode();

    public static implicit operator string(Email email) => email.Address;
}
