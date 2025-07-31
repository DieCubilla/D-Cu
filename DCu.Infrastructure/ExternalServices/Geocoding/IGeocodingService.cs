public interface IGeocodingService
{
    Task<Coordinates?> GetCoordinatesAsync(Address address, CancellationToken cancellationToken = default);
}