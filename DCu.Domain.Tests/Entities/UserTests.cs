using DCu.Domain.Entities;
using DCu.Domain.Interfaces;
using DCu.Domain.Interfaces.Security;
using DCu.Domain.ValueObjects;
using Moq;
using System;
using Xunit;

namespace DCu.Domain.Tests.Entities;

public class UserTests
{
    private readonly Mock<IPasswordHasher> _passwordHasherMock;

    public UserTests()
    {
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _passwordHasherMock
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns<string>(pwd => $"HASHED_{pwd}");

        _passwordHasherMock
            .Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()))
            .Returns<string, string>((plain, hash) => hash == $"HASHED_{plain}");
    }

    [Fact]
    public void Create_ShouldInitializeUserWithHashedPassword()
    {
        var role = Role.Create("Admin");
        var user = User.Create("Juan", "juan@email.com", "MySecure123!", role, _passwordHasherMock.Object);

        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal("Juan", user.Name);
        Assert.Equal("juan@email.com", user.Email.Address);
        Assert.Equal("HASHED_MySecure123!", user.Password.HashedValue);
        Assert.Equal(role, user.Role);
        Assert.True(user.IsActive);
    }

    [Fact]
    public void ChangePassword_ShouldUpdatePasswordHash()
    {
        var role = Role.Create("Admin");
        var user = User.Create("Ana", "ana@email.com", "OldPass1234!", role, _passwordHasherMock.Object);

        user.ChangePassword("NewPass1234!", _passwordHasherMock.Object);

        Assert.Equal("HASHED_NewPass1234!", user.Password.HashedValue);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnTrue_ForCorrectPassword()
    {
        var role = Role.Create("Admin");
        var user = User.Create("Pedro", "pedro@email.com", "12345678Abc!", role, _passwordHasherMock.Object);

        var isValid = user.VerifyPassword("12345678Abc!", _passwordHasherMock.Object);

        Assert.True(isValid);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnFalse_ForIncorrectPassword()
    {
        var role = Role.Create("Admin");
        var user = User.Create("Lucas", "lucas@email.com", "Correct1234!", role, _passwordHasherMock.Object);

        var isValid = user.VerifyPassword("WrongPassword!", _passwordHasherMock.Object);

        Assert.False(isValid);
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveFalse()
    {
        var role = Role.Create("Admin");
        var user = User.Create("Erika", "erika@email.com", "Xyzbjhbjkb321@!", role, _passwordHasherMock.Object);

        user.Deactivate();

        Assert.False(user.IsActive);
    }

    [Fact]
    public void Rehydrate_ShouldRestoreAllPropertiesCorrectly()
    {
        var role = Role.Create("Admin");
        var userId = Guid.NewGuid();
        var user = User.Rehydrate(userId, "Mario", "mario@email.com", "HASHED_Original!", role, true);

        Assert.Equal(userId, user.Id);
        Assert.Equal("Mario", user.Name);
        Assert.Equal("mario@email.com", user.Email.Address);
        Assert.Equal("HASHED_Original!", user.Password.HashedValue);
        Assert.True(user.IsActive);
    }
}
