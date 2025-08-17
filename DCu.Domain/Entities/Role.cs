namespace DCu.Domain.Entities;

public class Role
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    // Constructor para EF
    private Role() { 
        Name = default!;
    }

    private Role(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public static Role Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del rol es obligatorio.", nameof(name));

        return new Role(name.Trim());
    }

    public override string ToString() => Name;
}