using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Moq;
using OrderManagementSystem.Controllers;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services.Implementations;
using OrderManagementSystem.Services.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace OrderManagementSystem.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserService> _mockUserService;
    public UserServiceTests()
    {
        _mockUserService = new Mock<IUserService>();
    }

    // GetExpectedUser
    [Fact]
    public async Task GetUserById_ReturnsExpectedUser()
    {
        // Arrange
        var expectedUser = new User
        {
            Email = "shoaib@test.com",
            Password = "Shoaib@123" 
        };
        _mockUserService.Setup(s => s.GetUserByIdAsync(1))
                   .ReturnsAsync(expectedUser);

        // Act
        var result = await _mockUserService.Object.GetUserByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUser.Email, result.Email);
        Assert.Equal(expectedUser.Password, result.Password);
    }

    // Case For When Id Is Invalid
    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public async Task GetUserById_ReturnsNull_WhenIdIsInvalid(int invalidId)
    {

        _mockUserService.Setup(service => service.GetUserByIdAsync(It.Is<int>(i => i <= 0)))
          .ReturnsAsync((User?)null);

        var result = await _mockUserService.Object.GetUserByIdAsync(invalidId);

        Assert.Null(result);
    }

    // Case For When User Not Found
    [Fact]
    public async Task GetUserById_ReturnsNull_WhenUserNotFound()
    {

        _mockUserService.Setup(service => service.GetUserByIdAsync(999))
                   .ReturnsAsync((User?)null);

        var result = await _mockUserService.Object.GetUserByIdAsync(999);

        Assert.Null(result);
    }


}

