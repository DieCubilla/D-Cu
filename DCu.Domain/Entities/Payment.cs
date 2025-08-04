namespace DCu.Domain.Entities;

using DCu.Domain.Enums;

public class Payment
{
    public Guid Id { get; private set; }

    public Guid TripId { get; private set; }
    public Trip Trip { get; private set; } = default!;

    public Guid CollectedByUserId { get; private set; }
    public User CollectedBy { get; private set; } = default!;

    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }

    public PaymentMethod Method { get; private set; }

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
            throw new ArgumentException("TripId is required.", nameof(tripId));
        if (collectedByUserId == Guid.Empty)
            throw new ArgumentException("CollectedByUserId is required.", nameof(collectedByUserId));
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than 0.");
        if (date == default)
            throw new ArgumentException("Date is required.", nameof(date));

        return new Payment(tripId, collectedByUserId, amount, date, method);
    }

    public override string ToString()
        => $"{Amount:C} - {Method} on {Date:yyyy-MM-dd}";
}
