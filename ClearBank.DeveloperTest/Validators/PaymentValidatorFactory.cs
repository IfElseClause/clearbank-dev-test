using ClearBank.DeveloperTest.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClearBank.DeveloperTest.Validators
{
    internal class PaymentValidatorFactory : IPaymentValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentValidatorFactory(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentSchemeValidator GetValidator(PaymentScheme scheme)
        {
            return scheme switch
            {
                PaymentScheme.Bacs => _serviceProvider.GetRequiredService<BacsPaymentValidator>(),
                PaymentScheme.FasterPayments => _serviceProvider.GetRequiredService<FasterPaymentsValidator>(),
                PaymentScheme.Chaps => _serviceProvider.GetRequiredService<ChapsPaymentValidator>(),
                _ => throw new ArgumentException("Invalid payment scheme", nameof(scheme))
            };
        }
    }
}
