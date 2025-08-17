namespace DCu.Domain.Entities;

using DCu.Domain.Enums;
using DCu.Domain.ValueObjects;

public class Payment
{
    public Guid Id { get; private set; }

    public Guid TripId { get; private set; }
    public Trip Trip { get; private set; } = default!;

    public Guid CollectedByUserId { get; private set; }
    public User CollectedBy { get; private set; } = default!;

    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }

    public PaymentMethod Method { get; private set; } = default!;

    private Payment() { }

    private Payment(Guid tripId, Guid collectedByUserId, decimal amount, DateTime date, PaymentMethod method)
    {

        Id = Guid.NewGuid();
        TripId = tripId;
        CollectedByUserId = collectedByUserId;
        Amount = amount;
        Date = date;
        Method = method;
    }

    public static Payment Create(Guid tripId, Guid collectedByUserId, decimal amount, DateTime date, PaymentMethod method)
    {
        if (tripId == Guid.Empty)
            throw new ArgumentException("El viaje es requerido.", nameof(tripId));
        if (collectedByUserId == Guid.Empty)
            throw new ArgumentException("El usuario es requerido.", nameof(collectedByUserId));
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "El monto debe ser mayor a 0.");
        if (date == default)
            throw new ArgumentException("La fecha es requerida.", nameof(date));
        if (method == default)
            throw new ArgumentException("El metodo es requerido.", nameof(method));

        return new Payment(tripId, collectedByUserId, amount, date, method);
    }

    public override string ToString()
        => $"{Amount:C} - {Method} on {Date:yyyy-MM-dd}";
}
