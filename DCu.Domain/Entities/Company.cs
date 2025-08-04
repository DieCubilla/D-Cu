namespace DCu.Domain.Entities;

public class Company
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; }

    // Navegación (opcional)
    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    private Company() { }

    private Company(string name)
    {

        Id = Guid.NewGuid();
        Name = name;
        IsActive = true;
    }

    public static Company Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Company name is required.", nameof(name));

        return new Company(name);
    }

    public void AddUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (user.CompanyId != Id)
            throw new InvalidOperationException("No se pudo unir el usuario a la empresa.");
        _users.Add(user);
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;

    public override string ToString() => Name;
}
