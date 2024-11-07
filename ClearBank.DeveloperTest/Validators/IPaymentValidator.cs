using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public interface IPaymentValidator
    {
        bool Validate(Account account, MakePaymentRequest paymentRequest);
    }
}
