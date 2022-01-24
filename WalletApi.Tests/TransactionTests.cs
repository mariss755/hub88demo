using FluentAssertions;
using Moq;
using WalletApi.Controllers;
using WalletApi.DTOs;
using WalletApi.Enums;
using WalletApi.Services;
using Xunit;

namespace WalletApi.Tests
{
    public class TransactionTests
    {
        private readonly TransactionController _sut;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ITransactionService> _transactionServiceMock;
        
        public TransactionTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _transactionServiceMock = new Mock<ITransactionService>();
            _sut = new TransactionController(_transactionServiceMock.Object, _userServiceMock.Object);
        }

        [Fact]
        public void Transaction_bet_returns_user_balance()
        {
            var transactionDto = new TransactionBetDto()
            {
                UserName = "testUser",
                Amount = 50000,
                Currency = Currency.ADA.ToString(),
                RequestUuid = "89b37ae7-434c-449c-80c9-06b0b46e32c5",
                TransactionUuid = "c4a5ef4a-3578-4161-b71b-54474e455d70"
            };
            
            var userBalanceDto = new UserBalanceDto()
            {
                User = "testUser",
                RequestUuid = "89b37ae7-434c-449c-80c9-06b0b46e32c5",
                Balance = 100000,
                Currency = Currency.EUR.ToString(),
                Status = Status.RS_OK.ToString()
            };
            
            var requestDto = new RequestUserInfoDto()
            {
                RequestUuid = "89b37ae7-434c-449c-80c9-06b0b46e32c5",
                UserName = "testUser"
            };

            _transactionServiceMock.Setup(x => x.DecreaseUserBalance(transactionDto)).Returns(Status.RS_OK);
            _userServiceMock.Setup(x => x.GetUserBalance(It.Is<RequestUserInfoDto>(x => 
                x.RequestUuid == requestDto.RequestUuid 
                && x.UserName == requestDto.UserName), 
                    Status.RS_OK))
                .Returns(userBalanceDto);
            
            // Act
            var result = _sut.TransactionBet(transactionDto);

            // Assert
            result.Value.Should().Be(userBalanceDto);
        }

        [Fact]
        public void Transaction_win_returns_user_balance()
        {
            var transactionDto = new TransactionWinDto()
            {
                UserName = "testUser",
                Amount = 50000,
                Currency = Currency.EUR.ToString(),
                RequestUuid = "89b37ae7-434c-449c-80c9-06b0b46e32c5",
                TransactionUuid = "c4a5ef4a-3578-4161-b71b-54474e455d70",
                ReferenceTransactionUuid = "8748b544-132c-4e1f-9db6-6fee1e4f0906"
            };
            
            var userBalanceDto = new UserBalanceDto()
            {
                User = "testUser",
                RequestUuid = "89b37ae7-434c-449c-80c9-06b0b46e32c5",
                Balance = 150000,
                Currency = Currency.EUR.ToString(),
                Status = Status.RS_OK.ToString()
            };
            
            var requestDto = new RequestUserInfoDto()
            {
                RequestUuid = "89b37ae7-434c-449c-80c9-06b0b46e32c5",
                UserName = "testUser"
            };
            
            _transactionServiceMock.Setup(x => x.IncreaseUserBalance(transactionDto)).Returns(Status.RS_OK);
            _userServiceMock.Setup(x => x.GetUserBalance(It.Is<RequestUserInfoDto>(x => 
                        x.RequestUuid == requestDto.RequestUuid 
                        && x.UserName == requestDto.UserName), 
                    Status.RS_OK))
                .Returns(userBalanceDto);
            
            // Act
            var result = _sut.TransactionWin(transactionDto);

            // Assert
            result.Value.Should().Be(userBalanceDto);
        }

        [Fact]
        public void Transaction_rollback_returns_user_balance()
        {
            var transactionDto = new TransactionRollbackDto()
            {
                UserName = "testUser",
                RequestUuid = "89b37ae7-434c-449c-80c9-06b0b46e32c5",
                TransactionUuid = "c4a5ef4a-3578-4161-b71b-54474e455d70",
                ReferenceTransactionUuid = "8748b544-132c-4e1f-9db6-6fee1e4f0906"
            };
            
            var userBalanceDto = new UserBalanceDto()
            {
                User = "testUser",
                RequestUuid = "89b37ae7-434c-449c-80c9-06b0b46e32c5",
                Balance = 150000,
                Currency = Currency.EUR.ToString(),
                Status = Status.RS_OK.ToString()
            };
            
            var requestDto = new RequestUserInfoDto()
            {
                RequestUuid = "89b37ae7-434c-449c-80c9-06b0b46e32c5",
                UserName = "testUser"
            };
            
            _transactionServiceMock.Setup(x => x.RollbackTransaction(transactionDto)).Returns(Status.RS_OK);
            _userServiceMock.Setup(x => x.GetUserBalance(It.Is<RequestUserInfoDto>(x => 
                        x.RequestUuid == requestDto.RequestUuid 
                        && x.UserName == requestDto.UserName), 
                    Status.RS_OK))
                .Returns(userBalanceDto);
            
            // Act
            var result = _sut.TransactionRollback(transactionDto);

            // Assert
            result.Value.Should().Be(userBalanceDto);
        }
    }
}