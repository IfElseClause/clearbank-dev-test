using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IPaymentValidatorFactory _validatorFactory;

        public PaymentService(
            IAccountDataStoreFactory accountDataStoreFactory,
            IPaymentValidatorFactory validatorFactory)
        {
            _accountDataStore = accountDataStoreFactory.Create();
            _validatorFactory = validatorFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account = _accountDataStore.GetAccount(request.DebtorAccountNumber);

            IPaymentSchemeValidator paymentSchemeValidator = _validatorFactory.GetValidator(request.PaymentScheme);

            if (account == null || !paymentSchemeValidator.Validate(account.AllowedPaymentSchemes))
            {
                return new MakePaymentResult() { Success = false };
            }

            account.Balance -= request.Amount;
            _accountDataStore.UpdateAccount(account);

            return new MakePaymentResult() { Success = true };
        }
    }
}
