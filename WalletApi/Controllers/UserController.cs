using Microsoft.AspNetCore.Mvc;
using WalletApi.DTOs;
using WalletApi.Services;

namespace WalletApi.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("info")]
        public ActionResult<UserInfoDto> UserInfo(RequestUserInfoDto requestUserInfoDto)
        {
            var foo = requestUserInfoDto;
            return _userService.GetUser(requestUserInfoDto);

        }

        [HttpPost("balance")]
        public ActionResult<UserBalanceDto> UserBalance([FromBody] RequestUserInfoDto requestUserInfoDto)
        {
            return _userService.GetUserBalance(requestUserInfoDto);
        }
    }
}