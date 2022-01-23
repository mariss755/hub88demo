using Microsoft.AspNetCore.Mvc;
using WalletApi.DTOs;
using WalletApi.Services;

namespace WalletApi.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("info")]
        public ActionResult<UserInfoDto> UserInfo([FromBody] RequestUserInfoDto requestUserInfoDto)
        {
            return _userService.GetUser(requestUserInfoDto);

        }

        [HttpPost("balance")]
        public ActionResult<UserBalanceDto> UserBalance([FromBody] RequestUserInfoDto requestUserInfoDto)
        {
            return _userService.GetUserBalance(requestUserInfoDto);
        }
    }
}