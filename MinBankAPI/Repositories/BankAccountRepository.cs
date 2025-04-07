using Microsoft.EntityFrameworkCore;
using MinBankAPI.Data;
using MinBankAPI.Interfaces;
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

        public async Task<(bool success, string message)> WithdrawalAsync(string accountNum, decimal amountWithdrawwn)
        {
            try
            {
                var account = await accountsDbContext.BankAccounts
                    .FirstOrDefaultAsync(b => b.accountNumber == accountNum);

                if (account == null)
                    return (false, "Account not found.");

                // Condition 1: Withdrawal amount must be greater than 0
                if (amountWithdrawwn <= 0)
                    return (false, "Withdrawal amount must be greater than zero.");

                // Condition 2: Withdrawal amount cannot exceed available balance
                if (amountWithdrawwn > account.availableBalance)
                    return (false, "Insufficient balance.");

                // Condition 3: Fixed Deposit accounts must be fully withdrawn
                if (account.accountType == "Fixed Deposit" && amountWithdrawwn != account.availableBalance)
                    return (false, "You must withdraw the full balance for a Fixed Deposit account.");

                // Condition 4: Withdrawals are not allowed on inactive accounts
                if (account.accountStatus == "Inactive")
                    return (false, "Withdrawals are not allowed on inactive accounts.");

                // Perform withdrawal
                account.availableBalance -= amountWithdrawwn;
                accountsDbContext.Withdrawals.Add(new WithdrawFromAccount
                {
                    accountNumber = accountNum,
                    Amount = amountWithdrawwn
                });

                await accountsDbContext.SaveChangesAsync();
                return (true, "Withdrawal successful.");
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return (false, "An unexpected error occurred.");
            }
        }
    }
}
