using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    internal class FasterPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public bool Validate(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            if (!allowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            {
                return false;
            }

            return true;
        }
    }
}
