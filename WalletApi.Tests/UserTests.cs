using System;
using FluentAssertions;
using Moq;
using WalletApi.Controllers;
using WalletApi.DTOs;
using WalletApi.Enums;
using WalletApi.Services;
using Xunit;

namespace WalletApi.Tests
{
    public class UserTests
    {
        private readonly UserController _sut;
        private readonly Mock<IUserService> _userServiceMock;

        public UserTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _sut = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public void Return_user_info()
        {
            var requestDto = new RequestUserInfoDto()
            {
                RequestUuid = "2f43a951-3d4c-4406-9fd9-a342bc0c3200",
                UserName = "testUser1"
            };
            
            var userInfoDto = new UserInfoDto()
            {
                User = "testUser1",
                RequestUuid = "2f43a951-3d4c-4406-9fd9-a342bc0c3200",
                Country = Country.EE.ToString(),
                Status = Status.RS_OK.ToString(),
                BirthDate = new DateTime(1995, 05, 05).ToString("yyyy-MM-dd"),
                RegistrationDate = DateTime.Today.ToString("yyyy-MM-dd")
            };
            
            _userServiceMock.Setup(x => x.GetUser(requestDto)).Returns(userInfoDto);
            
            // Act
            var result = _sut.UserInfo(requestDto);
            
            // Assert
            result.Value.Should().Be(userInfoDto);
        }
        
        [Fact]
        public void Return_error_when_data_missing()
        {
            var requestDto = new RequestUserInfoDto()
            {
                UserName = "testUser1"
            };
            
            _userServiceMock.Setup(x => x.GetUser(requestDto)).Returns((UserInfoDto)null!);
            
            // Act
            var result = _sut.UserInfo(requestDto);
            
            // Assert
            result.Value.Should().BeNull();
        }

        [Fact]
        public void Return_user_balance()
        {
            var requestDto = new RequestUserInfoDto()
            {
                RequestUuid = "2f43a951-3d4c-4406-9fd9-a342bc0c3200",
                UserName = "testUser1"
            };

            var userBalanceDto = new UserBalanceDto()
            {
                User = "testUser1",
                RequestUuid = "2f43a951-3d4c-4406-9fd9-a342bc0c3200",
                Balance = 100000,
                Currency = Currency.EUR.ToString(),
                Status = Status.RS_OK.ToString()
            };
            
            _userServiceMock.Setup(x => x.GetUserBalance(requestDto, Status.RS_OK))
                .Returns(userBalanceDto);
            
            // Act
            var result = _sut.UserBalance(requestDto);
            
            // Assert
            result.Value.Should().Be(userBalanceDto);
        }
    }
}