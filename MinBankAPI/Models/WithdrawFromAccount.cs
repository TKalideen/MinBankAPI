using System.ComponentModel.DataAnnotations;

namespace MinBankAPI.Models
{
    public class WithdrawFromAccount
    {
        [Key]
        public long idNumber { get; set; }
        public string accountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
