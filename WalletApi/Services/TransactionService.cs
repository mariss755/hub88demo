using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WalletApi.DAL;
using WalletApi.DTOs;
using WalletApi.Entities;
using WalletApi.Enums;

namespace WalletApi.Services
{
    public interface ITransactionService
    {
        Status IncreaseUserBalance(TransactionWinDto transactionWinDto);
        Status DecreaseUserBalance(TransactionBetDto transactionBetDto);
        Status RollbackTransaction(TransactionRollbackDto transactionRollbackDto);
    }

    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        
        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public Status IncreaseUserBalance(TransactionWinDto transactionWinDto)
        {
            var user = _context.Users
                .Include(usr => usr.Transactions)
                .SingleOrDefault(u => u.UserName == transactionWinDto.UserName);

            var error = CheckTransactionWinErrors(user, transactionWinDto);
            
            if (error != Status.RS_OK)
            {
                return error;
            }

            var transaction = new Transaction
            {
                Id = new Guid(transactionWinDto.TransactionUuid),
                Amount = transactionWinDto.Amount,
                Currency = (Currency) Enum.Parse(typeof(Currency), transactionWinDto.Currency),
                InsertedAt = DateTime.Now,
                Kind = TransactionKind.TK_WIN,
                ReferenceTransactionId = new Guid(transactionWinDto.ReferenceTransactionUuid),
                UserId = user!.Id
            };

            user!.Transactions!.Add(transaction);
            user.Balance += transactionWinDto.Amount;
            
            _context.Transactions.Add(transaction);
            _context.Users.Update(user);
            _context.SaveChanges();
            
            return Status.RS_OK;
            
        }

        private bool TransactionExists(string transactionUuid)
        {
            var transaction = _context.Transactions.SingleOrDefault(t => t.Id == new Guid(transactionUuid));

            return transaction != null;
        }

        public Status DecreaseUserBalance(TransactionBetDto transactionBetDto)
        {
            var user = _context.Users
                .Include(usr => usr.Transactions)
                .SingleOrDefault(u => u.UserName == transactionBetDto.UserName);

            var error = CheckTransactionBetErrors(user, transactionBetDto);

            if (error != Status.RS_OK)
            {
                return error;
            }

            var transaction = new Transaction
            {
                Id = new Guid(transactionBetDto.TransactionUuid),
                Amount = transactionBetDto.Amount,
                Currency = (Currency) Enum.Parse(typeof(Currency), transactionBetDto.Currency),
                InsertedAt = DateTime.Now,
                Kind = TransactionKind.TK_BET,
                UserId = user!.Id
            };
            user!.Transactions!.Add(transaction);
            
            user.Balance -= transactionBetDto.Amount;

            _context.Transactions.Add(transaction);
            _context.Users.Update(user);
            _context.SaveChanges();
            
            return Status.RS_OK;
            
        }
        
        public Status RollbackTransaction(TransactionRollbackDto transactionRollbackDto)
        {
            var user = _context.Users
                .Include(usr => usr.Transactions)
                .SingleOrDefault(u => u.UserName == transactionRollbackDto.UserName);
            
            var error = CheckTransactionRollbackErrors(user, transactionRollbackDto);

            if (error != Status.RS_OK)
            {
                return error;
            }

            var transactionToRollback = user!.Transactions!
                    .SingleOrDefault(t => t.Id == new Guid(transactionRollbackDto.ReferenceTransactionUuid));
            
            if (transactionToRollback != null)
            {
                if (transactionToRollback.Kind == TransactionKind.TK_BET)
                {
                    user.Balance += transactionToRollback.Amount;
                } else if (transactionToRollback.Kind == TransactionKind.TK_WIN)
                {
                    user.Balance -= transactionToRollback.Amount;
                }

                _context.Transactions.Remove(transactionToRollback);
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            return Status.RS_OK;

        }

        private Status CheckTransactionRollbackErrors(User? user, TransactionRollbackDto transactionRollbackDto)
        {
            if (user == null)
            {
                return Status.RS_ERROR_UNKNOWN;
            }
            if (TransactionExists(transactionRollbackDto.TransactionUuid))
            {
                return Status.RS_ERROR_DUPLICATE_TRANSACTION;
            }
            
            return Status.RS_OK;
        }

        private Status CheckTransactionWinErrors(User? user, TransactionWinDto transactionWinDto)
        {
            if (user == null)
            {
                return Status.RS_ERROR_UNKNOWN;
            }

            if (TransactionExists(transactionWinDto.TransactionUuid))
            {
                return Status.RS_ERROR_DUPLICATE_TRANSACTION;
            }

            if (transactionWinDto.Currency != user.Currency.ToString())
            {
                return Status.RS_ERROR_WRONG_CURRENCY;
            }

            if (!TransactionExists(transactionWinDto.ReferenceTransactionUuid))
            {
                return Status.RS_ERROR_TRANSACTION_DOES_NOT_EXIST;
            }

            return Status.RS_OK;
        }

        private Status CheckTransactionBetErrors(User? user, TransactionBetDto transactionBetDto)
        {
            if (user == null)
            {
                return Status.RS_ERROR_UNKNOWN;
            }
            
            if (TransactionExists(transactionBetDto.TransactionUuid))
            {
                return Status.RS_ERROR_DUPLICATE_TRANSACTION;
            }
            
            if (transactionBetDto.Currency != user.Currency.ToString())
            {
                return Status.RS_ERROR_WRONG_CURRENCY;
            }

            if (transactionBetDto.Amount > user.Balance)
            {
                return Status.RS_ERROR_NOT_ENOUGH_MONEY;
            }

            return Status.RS_OK;
        }

        
    }
}