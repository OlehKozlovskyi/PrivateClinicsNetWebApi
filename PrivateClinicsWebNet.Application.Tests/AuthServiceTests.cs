using PrivateClinicsWebNet.Application;
using PrivateClinicsWebNet.Application.Services;
using PrivateClinicsWebNet.Application.Abstractions;
using PrivateClinicsWebNet.BusinessLogic.Repositories;
using Moq;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.DataAccess.Abstractions;
using PrivateClinicsWebNet.DataAccess.Services;
using AutoMapper;
using PrivateClinicsWebNet.Application.Mapping;
using Microsoft.AspNetCore.Identity;
using PrivateClinicsWebNet.Application.DTOs;

namespace PrivateClinicsWebNet.Application.Tests;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly AuthService _authService;
    private readonly Mapper _mapper;

    public AuthServiceTests() 
    {
        _mapper = new Mapper(new MapperConfiguration(config => config.AddProfile<UserRegistrationProfileMap>()));
        _mockUserRepository = new Mock<IUserRepository>();
        _mockTokenService = new Mock<ITokenService>();
        _authService = new AuthService(_mockUserRepository.Object, _mockTokenService.Object, _mapper);
    }

    [Fact]
    public async Task Login_ShouldReturnToken_WhenUserIsAuthorized()
    {
        var loginDto = new LoginDto("maria@gmail.com", "12" );
        IdentityUser user = new IdentityUser { UserName = "maria@gmail.com", PasswordHash = "12"};
        var token = "mock_token";
        _mockUserRepository
            .Setup(repository => repository.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync(user);
        _mockTokenService
            .Setup(service => service.GenerateJwt(user, user.Email))
            .Returns(token);
        var result = await _authService.Login(loginDto);
    }
}
