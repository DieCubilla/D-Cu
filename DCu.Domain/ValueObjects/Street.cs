namespace DCu.Domain.ValueObjects;


public sealed class Street
{
    public string Name { get; }

    public Street(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("El nombre de la calle no puede ser vacío.", nameof(name));
        }

        Name = name.Trim();
    }
}