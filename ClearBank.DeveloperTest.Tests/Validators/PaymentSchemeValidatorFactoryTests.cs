using AutoFixture.Xunit2;
using AutoFixture;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;
using System;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class PaymentSchemeValidatorFactoryTests
    {

        private readonly IFixture _fixture;

        public PaymentSchemeValidatorFactoryTests()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _fixture.Register<IServiceProvider>(() =>
            {
                var serviceProvider = Substitute.For<IServiceProvider>();
                serviceProvider.GetService(typeof(BacsPaymentSchemeValidator)).Returns(new BacsPaymentSchemeValidator());
                serviceProvider.GetService(typeof(FasterPaymentSchemeValidator)).Returns(new FasterPaymentSchemeValidator());
                serviceProvider.GetService(typeof(ChapsPaymentSchemeValidator)).Returns(new ChapsPaymentSchemeValidator());
                return serviceProvider;
            });
        }

        [Fact]
        internal void GetValidator_WithBacsPaymentScheme_ReturnsBacsPaymentValidator()
        {
            // Arrange
            var sut = _fixture.Create<PaymentSchemeValidatorFactory>();

            var paymentScheme = PaymentScheme.Bacs;

            // Act
            var result = sut.GetValidator(scheme: paymentScheme);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BacsPaymentSchemeValidator>();
        }

        [Fact]
        internal void GetValidator_WithBacsPaymentScheme_ReturnsFasterPaymentsPaymentValidator()
        {
            // Arrange
            var sut = _fixture.Create<PaymentSchemeValidatorFactory>();

            var paymentScheme = PaymentScheme.FasterPayments;

            // Act
            var result = sut.GetValidator(scheme: paymentScheme);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FasterPaymentSchemeValidator>();
        }

        [Fact]
        internal void GetValidator_WithChapsPaymentScheme_ReturnsChapsPaymentValidator()
        {
            // Arrange
            var sut = _fixture.Create<PaymentSchemeValidatorFactory>();

            var paymentScheme = PaymentScheme.Chaps;

            // Act
            var result = sut.GetValidator(scheme: paymentScheme);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ChapsPaymentSchemeValidator>();
        }

        [Fact]
        internal void GetValidator_WithInvalidPaymentScheme_ThrowsException()
        {
            // Arrange
            var sut = _fixture.Create<PaymentSchemeValidatorFactory>();

            PaymentScheme paymentScheme = (PaymentScheme)99;

            // Act & Assert
            Action act = () => sut.GetValidator(scheme: paymentScheme);

            act.Should().Throw<ArgumentException>()
               .WithMessage("Invalid payment scheme (Parameter 'scheme')");
        }
    }
}
