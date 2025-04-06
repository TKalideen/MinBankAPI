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

        public async Task<bool> WithdrawalAsync(string accountNum, decimal amountWithdrawwn)
        {
            try
            {
                var account = await accountsDbContext.BankAccounts
                    .FirstOrDefaultAsync(b => b.accountNumber == accountNum);

                if (account == null)
                    throw new InvalidOperationException("Account not found.");

                // Condition 1: Withdrawal amount must be greater than 0
                if (amountWithdrawwn <= 0)
                    throw new InvalidOperationException("Withdrawal amount must be greater than zero.");

                // Condition 2: Withdrawal amount cannot exceed available balance
                if (amountWithdrawwn > account.AvailableBalance)
                    throw new InvalidOperationException("Insufficient balance.");

                // Condition 3: Fixed Deposit accounts must be fully withdrawn
                if (account.accountType == "Fixed Deposit" && amountWithdrawwn != account.AvailableBalance)
                    throw new InvalidOperationException("You must withdraw the full balance for a Fixed Deposit account.");

                // Condition 4: Withdrawals are not allowed on inactive accounts
                if (account.accountStatus == "Inactive")
                    throw new InvalidOperationException("Withdrawals are not allowed on inactive accounts.");

                // Perform withdrawal
                account.AvailableBalance -= amountWithdrawwn;
                accountsDbContext.Withdrawals.Add(new WithdrawFromAccount
                {
                    accountNumber = accountNum,
                    Amount = amountWithdrawwn
                });

                await accountsDbContext.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException ex)
            {
                // Known business rule violations
                Console.WriteLine($"Validation error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Unexpected exception
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }
    }
}
