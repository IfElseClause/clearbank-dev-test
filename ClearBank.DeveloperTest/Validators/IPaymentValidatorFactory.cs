using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public interface IPaymentValidatorFactory
    {
        IPaymentSchemeValidator GetValidator(PaymentScheme scheme);
    }
}
