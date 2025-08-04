namespace DCu.Domain.Entities;

public class Department
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    private readonly List<City> _cities = new();
    public IReadOnlyCollection<City> Cities => _cities.AsReadOnly();

    // Constructor para EF
    private Department() { }

    private Department(string name)
    {
        Id = Guid.NewGuid(); 
        Name = name;
    }

    public static Department Create(string name) 
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre es obligatorio.", nameof(name));

        return new Department(name); 
    }

    public void AddCity(City city)
    {
        if (city == null) throw new ArgumentNullException(nameof(city));
        _cities.Add(city);
    }

    public override string ToString() => Name;
}
