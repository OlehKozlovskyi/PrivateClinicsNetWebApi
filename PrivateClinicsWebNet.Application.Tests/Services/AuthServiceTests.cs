using PrivateClinicsWebNet.Application.Services;
using Moq;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.DataAccess.Abstractions;
using Microsoft.AspNetCore.Identity;
using PrivateClinicsWebNet.Application.DTOs;
using FluentAssertions;
using PrivateClinicsWebNet.Application.Exceptions;
using PrivateClinicsWebNet.Application.Tests.TestData;
using PrivateClinicsWebNet.BusinessLogic.Factories;
using PrivateClinicsWebNet.BusinessLogic.Exceptions;

namespace PrivateClinicsWebNet.Application.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly AuthService _authService;
    private readonly UserFactory _userFactory;

    public AuthServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockTokenService = new Mock<ITokenService>();
        _userFactory = new UserFactory();
        _authService = new AuthService(_mockUserRepository.Object, _mockTokenService.Object, _userFactory);
    }

    #region Login tests
    [Theory]
    [InlineData(null, "password")]
    [InlineData("example@gmail.com", null)]
    [InlineData(null, null)]
    public async Task Login_ShouldThrowNullReferenceException_WhenInputsAreInvalid(string email, string password)
    {
        var loginDto = new LoginDto(email, password);
        var act = async () => await _authService.Login(loginDto);
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task Login_ShouldReturnToken_WhenUserIsAuthorized()
    {
        var loginDto = new LoginDto("user@gmail.com", "12");
        var user = new IdentityUser { UserName = "user@gmail.com", PasswordHash = "12" };
        var expectedToken = "mock_token";
        _mockUserRepository
            .Setup(repository => repository.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync(user);
        _mockUserRepository
            .Setup(repository => repository.CheckPasswordAsync(user, loginDto.Password))
            .ReturnsAsync(true);
        _mockTokenService
            .Setup(service => service.GenerateJwt(user, loginDto.Email))
            .Returns(expectedToken);
        var result = await _authService.Login(loginDto);
        result.Should().BeSameAs(expectedToken);
    }

    [Fact]
    public async Task Login_ShouldThrowUserNotFoundException_WhenEmailIsWrong()
    {
        var loginDto = new LoginDto("user@gmail.com", "1");
        var user = new IdentityUser { UserName = "admin@gmail.com", PasswordHash = "2" };
        _mockUserRepository
            .Setup(repository => repository.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync(user);
        var act = async () => await _authService.Login(loginDto);
        await act.Should().ThrowAsync<UserNotFoundException>();
    }

    [Fact]
    public async Task Login_ShouldThrowUserNotFoundException_WhenUserNotFound()
    {
        var loginDto = new LoginDto("user@gmail.com", "password");
        IdentityUser user = new IdentityUser {UserName ="user123@gmail.com", PasswordHash = "password" };
        _mockUserRepository
            .Setup(repository => repository.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync(user);
        var act = async()=>await _authService.Login(loginDto);
        await act.Should().ThrowAsync<UserNotFoundException>();
    }

    [Fact]
    public async Task Login_ShouldThrowInvalidPasswordException_WhenPasswordIsWrong()
    {
        var loginDto = new LoginDto("user@gmail.com", "1");
        var user = new IdentityUser { UserName = "user@gmail.com", PasswordHash = "2" };
        _mockUserRepository
            .Setup(repository => repository.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync(user);
        _mockUserRepository
            .Setup(repository => repository.CheckPasswordAsync(user, user.PasswordHash))
            .ReturnsAsync(false);
        var act = async () => await _authService.Login(loginDto);
        await act.Should().ThrowAsync<InvalidPasswordException>();
    }
    #endregion

    #region Registration tests
    [Theory]
    [InlineData(null, null, null)]
    [InlineData(null, "password", "Patient")]
    [InlineData("example@gmail.com", null, "Patient")]
    [InlineData("example@gmail.com", "password", null)]
    public async Task Register_ShouldThrowNullReferenceException_WhenIncomingDataInvalid(string email, string password, string role)
    {
        var registerDto = new RegisterDto(email, password, role);
        var act = async () => await _authService.Register(registerDto);
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    public async Task Register_ShouldThrowInvalidUserRoleException_WhenUserRoleUnsupported()
    {
        var registerDto = new RegisterDto("example@gmail.com", "Password", "Manager");
        _mockUserRepository
            .Setup(repository => repository.RegisterUser(It.IsAny<IdentityUser>(), registerDto.Password))
            .ReturnsAsync(IdentityResult.Success);
        var act = async () => await _authService.Register(registerDto);
        await act.Should().ThrowAsync<InvalidUserRoleException>();
    }

    [Theory]
    [ClassData(typeof(UserTestData))]
    public async Task Register_ShouldRegisterNewUser_WhenIncomigDataCorrect(RegisterDto registerDto)
    {
        _mockUserRepository
            .Setup(repository => repository.RegisterUser(It.IsAny<IdentityUser>(), registerDto.Password))
            .ReturnsAsync(IdentityResult.Success);
        var act = async () => await _authService.Register(registerDto);
        await act.Should().NotThrowAsync();
    }
    #endregion
}
