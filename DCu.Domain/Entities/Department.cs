namespace DCu.Domain.Entities;

public class Department
{
    public string Name { get; private set; }

    private readonly List<City> _cities = new();
    public IReadOnlyCollection<City> Cities => _cities.AsReadOnly();

    private Department() { }

    public Department(string name)
    {
        Name = name;
    }

    public void AddCity(City city)
    {
        _cities.Add(city);
    }
}
