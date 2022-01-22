using Microsoft.EntityFrameworkCore;
using WalletApi.Entities;

namespace WalletApi.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Transaction> Transactions { get; set; } = default!;
    }
}