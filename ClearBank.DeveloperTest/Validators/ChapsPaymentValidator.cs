using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    internal class ChapsPaymentValidator : IPaymentSchemeValidator
    {
        public bool Validate(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            if (!allowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                return false;
            }

            return true;
        }
    }
}
