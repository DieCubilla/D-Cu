using DCu.Domain.ValueObjects;

namespace DCu.Domain.Interfaces.Security;

public interface IPasswordHasher
{
    PasswordHash Hash(Password plainPassword);
    bool Verify(Password password, PasswordHash passwordHash);
}
