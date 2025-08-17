namespace DCu.Domain.Entities;

public class Destination
{
    public Guid Id { get; private set; }
    public Guid TripId { get; private set; }
    public Guid AddressId { get; private set; }
    public int Order { get; private set; }
    public int Passengers { get; private set; }
    public DateTime? ArrivalAt { get; private set; }
    public DateTime? DepartureAt { get; private set; }

    // Constructor para EF
    private Destination() { }

    private Destination(Guid id, Guid tripId, Guid addressId, int order, int passengers, DateTime? arrivalAt, DateTime? departureAt)
    {
        Id = id;
        TripId = tripId;
        AddressId = addressId;
        Order = order;
        Passengers = passengers;
        ArrivalAt = arrivalAt;
        DepartureAt = departureAt;
    }

    public static Destination Create(Guid tripId, Guid addressId, int order, int passengers)
    {
        if (tripId == Guid.Empty) throw new ArgumentException("El viaje es obligatorio.", nameof(tripId));
        if (addressId == Guid.Empty) throw new ArgumentException("La dirección es obligatoria.", nameof(addressId));
        if (order < 1) throw new ArgumentOutOfRangeException(nameof(order), "El orden debe ser mayor o igual a 1.");
        if (passengers < 1) throw new ArgumentOutOfRangeException(nameof(passengers), "El número de pasajeros es requerido.");

        return new Destination(Guid.NewGuid(), tripId, addressId, order, passengers, null, null);
    }

    public void UpdatePassengerInfo(int passengers, DateTime? arrivalAt, DateTime? departureAt)
    {
        if (passengers < 0) throw new ArgumentOutOfRangeException(nameof(passengers), "El número de pasajeros no puede ser negativo.");
        Passengers = passengers;
        ArrivalAt = arrivalAt;
        DepartureAt = departureAt;
    }

    public void MarkArrival(DateTime arrivalAt)
    {
        if (arrivalAt == default) throw new ArgumentException("La fecha de llegada es obligatoria.", nameof(arrivalAt));
        ArrivalAt = arrivalAt;
    }

    public void MarkADeparture(DateTime departureAt)
    {
        if (departureAt == default) throw new ArgumentException("La fecha de salida es obligatoria.", nameof(departureAt));
        DepartureAt = departureAt;
    }
}