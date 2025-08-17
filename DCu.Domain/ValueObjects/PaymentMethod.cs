using DCu.Domain.Common;

namespace DCu.Domain.ValueObjects;

public sealed class PaymentMethod : ValueObject
{
    public static readonly PaymentMethod Cash = new("Efectivo");
    public static readonly PaymentMethod Card = new("Tarjeta");
    public static readonly PaymentMethod Transfer = new("Transferencia");
    // Agrega más métodos de pago si es necesario
    // public static readonly PaymentMethod ...

    public string Value { get; }

    // El constructor es privado para que solo se pueda crear a través de los métodos de fábrica
    private PaymentMethod(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El método de pago no puede ser nulo o vacío.", nameof(value));
        Value = value;
    }

    /// <summary>
    /// Crea un PaymentMethod desde una cadena de texto, validando que el valor sea conocido.
    /// </summary>
    public static PaymentMethod From(string value)
    {
        if (value.Equals(Cash.Value, StringComparison.OrdinalIgnoreCase))
            return Cash;
        if (value.Equals(Card.Value, StringComparison.OrdinalIgnoreCase))
            return Card;
        if (value.Equals(Transfer.Value, StringComparison.OrdinalIgnoreCase))
            return Transfer;

        throw new ArgumentException($"El valor '{value}' no es un método de pago válido.", nameof(value));
    }

    /// <summary>
    /// Método requerido para la igualdad de Value Objects.
    /// </summary>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}