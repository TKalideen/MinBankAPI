using System.ComponentModel.DataAnnotations;

namespace MinBankAPI.Models
{
    public class BankAccounts
    {
        [Key]
        public long idNumber { get; set; }
        public string accountNumber { get; set; }
        public string accountType { get; set; } // Cheque, Savings, Fixed Deposit
        public string accountHolderName { get; set; }
        public string resdentialAddress { get; set; }
        public string emailAddress { get; set; }
        public string cellphoneNumber { get; set; }
        public string accountStatus { get; set; } // Active, Inactive
        public decimal availableBalance { get; set; }
    }
}
