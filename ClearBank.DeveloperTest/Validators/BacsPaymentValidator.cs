using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    internal class BacsPaymentValidator : IPaymentSchemeValidator
    {
        public bool Validate(AllowedPaymentSchemes allowedPaymentSchemes)
        {           
            if (!allowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
            {
                return false;
            }

            return true;
        }
    }
}
