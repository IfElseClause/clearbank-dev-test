using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using Microsoft.Extensions.Logging;
using System;
using System.Transactions;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IPaymentSchemeValidatorFactory _validatorFactory;
        private readonly ILogger<PaymentService> _logger;
        private readonly IBalanceValidator _balanceValidator;
        public PaymentService(
            IAccountDataStore accountDataStore,
            IPaymentSchemeValidatorFactory validatorFactory,
            ILogger<PaymentService> logger,
            IBalanceValidator balanceValidator)
        {
            _accountDataStore = accountDataStore;
            _validatorFactory = validatorFactory;
            _logger = logger;
            _balanceValidator = balanceValidator;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            _logger.LogInformation("Starting payment process for debtor account {DebtorAccountNumber} with amount {Amount} and payment scheme {PaymentScheme}.", request.DebtorAccountNumber, request.Amount, request.PaymentScheme);

            try
            {
                Account account = _accountDataStore.GetAccount(request.DebtorAccountNumber);
                if (account == null)
                {
                    _logger.LogWarning("Account not found for debtor account number {DebtorAccountNumber}. Payment cannot proceed.", request.DebtorAccountNumber);
                    return new MakePaymentResult() { Success = false };
                }

                _logger.LogInformation("Account found for debtor account number {DebtorAccountNumber} with balance {Balance}.",
                                       account.AccountNumber, account.Balance);

                IPaymentSchemeValidator paymentSchemeValidator = _validatorFactory.GetValidator(request.PaymentScheme);
                if (!paymentSchemeValidator.Validate(account.AllowedPaymentSchemes))
                {
                    _logger.LogWarning("Payment scheme validation failed for account {DebtorAccountNumber} and payment scheme {PaymentScheme}.", account.AccountNumber, request.PaymentScheme);
                    return new MakePaymentResult() { Success = false };
                }

                if (!_balanceValidator.HasSufficientBalance(account.Balance, request.Amount))
                {
                    _logger.LogWarning("Insufficient balance for account {DebtorAccountNumber}. Payment amount: {Amount}, Account balance: {Balance}.", account.AccountNumber, request.Amount, account.Balance);
                    return new MakePaymentResult() { Success = false };
                }

                using (var scope = new TransactionScope())
                {
                    var updatedAccount = account with { Balance = account.Balance - request.Amount };

                    _accountDataStore.UpdateAccount(updatedAccount);
                    _logger.LogInformation("Account {DebtorAccountNumber} balance updated from {OldBalance} to {NewBalance}.", account.AccountNumber, account.Balance, updatedAccount.Balance);
                    scope.Complete();
                }

                _logger.LogInformation("Payment process completed successfully for debtor account {DebtorAccountNumber}.", request.DebtorAccountNumber);
                return new MakePaymentResult() { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the payment for debtor account {DebtorAccountNumber}.", request.DebtorAccountNumber);
                return new MakePaymentResult() { Success = false };
            }
        }
    }
}
