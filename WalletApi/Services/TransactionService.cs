using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WalletApi.DAL;
using WalletApi.DTOs;
using WalletApi.Entities;
using WalletApi.Enums;

namespace WalletApi.Services
{
    public class TransactionService
    {
        private readonly AppDbContext _context;
        
        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public Status IncreaseUserBalance(TransactionWinDto transactionWinDto)
        {
            var user = _context.Users.Include(usr => usr.Transactions).SingleOrDefault(u => u.UserName == transactionWinDto.UserName);
            
            if (user == null)
            {
                return Status.RS_ERROR_UNKNOWN;
            }

            if (TransactionExists(transactionWinDto.TransactionUuid))
            {
                return Status.RS_ERROR_DUPLICATE_TRANSACTION;
            }

            if (transactionWinDto.Currency != user.Currency)
            {
                return Status.ERROR_WRONG_CURRENCY;
            }

            if (!TransactionExists(transactionWinDto.ReferenceTransationUuid))
            {
                return Status.RS_ERROR_TRANSACTION_DOES_NOT_EXIST;
            }

            user.Transactions!.Add(new Transaction
            {
                Id = new Guid(transactionWinDto.TransactionUuid),
                Amount = transactionWinDto.Amount,
                Currency = transactionWinDto.Currency,
                InsertedAt = DateTime.Now,
                Kind = TransactionKind.TK_WIN,
                ReferenceTransactionId = new Guid(transactionWinDto.ReferenceTransationUuid),
                UserId = user.Id
            });
            user.Balance += transactionWinDto.Amount;

            _context.SaveChanges();
            
            return Status.RS_OK;


        }

        private bool TransactionExists(string transactionUuid)
        {
            var transaction = _context.Transactions.SingleOrDefault(t => t.Id == new Guid(transactionUuid));

            return transaction != null;
        }
    }
}