using DCu.Domain.Common;

namespace DCu.Domain.ValueObjects;

public sealed class ClientType : ValueObject
{
    public static readonly ClientType Particular = new("Particular");
    public static readonly ClientType Empresa = new("Empresa");

    public string Value { get; }

    private ClientType(string value)
    {
        Value = value;
    }

    public static ClientType From(string value)
    {
        if (string.Equals(value, "Particular", StringComparison.OrdinalIgnoreCase)) return Particular;
        if (string.Equals(value, "Empresa", StringComparison.OrdinalIgnoreCase)) return Empresa;
        throw new ArgumentException("Tipo de cliente no válido.", nameof(value));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
