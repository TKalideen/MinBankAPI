using Microsoft.EntityFrameworkCore;
using MinBankAPI.Models;

namespace MinBankAPI.Data
{
    public class AccountsDbContext : DbContext
    {
        public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options) { }

        public DbSet<BankAccounts> BankAccounts { get; set; }
        public DbSet<WithdrawFromAccount> Withdrawals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define precision and scale for the decimal properties
            modelBuilder.Entity<BankAccounts>()
                .Property(b => b.availableBalance)
                .HasColumnType("decimal(18,2)");  // Specify precision (18) and scale (2)

            modelBuilder.Entity<WithdrawFromAccount>()
                .Property(w => w.Amount)
                .HasColumnType("decimal(18,2)");  // Specify precision (18) and scale (2)

            // Seed BankAccount
            modelBuilder.Entity<BankAccounts>().HasData(
                new BankAccounts
                {
                    idNumber = 8965234589125,
                    accountNumber = "6263451278",
                    accountType = "Savings",
                    accountHolderName = "John’s Savings",
                    accountStatus = "Active",
                    resdentialAddress = "12 Smith Avenue, Durban, 3452",
                    cellphoneNumber = "0123678945",
                    emailAddress = "Johns@gmail.com",
                    availableBalance = 100120.00M

                },
                new BankAccounts
                {
                    idNumber = 95938619222618,
                    accountNumber = "89652345125",
                    accountType = "Fixed Deposit",
                    accountHolderName = "Jane’s Fixed",
                    accountStatus = "Active",
                    resdentialAddress = "13 Windswaeltjie Avenue, Newcastle, 2940",
                    cellphoneNumber = "0725698945",
                    emailAddress = "jane.doe@gmail.com",
                    availableBalance = 5000.00M
                },
                new BankAccounts
                {
                    idNumber = 8691190759656,
                    accountNumber = "9876543210",
                    accountType = "Cheque",
                    accountHolderName = "Tanay Transaction",
                    accountStatus = "Active",
                    resdentialAddress = "2 Muckelnert Avenue, Durban, 3452",
                    cellphoneNumber = "0745698745",
                    emailAddress = "tanay.kgmail.com",
                    availableBalance = 2500.00M
                },
                new BankAccounts
                {
                    idNumber = 9859082070719,
                    accountNumber = "9876542345",
                    accountType = "Cheque",
                    accountHolderName = "Sanvi Savings",
                    accountStatus = "Active",
                    resdentialAddress = "3300 LaLucia Ridge Avenue, Cape Town, 7894",
                    cellphoneNumber = "0750246982",
                    emailAddress = "Johns@gmail.com",
                    availableBalance = 125000.00M
                },
                new BankAccounts
                {
                    idNumber = 8883275546624,
                    accountNumber = "9876533345",
                    accountType = "Cheque",
                    accountHolderName = "Jimmy Cheque",
                    accountStatus = "Inactive",
                    resdentialAddress = "63 Jane Avenue, Dundee, 8795",
                    cellphoneNumber = "0835698756",
                    emailAddress = "Johns@gmail.com",
                    availableBalance = 125000.00M
                },
                new BankAccounts
                {
                    idNumber = 9056861318568,
                    accountNumber = "9056861318",
                    accountType = "Cheque",
                    accountHolderName = "Suvashka Jugernath Cheque",
                    accountStatus = "Inactive",
                    resdentialAddress = "63 Elachie Avenue, Newcastle, 2940",
                    cellphoneNumber = "0765158778",
                    emailAddress = "suvashka.kalideen@gmail.com",
                    availableBalance = 12005000.00M
                },
                new BankAccounts
                {
                    idNumber = 9356461312518,
                    accountNumber = "9057763318",
                    accountType = "Cheque",
                    accountHolderName = "Sheryl Kalideen Fixed",
                    accountStatus = "Inactive",
                    resdentialAddress = "2 Queensbury Avenue, Durban, 4051",
                    cellphoneNumber = "0845698752",
                    emailAddress = "skali@gmail.com",
                    availableBalance = 125000.00M
                }
            );
        }
    }
}
