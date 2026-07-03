using API.Services.IServices;
using Business.IServices;
using Moq;
using Repository.DTOs.Auth;
using Repository.IRepository;
using Repository.Models;

namespace Business.Tests;



public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IJwtService> _mockJwtService;
    private readonly IAuthService _authService;
    
    

    public AuthServiceTests()
    {
        _mockJwtService = new Mock<IJwtService>();
        _mockUserRepository = new Mock<IUserRepository>();
        _authService= new AuthServices(_mockUserRepository.Object, _mockJwtService.Object);
    }

    [Fact]
    public async Task RegisterAsync_withValidRequest_returnAuthResponse()
    {
        // Arrange
        var registerRequest = new RegisterRequestDto{
            Username = "Pratyush",
            Email = "Pratyush@example.com",
            Password = "Pratyush@1234",
            FirstName = "Pratyush",
            LastName = "Kumar"
        };
        _mockUserRepository.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync((User?) null);
        
        _mockUserRepository.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User?) null);

        _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);
        
        var expectedToken= "TestToken";
        
        _mockJwtService.Setup(x=>x.GenerateToken(It.IsAny<User>()))
            .Returns(expectedToken);
        
        //Act
        var result = await _authService.RegisterAsync(registerRequest);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(registerRequest.Username, result.Username);
        Assert.Equal(registerRequest.Email, result.Email);
        Assert.Equal(registerRequest.FirstName, result.FirstName);
        Assert.Equal(registerRequest.LastName, result.LastName);
        Assert.Equal(expectedToken, result.Token);
        Assert.NotNull(result.UserId);
        
        _mockUserRepository.Verify(
            x => x.GetUserByUsernameAsync(registerRequest.Username), 
            Times.Once
        );
        _mockUserRepository.Verify(
            x => x.GetUserByEmailAsync(registerRequest.Email), 
            Times.Once
        );
        _mockJwtService.Verify(x=>x.GenerateToken(
            It.IsAny<User>()),
            Times.Once
        );
    }
}

