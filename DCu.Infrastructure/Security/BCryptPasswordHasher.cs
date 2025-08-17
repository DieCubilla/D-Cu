using BCrypt.Net;
using DCu.Domain.Interfaces.Security;
using DCu.Domain.ValueObjects;

namespace DCu.Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 12;

    // El método ahora recibe un Value Object Password
    public PasswordHash Hash(Password password)
    {
        // Se accede al valor de la contraseña a través de la propiedad Value del VO
        var hash = BCrypt.Net.BCrypt.HashPassword(password.Value.Trim(), workFactor: WorkFactor);

        // Se devuelve un nuevo PasswordHash, usando el método de fábrica
        return PasswordHash.FromHash(hash);
    }

    // El método ahora recibe los Value Objects Password y PasswordHash
    public bool Verify(Password password, PasswordHash passwordHash)
    {
        // Se accede a los valores a través de las propiedades Value de los VOs
        return BCrypt.Net.BCrypt.Verify(password.Value, passwordHash.Value);
    }
}