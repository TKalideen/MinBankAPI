using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MinBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class BankAccountsWithdrawls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    idNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accountHolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resdentialAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cellphoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accountStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    availableBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.idNumber);
                });

            migrationBuilder.CreateTable(
                name: "Withdrawals",
                columns: table => new
                {
                    idNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawals", x => x.idNumber);
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "idNumber", "accountHolderName", "accountNumber", "accountStatus", "accountType", "availableBalance", "cellphoneNumber", "emailAddress", "resdentialAddress" },
                values: new object[,]
                {
                    { 8691190759656L, "Tanay Transaction", "9876543210", "Active", "Cheque", 2500.00m, "0745698745", "tanay.kgmail.com", "2 Muckelnert Avenue, Durban, 3452" },
                    { 8883275546624L, "Jimmy Cheque", "9876533345", "Inactive", "Cheque", 125000.00m, "0835698756", "Johns@gmail.com", "63 Jane Avenue, Dundee, 8795" },
                    { 8965234589125L, "John’s Savings", "6263451278", "Active", "Savings", 100120.00m, "0123678945", "Johns@gmail.com", "12 Smith Avenue, Durban, 3452" },
                    { 9056861318568L, "Suvashka Jugernath Cheque", "9056861318", "Inactive", "Cheque", 12005000.00m, "0765158778", "suvashka.kalideen@gmail.com", "63 Elachie Avenue, Newcastle, 2940" },
                    { 9356461312518L, "Sheryl Kalideen Fixed", "9057763318", "Inactive", "Cheque", 125000.00m, "0845698752", "skali@gmail.com", "2 Queensbury Avenue, Durban, 4051" },
                    { 9859082070719L, "Sanvi Savings", "9876542345", "Active", "Cheque", 125000.00m, "0750246982", "Johns@gmail.com", "3300 LaLucia Ridge Avenue, Cape Town, 7894" },
                    { 95938619222618L, "Jane’s Fixed", "89652345125", "Active", "Fixed Deposit", 5000.00m, "0725698945", "jane.doe@gmail.com", "13 Windswaeltjie Avenue, Newcastle, 2940" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Withdrawals");
        }
    }
}
