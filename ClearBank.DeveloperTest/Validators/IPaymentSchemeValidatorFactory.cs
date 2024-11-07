using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public interface IPaymentSchemeValidatorFactory
    {
        IPaymentSchemeValidator GetValidator(PaymentScheme scheme);
    }
}
