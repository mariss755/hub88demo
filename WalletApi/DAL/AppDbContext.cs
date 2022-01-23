using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WalletApi.Entities;
using WalletApi.Enums;

namespace WalletApi.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Transaction> Transactions { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = new Guid("ca65ca5e-3e01-4693-9324-69ade8268292"),
                    UserName = "user1234",
                    Balance = 1000000,
                    BirthDate = new DateTime(1980, 01, 01),
                    Country = Country.BE,
                    Currency = Currency.EUR,
                    RegistrationDate = DateTime.Now.AddDays(-1)
                },
                new User
                {
                    Id = new Guid("b3b2b52a-ab5d-4bee-a15c-cfff0f3dff02"),
                    UserName = "user4567",
                    Balance = 0,
                    BirthDate = new DateTime(1997, 02, 02),
                    Country = Country.EE,
                    Currency = Currency.EUR,
                    RegistrationDate = DateTime.Now.AddDays(-2)
                });

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = new Guid("16d2dcfe-b89e-11e7-854a-58404eea6d16"),
                    Amount = 364000,
                    Currency = Currency.EUR,
                    InsertedAt = DateTime.Now,
                    Kind = TransactionKind.TK_BET,
                    UserId = Guid.Parse("ca65ca5e-3e01-4693-9324-69ade8268292")
                });
        }

    }
}