namespace DCu.Domain.Entities;

public class Role
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public List<User> Users { get; private set; } = new();

    private Role() { }

    public static Role Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del rol es obligatorio", nameof(name));

        return new Role
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}


