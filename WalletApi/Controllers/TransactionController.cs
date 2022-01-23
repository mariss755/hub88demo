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

            return new UserBalanceDto();
        }
    }
}