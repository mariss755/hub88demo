using System.Linq;
using WalletApi.DAL;
using WalletApi.DTOs;
using WalletApi.Enums;

namespace WalletApi.Services
{
    public interface IUserService
    {
        UserInfoDto GetUser(RequestUserInfoDto requestUserInfoDto);
        UserBalanceDto GetUserBalance(RequestUserInfoDto requestUserInfoDto, Status status = Status.RS_OK);
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public UserInfoDto GetUser(RequestUserInfoDto requestUserInfoDto)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == requestUserInfoDto.UserName);

            if (user != null)
            {
                return user.AsDto(requestUserInfoDto.RequestUuid, Status.RS_OK);
            }
            return new UserInfoDto
            {
                RequestUuid = requestUserInfoDto.RequestUuid,
                Status = Status.RS_ERROR_UNKNOWN.ToString()
            };
        }
        
        public UserBalanceDto GetUserBalance(RequestUserInfoDto requestUserInfoDto, Status status = Status.RS_OK)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == requestUserInfoDto.UserName);

            if (user != null)
            {
                return user.AsBalanceDto(requestUserInfoDto.RequestUuid, status);
            }
            return new UserBalanceDto {
                RequestUuid = requestUserInfoDto.RequestUuid,
                Status = Status.RS_ERROR_UNKNOWN.ToString()
            };
        }
    }
}