using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    internal class ChapsPaymentValidator : IPaymentValidator
    {
        public bool Validate(Account account, MakePaymentRequest paymentRequest)
        {
            return true;
        }
    }
}
