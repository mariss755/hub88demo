using System.Linq;
using WalletApi.DAL;
using WalletApi.DTOs;
using WalletApi.Entities;

namespace WalletApi.DAOs
{
    public class UserService
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
        
        public UserBalanceDto GetUserBalance(RequestUserInfoDto requestUserInfoDto)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == requestUserInfoDto.UserName);

            if (user != null)
            {
                return user.AsBalanceDto(requestUserInfoDto.RequestUuid, Status.RS_OK);
            }
            return new UserBalanceDto {
                RequestUuid = requestUserInfoDto.RequestUuid,
                Status = Status.RS_ERROR_UNKNOWN.ToString()
            };
        }
    }
}