using MinBankAPI.Models;

namespace MinBankAPI.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccounts>> GetBankAccountsAsync(string accountHolderName);
        Task<BankAccounts> GetBankAccountAsync(string accountNumber);
        Task<(bool success, string message)> WithdrawalAsync(string accountNum, decimal amount);
    }
}
