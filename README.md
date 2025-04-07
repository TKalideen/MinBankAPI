# MinBankAPI - API Documentation

This is a demo API for managing bank account details, withdrawals, and balance checks. The API allows users to interact with the bank account system through the following endpoints. Authorization is done via JWT (JSON Web Token) using Swagger, which you can use to interact with the API.

## API Endpoints

### 1. **Get Account Details by Account Holder Name**
   - **Method**: `GET`
   - **Endpoint**: `/api/accounts/BankAccounts/{accountHolderName}`
   - **Description**: Retrieve account details by providing the account holder's name.
   - **Example Request**:  
     `GET /api/accounts/BankAccounts/John’s Savings`
   - **Response**: Returns the details of the bank account associated with the account holder's name.

### 2. **Get Account Details by Account Number**
   - **Method**: `GET`
   - **Endpoint**: `/api/accounts/BankAccounts/{accountNumber}`/api/BankAccounts/account/{accountNumber}
   - **Description**: Retrieve account details by providing the account number.
   - **Example Request**:  
     `GET /api/BankAccounts/accounts/6263451278`
   - **Response**: Returns the details of the bank account associated with the provided account number.

### 3. **Withdraw Funds**
   - **Method**: `POST`
   - **Endpoint**: `/api/BankAccounts/withdraw`
   - **Description**: Withdraw a specified amount from an account.
   - **Example Request**:  
     `POST /api/BankAccounts/withdraw`  
     Request Body:
     ```json
     {
         "accountNumber": "6263451278",
         "amount": 100.00
     }
     ```
   - **Response**: Returns a success or failure message depending on the withdrawal status.
## Unit Tests

The project includes unit tests that cover 6 different scenarios to ensure the correct functionality of the bank account operations. These tests are part of the solution and use **mock data** to simulate real-world scenarios.

### Test Scenarios

1. **Test for Valid Withdrawal**:  
   Ensures that when valid data is provided, the withdrawal operation is successfully completed.
   
2. **Test for Invalid Withdrawal Amount (Zero or Negative)**:  
   Ensures that the withdrawal operation fails when the amount is zero or negative.
   
3. **Test for Insufficient Balance**:  
   Ensures that the withdrawal operation fails when the amount exceeds the available balance.
   
4. **Test for Fixed Deposit Account (Full Balance Withdrawal Required)**:  
   Ensures that only full withdrawals are allowed for Fixed Deposit accounts. The withdrawal will fail if the full balance isn't withdrawn.
   
5. **Test for Inactive Account Status**:  
   Ensures that withdrawals are not allowed for accounts that have an "Inactive" status.
   
6. **Test for Missing Account (Account Not Found)**:  
   Ensures that the operation returns an appropriate error when the account does not exist.

### How the Tests Are Structured

- **Mock Data**: Mock data is used to simulate actual database behavior. The unit tests are independent of the real database, focusing on business logic and service layer behavior.
  
- **Moq**: The tests make use of **Moq**, a library to mock dependencies such as the repository. This allows the controller logic to be isolated and tested without the need for actual database interactions.
  
- **Test Logic**: Each test case evaluates different conditions of the withdrawal process:
   - Whether the withdrawal is valid.
   - Whether invalid or edge cases (like insufficient balance or inactive status) are handled correctly.
   - If accounts are handled correctly in edge cases like missing accounts.

By using mock data and Moq, the tests ensure that the application behaves as expected under various scenarios, allowing for better code reliability and correctness in production environments.

## Authorization via Swagger

1. **Login Endpoint**:  
   First, you must click on the **login** endpoint to obtain a JWT token.  
   - **Endpoint**: `/api/auth/login`
   - **Description**: Generate a token by providing valid credentials (this can be hardcoded for testing purposes).

2. **Generate the Token**:  
   After calling the login endpoint, you will receive a JWT token. Copy this token.

3. **Authorize in Swagger**:  
   On the Swagger UI, click the **Authorize** button located at the top right corner of the page.  
   Paste the JWT token into the dialog box and click **Authorize**. This will allow you to access the protected endpoints.

4. **Test Each Endpoint**:  
   Once authorized, you can proceed to test each of the available endpoints. Make sure to pass the required parameters, such as account holder name, account number, and amount for withdrawals.

## Database Setup

### SQL Server Configuration

- **Local SQL Server**:  
  This project uses **SQL Server** for database management. SQL Server must be running locally on your machine for the application to work properly.

- **SQL Server Management Studio (SSMS)**:  
  You can use **SQL Server Management Studio (SSMS)** to view and manage the database. Ensure you have connected to your local SQL Server instance.

- **Migrations**:  
  The project includes a migration script that will automatically set up the database schema when you run the application for the first time. Seed data is also included in the migration, so you will have sample data in the database after migration.

  You can apply migrations using the following command:

