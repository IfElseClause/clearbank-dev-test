using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    internal class FasterPaymentsValidator : IPaymentValidator
    {
        public bool Validate(Account account, MakePaymentRequest paymentRequest)
        {
            return true;
        }
    }
}
