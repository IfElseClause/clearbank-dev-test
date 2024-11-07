using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClearBank.DeveloperTest.Validators
{
    internal class PaymentSchemeValidatorFactory : IPaymentSchemeValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentSchemeValidatorFactory(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentSchemeValidator GetValidator(PaymentScheme scheme)
        {
            return scheme switch
            {
                PaymentScheme.Bacs => _serviceProvider.GetRequiredService<BacsPaymentSchemeValidator>(),
                PaymentScheme.FasterPayments => _serviceProvider.GetRequiredService<FasterPaymentSchemeValidator>(),
                PaymentScheme.Chaps => _serviceProvider.GetRequiredService<ChapsPaymentSchemeValidator>(),
                _ => throw new ArgumentException("Invalid payment scheme", nameof(scheme))
            };
        }
    }
}
