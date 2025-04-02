namespace MinBankAPI.Models
{
    public class WithdrawFromAccount
    {
        public int Id { get; set; }
        public string accountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
