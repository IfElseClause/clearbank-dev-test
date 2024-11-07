using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using System.Transactions;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IPaymentSchemeValidatorFactory _validatorFactory;

        public PaymentService(
            IAccountDataStore accountDataStore,
            IPaymentSchemeValidatorFactory validatorFactory)
        {
            _accountDataStore = accountDataStore;
            _validatorFactory = validatorFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    Account account = _accountDataStore.GetAccount(request.DebtorAccountNumber);

                    IPaymentSchemeValidator paymentSchemeValidator = _validatorFactory.GetValidator(request.PaymentScheme);

                    if (account == null
                        || !paymentSchemeValidator.Validate(account.AllowedPaymentSchemes))
                    {
                        return new MakePaymentResult() { Success = false };
                    }

                    var updatedAccount = account with { Balance = account.Balance - request.Amount };

                    _accountDataStore.UpdateAccount(updatedAccount);

                    scope.Complete();
                }
                catch
                {
                    return new MakePaymentResult() { Success = false };
                }
            }

            return new MakePaymentResult() { Success = true };
        }
    }
}
