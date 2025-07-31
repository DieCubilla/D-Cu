namespace DCu.Domain.Entities;

public class Destination
{
    public Guid Id { get; private set; }

    public Guid TripId { get; private set; }
    public Trip Trip { get; private set; } = default!;

    public Guid AddressId { get; private set; }
    public Address Address { get; private set; } = default!;

    public int Order { get; private set; }

    public int? Passengers { get; private set; }

    public DateTime? ArrivalAt { get; private set; }
    public DateTime? DepartureAt { get; private set; }

    private Destination() { }

    public static Destination Create(Guid tripId, Guid addressId, int order, int? passengers = null, DateTime? arrivalAt = null, DateTime? departureAt = null)
    {
        return new Destination
        {
            Id = Guid.NewGuid(),
            TripId = tripId,
            AddressId = addressId,
            Order = order,
            Passengers = passengers,
            ArrivalAt = arrivalAt,
            DepartureAt = departureAt
        };
    }

    public void UpdatePassengerInfo(int? passengers, DateTime? arrivalAt, DateTime? departureAt)
    {
        Passengers = passengers;
        ArrivalAt = arrivalAt;
        DepartureAt = departureAt;
    }

    public void MarkBoarded(DateTime arrivalAt) => ArrivalAt = arrivalAt;
    public void MarkAlighted(DateTime departureAt) => DepartureAt = departureAt;
}
