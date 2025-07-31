using DCu.Domain.Common;

public class Coordinates : ValueObject
{
    public decimal Latitude { get; }
    public decimal Longitude { get; }

    public Coordinates(decimal latitude, decimal longitude)
    {
        if (latitude is < -90 or > 90)
            throw new ArgumentOutOfRangeException(nameof(latitude));
        if (longitude is < -180 or > 180)
            throw new ArgumentOutOfRangeException(nameof(longitude));

        Latitude = latitude;
        Longitude = longitude;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }

    public override string ToString() => $"{Latitude}, {Longitude}";
}
