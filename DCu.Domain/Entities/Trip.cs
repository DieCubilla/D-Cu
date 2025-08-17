using DCu.Domain.Entities;
using DCu.Domain.Enums;
using System.Net.NetworkInformation;

namespace DCu.Domain.Entities;

public class Trip
{
    private readonly List<Destination> _destinations = new();
    private readonly List<Payment> _payments = new();

    public Guid Id { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid UserId { get; private set; } // Quién cargó el viaje
    public Guid VehicleId { get; private set; }
    public DateTime DepartureTime { get; private set; }
    public TripStatus Status { get; private set; }
    public decimal Price { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = default!;


    public IReadOnlyCollection<Destination> Destinations => _destinations.AsReadOnly();
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    public PaymentStatus PaymentStatus => CalculatePaymentStatus();


    private Trip() { } // EF

    private Trip(Guid clientId, Guid userId, Guid vehicleId, DateTime departureTime, decimal price, Guid companyId)
    {
        Id = Guid.NewGuid();
        ClientId = clientId;
        UserId = userId;
        VehicleId = vehicleId;
        DepartureTime = departureTime;
        Price = price;
        Status = TripStatus.Pending;
        CompanyId = companyId;
    }

    public static Trip Create(Guid clientId, Guid userId, Guid vehicleId, DateTime departureTime, decimal price, Guid companyId)
    {
        if (clientId == Guid.Empty) throw new ArgumentException("Cliente requerido.", nameof(clientId));
        if (userId == Guid.Empty) throw new ArgumentException("UserId is required.", nameof(userId));
        if (vehicleId == Guid.Empty) throw new ArgumentException("El vehículo es obligatorio.", nameof(vehicleId));
        if (departureTime == default) throw new ArgumentException("La fecha de salida es obligatoria.", nameof(departureTime));
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price), "El precio no puede ser menor a 0.");
        if (companyId == Guid.Empty) throw new ArgumentException("La empresa es obligatoria.", nameof(companyId));

        return new Trip(clientId, userId, vehicleId, departureTime, price, companyId);
    }

    public void AddDestination(Destination destination)
    {
        _destinations.Add(destination);
    }

    public void AddPayment(Payment pay)
    {
        _payments.Add(pay);
    }

    public void ChangeStatus(TripStatus newStatus)
    {
        Status = newStatus;
    }

    private PaymentStatus CalculatePaymentStatus()
    {
        var totalPaid = _payments.Sum(p => p.Amount);
        if (totalPaid == 0) return PaymentStatus.Unpaid;
        if (totalPaid < Price) return PaymentStatus.PartiallyPaid;
        return PaymentStatus.Paid;
    }
}
