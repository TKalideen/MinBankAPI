using MinBankAPI.Controllers;
using MinBankAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MinBankAPI.Models;
using Moq;

namespace MinBankAPI.Tests
{
    public class APIBankTests
    {
        private readonly Mock<IBankAccountRepository> _mockBankAccountRepo;
        private readonly BankAccountsController _controllerBankAccounts;

        public APIBankTests()
        {
            _mockBankAccountRepo = new Mock<IBankAccountRepository>();
            _controllerBankAccounts = new BankAccountsController(_mockBankAccountRepo.Object);
        }

        [Fact]
        public async Task GetBankAccount_ReturnsBankAccount_WhenAccountExists()
        {
            // Arrange
            var accountNum = "62547896321";
            var bankAcc = new BankAccounts
            {
                idNumber = 8967254579325,
                accountNumber = "62547896321",
                accountType = "Cheque",
                accountHolderName = "Dave Transactional",
                accountStatus = "Active",
                resdentialAddress = "12 Smith Avenue, Durban, 3452",
                cellphoneNumber = "0123678945",
                emailAddress = "Johns@gmail.com",
                availableBalance = 10120.00M
            };

            _mockBankAccountRepo.Setup(repo => repo.GetBankAccountAsync(accountNum))
                .ReturnsAsync(bankAcc);

            // Act
            var result = await _controllerBankAccounts.GetAccount(accountNum);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BankAccounts>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedAccount = Assert.IsType<BankAccounts>(okResult.Value);
            Assert.Equal(accountNum, returnedAccount.accountNumber);
        }

        [Fact]
        public async Task GetBankAccount_WhenAccount_Does_Not_Exist()
        {
            // Arrange
            var accountNum = "12345";
            _mockBankAccountRepo.Setup(repo => repo.GetBankAccountAsync(accountNum))
                .ReturnsAsync((BankAccounts)null);

            // Act
            var result = await _controllerBankAccounts.GetAccount(accountNum);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BankAccounts>>(result);
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task WithdrawMoney_WhenAmountIsInvalid()
        {
            // Arrange
            var withdrawAccount = new WithdrawFromAccount
            {
                accountNumber = "6263451278",
                Amount = 0
            };

            var bankAccount = new BankAccounts
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
            };

            _mockBankAccountRepo.Setup(x => x.WithdrawalAsync(withdrawAccount.accountNumber, withdrawAccount.Amount))
            .ReturnsAsync((false, "Withdrawal amount must be greater than zero."));

            // Act
            var result = await _controllerBankAccounts.Withdraw(withdrawAccount);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var message = badRequest.Value as string;
            Assert.Equal("Withdrawal amount must be greater than zero.", message);
        }

        [Fact]
        public async Task WithdrawMoney_WhenAccountIsInactive()
        {
            // Arrange
            var withdrawAccount = new WithdrawFromAccount
            {
                accountNumber = "9876543210",
                Amount = 100
            };

            var bankAccount = new BankAccounts
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
            };

            _mockBankAccountRepo.Setup(repo => repo.GetBankAccountAsync(bankAccount.accountNumber))
                .ReturnsAsync(bankAccount);

            _mockBankAccountRepo.Setup(repo => repo.WithdrawalAsync(withdrawAccount.accountNumber, withdrawAccount.Amount))
                .ReturnsAsync((false, "Withdrawals are not allowed on inactive accounts."));

            // Act
            var result = await _controllerBankAccounts.Withdraw(withdrawAccount);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var message = badRequestResult.Value as string;

            Assert.Equal("Withdrawals are not allowed on inactive accounts.", message);
        }

        [Fact]
        public async Task WithdrawMoney_ShouldFail_WhenAccountIsFixedDeposit_AndAmountIsNotFullBalance()
        {
            // Arrange
            var withdrawAccount = new WithdrawFromAccount
            {
                accountNumber = "1234567890", 
                Amount = 500  
            };

            var bankAccount = new BankAccounts
            {
                idNumber = 9356461312518,
                accountNumber = "9057763318",
                accountType = "Fixed",
                accountHolderName = "Sheryl Kalideen Fixed",
                accountStatus = "Active",
                resdentialAddress = "2 Queensbury Avenue, Durban, 4051",
                cellphoneNumber = "0845698752",
                emailAddress = "skali@gmail.com",
                availableBalance = 125000.00M
            };

            _mockBankAccountRepo.Setup(repo => repo.GetBankAccountAsync(bankAccount.accountNumber))
                .ReturnsAsync(bankAccount);

            _mockBankAccountRepo.Setup(repo => repo.WithdrawalAsync(withdrawAccount.accountNumber, withdrawAccount.Amount))
                .ReturnsAsync((false, "You must withdraw the full balance for a Fixed Deposit account."));

            // Act
            var result = await _controllerBankAccounts.Withdraw(withdrawAccount);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var message = badRequestResult.Value as string;

            // Ensure that the correct error message is returned
            Assert.Equal("You must withdraw the full balance for a Fixed Deposit account.", message);
        }


        [Fact]
        public async Task WithdrawMoney_ReturnsSuccessl()
        {

            var accNum = "6263451278";
            var amountWithdraw = 500;

            // Arrange
            var withdrawAccount = new WithdrawFromAccount
            {
                accountNumber = "6263451278",
                Amount = 500
            };

            var bankAccount = new BankAccounts
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
            };

            _mockBankAccountRepo.Setup(repo => repo.GetBankAccountAsync(bankAccount.accountNumber))
                .ReturnsAsync(bankAccount);
            _mockBankAccountRepo.Setup(x => x.WithdrawalAsync(It.IsAny<string>(), It.IsAny<decimal>()))
               .ReturnsAsync((true, "Withdrawal successful."));

            // Act
            var result = await _controllerBankAccounts.Withdraw(withdrawAccount);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Withdrawal successful.", actionResult.Value);
        }
    }
}