using Moq;
using OrderManagementSystem.Services.Implementations;
using OrderManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Tests
{
    public class AuthserviceTestFile
    {
        private readonly Mock<IAuthService> _mockAuthService;
        public AuthserviceTestFile()
        {
            _mockAuthService = new Mock<IAuthService>();
        }

        // Check For Email is Registered.
        [Fact]
        public async Task IsEmailRegisteredAsync_ReturnsTrue_WhenRegistered()
        {
            _mockAuthService.Setup(r => r.IsEmailRegisteredAsync("shoaib@test.com")).ReturnsAsync(true);

            var result = await _mockAuthService.Object.IsEmailRegisteredAsync("shoaib@test.com");

            Assert.True(result);
        }

        // Check For Email not Registered.
        [Fact]
        public async Task IsEmailRegisteredAsync_ReturnsFalse_WhenNotRegistered()
        {
            
            _mockAuthService
              .Setup(r => r.IsEmailRegisteredAsync("john@test.com"))
              .ReturnsAsync(false);

            var result = await _mockAuthService.Object.IsEmailRegisteredAsync("john@test.com");

            Assert.False(result);
        }

        // IsValid Password check 
        [Theory]
        [InlineData("Pass1234", true)]
        [InlineData("short", false)]
        [InlineData("nocaps123", false)]
        [InlineData("NoNumber!", false)]
        public void IsValidPassword_ChecksVariousConditions(string password, bool expected)
        {

            _mockAuthService.Setup(s => s.IsValidPassword(password)).Returns(expected);

            var result = _mockAuthService.Object.IsValidPassword(password);

            Assert.Equal(expected, result);
        }


    }
}
