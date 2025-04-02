using Microsoft.EntityFrameworkCore;
using MinBankAPI.Models;

namespace MinBankAPI.Data
{
    public class AccountsDbContext : DbContext
    {
        public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options) { }

        public DbSet<BankAccounts> BankAccounts { get; set; }
        public DbSet<WithdrawFromAccount> Withdrawals { get; set; }
    }
}
