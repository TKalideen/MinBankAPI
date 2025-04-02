namespace MinBankAPI.Models
{
    public class BankAccounts
    {
        public int Id { get; set; }
        public string accountNumber { get; set; }
        public string accountType { get; set; } // Cheque, Savings, Fixed Deposit
        public string accountHolderName { get; set; }
        public string accountStatus { get; set; } // Active, Inactive
        public decimal AvailableBalance { get; set; }
    }
}
