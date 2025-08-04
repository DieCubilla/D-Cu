namespace DCu.Domain.Entities;

public class City
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Department Department { get; private set; }

    // Constructor para EF
    private City() { }

    private City(string name, Guid departmentId)
    {
        Id = Guid.NewGuid();
        Name = name;
        DepartmentId = departmentId;
    }

    public static City Create(string name, Guid departmentId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre es obligatorio.", nameof(name));
        if (departmentId == Guid.Empty)
            throw new ArgumentException("El identificador de departamento es obligatorio.", nameof(departmentId));

        return new City(name, departmentId);
    }


    public override string ToString() => Name;
}
