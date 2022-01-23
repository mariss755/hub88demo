using Microsoft.AspNetCore.Mvc;
using WalletApi.DTOs;
using WalletApi.Services;

namespace WalletApi.Controllers
{
    [ApiController]
    [Route("transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        private readonly UserService _userService;
        
        public TransactionController(TransactionService transactionService, UserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }

        [HttpPost("win")]
        public ActionResult<UserBalanceDto> TransactionWin([FromBody] TransactionWinDto transactionWinDto)
        {
            var response = _transactionService.IncreaseUserBalance(transactionWinDto);

            return _userService.GetUserBalance(new RequestUserInfoDto
            {
                RequestUuid = transactionWinDto.RequestUuid, 
                UserName = transactionWinDto.UserName
            }, 
                response);
        }

        [HttpPost("bet")]
        public ActionResult<UserBalanceDto> TransactionBet([FromBody] TransactionBetDto transactionBetDto)
        {
            var response = _transactionService.DecreaseUserBalance(transactionBetDto);
            
            var trans = _userService.GetUserBalance(new RequestUserInfoDto
                {
                    RequestUuid = transactionBetDto.RequestUuid, 
                    UserName = transactionBetDto.UserName
                }, 
                response);
            return trans;
        }
    }
}