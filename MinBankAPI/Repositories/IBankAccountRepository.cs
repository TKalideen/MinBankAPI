using MinBankAPI.Models;

namespace MinBankAPI.Repositories
{
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccounts>> GetBankAccountsAsync(string accountHolderName);
        Task<BankAccounts> GetBankAccountAsync(string accountNumber);
        Task<bool> WithdrawalAsync(string accountNumber, decimal amount);
    }
}
