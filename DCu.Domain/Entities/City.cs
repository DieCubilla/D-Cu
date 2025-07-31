namespace DCu.Domain.Entities;

public class City
{
    public string Name { get; private set; }

    public Guid DepartmentId { get; private set; }
    public Department Department { get; private set; }

    private City() { }

    public City(string name, Guid departmentId)
    {
        Name = name;
        DepartmentId = departmentId;
    }
}
