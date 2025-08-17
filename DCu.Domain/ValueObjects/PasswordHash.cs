namespace DCu.Domain.ValueObjects;

public sealed class PasswordHash
{
    public string Value { get; }

    // Constructor privado para asegurar la creación a través de métodos de fábrica.
    private PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El hash de la contraseña no puede estar vacío.", nameof(value));
        }

        Value = value.Trim();
    }

    // Método de fábrica para crear un PasswordHash desde un hash ya existente (ej. desde la base de datos).
    public static PasswordHash FromHash(string hash)
    {
        return new PasswordHash(hash);
    }
}