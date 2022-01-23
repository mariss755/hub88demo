using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WalletApi.Enums;

namespace WalletApi.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        
        [StringLength(30, MinimumLength = 3)]
        public string UserName { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Balance { get; set; }
        public Country Country { get; set; }
        public Currency Currency { get; set; }

        public List<Transaction>? Transactions { get; set; }
        
    }
}