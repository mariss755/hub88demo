using System;
using System.Collections.Generic;

namespace WalletApi.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Balance { get; set; }
        public Country Country { get; set; }
        public Currency Currency { get; set; }

        public List<Transaction>? Transactions { get; set; }
        
    }
}