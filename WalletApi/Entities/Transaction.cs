using System;
using Microsoft.AspNetCore.SignalR;
using WalletApi.Enums;

namespace WalletApi.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid? ReferenceTransactionId { get; set; }

        public TransactionKind Kind { get; set; }

        public DateTime InsertedAt { get; set; }

        public Currency Currency { get; set; }
        public int Amount { get; set; }
        
    }
}