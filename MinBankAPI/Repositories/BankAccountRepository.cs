using Microsoft.EntityFrameworkCore;
using MinBankAPI.Data;
using MinBankAPI.Models;

namespace MinBankAPI.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly AccountsDbContext accountsDbContext;

        public BankAccountRepository(AccountsDbContext context)
        {
            accountsDbContext = context;
        }

        public async Task<IEnumerable<BankAccounts>> GetBankAccountsAsync(string accountHolderName)
        {
            return await accountsDbContext.BankAccounts
                .Where(b => b.accountHolderName == accountHolderName)
                .ToListAsync();
        }

        public async Task<BankAccounts> GetBankAccountAsync(string accountNumber)
        {
            return await accountsDbContext.BankAccounts.FirstOrDefaultAsync(b => b.accountNumber == accountNumber);
        }

        public async Task<bool> WithdrawalAsync(string accountNumber, decimal amount)
        {
            var account = await accountsDbContext.BankAccounts.FirstOrDefaultAsync(b => b.accountNumber == accountNumber);
            if (account == null || account.AvailableBalance < amount)
                return false;

            account.AvailableBalance -= amount;
            accountsDbContext.Withdrawals.Add(new WithdrawFromAccount { accountNumber = accountNumber, Amount = amount });
            await accountsDbContext.SaveChangesAsync();
            return true;
        }
    }
}
