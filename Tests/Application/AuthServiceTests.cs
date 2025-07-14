using Application.Dto.Authentication;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using FluentAssertions;
using Application.Exceptions;
using Application.Services;

namespace Tests.Application;

public class AuthServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private readonly Mock<IConfiguration> _configMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<AuthService>> _loggerMock;

    public AuthServiceTests()
    {
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);

        var contextAccessorMock = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
        var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            _userManagerMock.Object,
            contextAccessorMock.Object,
            claimsFactoryMock.Object,
            null, null, null, null);

        _configMock = new Mock<IConfiguration>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<AuthService>>();
    }

    private AuthService CreateService()
    {
        return new AuthService(
            _userManagerMock.Object,
            _mapperMock.Object,
            _configMock.Object,
            _signInManagerMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task CreateAdminAsync_ShouldThrow_WhenUserAlreadyExists()
    {
        var request = new RegisterAdminRequest { Username = "admin", Password = "123" };
        var existingUser = new ApplicationUser { UserName = request.Username };

        _userManagerMock.Setup(u => u.FindByNameAsync(request.Username))
            .ReturnsAsync(existingUser);

        var service = CreateService();

        await Assert.ThrowsAsync<ValidationException>(() =>
            service.CreateAdminAsync(request));
    }

    [Fact]
    public async Task CreateAdminAsync_ShouldThrow_WhenCreateFails()
    {
        var request = new RegisterAdminRequest { Username = "admin", Password = "123" };

        _userManagerMock.Setup(u => u.FindByNameAsync(request.Username))
            .ReturnsAsync((ApplicationUser?)null);

        var user = new ApplicationUser { UserName = request.Username };
        _mapperMock.Setup(m => m.Map<ApplicationUser>(request)).Returns(user);

        var identityResult = IdentityResult.Failed(new IdentityError { Description = "Failed" });

        _userManagerMock.Setup(u => u.CreateAsync(user, request.Password))
            .ReturnsAsync(identityResult);

        var service = CreateService();

        await Assert.ThrowsAsync<ValidationException>(() =>
            service.CreateAdminAsync(request));
    }

    [Fact]
    public async Task CreateAdminAsync_ShouldSucceed_WhenDataIsValid()
    {
        var request = new RegisterAdminRequest { Username = "admin", Password = "123" };
        var user = new ApplicationUser { UserName = request.Username };

        _userManagerMock.Setup(u => u.FindByNameAsync(request.Username))
            .ReturnsAsync((ApplicationUser?)null);

        _mapperMock.Setup(m => m.Map<ApplicationUser>(request)).Returns(user);

        _userManagerMock.Setup(u => u.CreateAsync(user, request.Password))
            .ReturnsAsync(IdentityResult.Success);

        var service = CreateService();

        await service.CreateAdminAsync(request);
    }

    [Fact]
    public async Task LoginAsync_ShouldThrow_WhenUserNotFound()
    {
        var request = new LoginRequest { Username = "user", Password = "pass" };

        _userManagerMock.Setup(u => u.FindByNameAsync(request.Username))
            .ReturnsAsync((ApplicationUser?)null);

        var service = CreateService();

        await Assert.ThrowsAsync<ValidationException>(() =>
            service.LoginAsync(request));
    }

    [Fact]
    public async Task LoginAsync_ShouldThrow_WhenPasswordInvalid()
    {
        var request = new LoginRequest { Username = "user", Password = "wrong" };
        var user = new ApplicationUser { UserName = request.Username };

        _userManagerMock.Setup(u => u.FindByNameAsync(request.Username))
            .ReturnsAsync(user);

        _signInManagerMock.Setup(s => s.CheckPasswordSignInAsync(user, request.Password, false))
            .ReturnsAsync(SignInResult.Failed);

        var service = CreateService();

        await Assert.ThrowsAsync<ValidationException>(() =>
            service.LoginAsync(request));
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnToken_WhenCredentialsValid()
    {
        var request = new LoginRequest { Username = "user", Password = "123" };
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = request.Username
        };

        _userManagerMock.Setup(u => u.FindByNameAsync(request.Username))
            .ReturnsAsync(user);

        _signInManagerMock.Setup(s => s.CheckPasswordSignInAsync(user, request.Password, false))
            .ReturnsAsync(SignInResult.Success);

        _userManagerMock.Setup(u => u.GetRolesAsync(user))
            .ReturnsAsync(new List<string> { "Admin" });

        _configMock.Setup(c => c["Jwt:Key"]).Returns("supersecretkey12345678901asdAAadsA23456");
        _configMock.Setup(c => c["Jwt:Issuer"]).Returns("test-issuer");
        _configMock.Setup(c => c["Jwt:Audience"]).Returns("test-audience");

        var service = CreateService();

        var result = await service.LoginAsync(request);

        result.Should().NotBeNull();
        result.Token.Should().NotBeNullOrEmpty();
        result.Expires.Should().BeAfter(DateTime.UtcNow);
    }
}
