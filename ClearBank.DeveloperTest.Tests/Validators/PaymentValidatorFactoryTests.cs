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
    public class PaymentValidatorFactoryTests
    {

        private readonly IFixture _fixture;

        public PaymentValidatorFactoryTests()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _fixture.Register<IServiceProvider>(() =>
            {
                var serviceProvider = Substitute.For<IServiceProvider>();
                serviceProvider.GetService(typeof(BacsPaymentValidator)).Returns(new BacsPaymentValidator());
                serviceProvider.GetService(typeof(FasterPaymentsValidator)).Returns(new FasterPaymentsValidator());
                serviceProvider.GetService(typeof(ChapsPaymentValidator)).Returns(new ChapsPaymentValidator());
                return serviceProvider;
            });
        }

        [Fact]
        internal void GetValidator_WithBacsPaymentScheme_ReturnsBacsPaymentValidator()
        {
            // Arrange
            var sut = _fixture.Create<PaymentValidatorFactory>();

            var paymentScheme = PaymentScheme.Bacs;

            // Act
            var result = sut.GetValidator(scheme: paymentScheme);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BacsPaymentValidator>();
        }

        [Fact]
        internal void GetValidator_WithBacsPaymentScheme_ReturnsFasterPaymentsPaymentValidator()
        {
            // Arrange
            var sut = _fixture.Create<PaymentValidatorFactory>();

            var paymentScheme = PaymentScheme.FasterPayments;

            // Act
            var result = sut.GetValidator(scheme: paymentScheme);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FasterPaymentsValidator>();
        }

        [Fact]
        internal void GetValidator_WithChapsPaymentScheme_ReturnsChapsPaymentValidator()
        {
            // Arrange
            var sut = _fixture.Create<PaymentValidatorFactory>();

            var paymentScheme = PaymentScheme.Chaps;

            // Act
            var result = sut.GetValidator(scheme: paymentScheme);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ChapsPaymentValidator>();
        }

        [Fact]
        internal void GetValidator_WithInvalidPaymentScheme_ThrowsException()
        {
            // Arrange
            var sut = _fixture.Create<PaymentValidatorFactory>();

            PaymentScheme paymentScheme = (PaymentScheme)99;

            // Act & Assert
            Action act = () => sut.GetValidator(scheme: paymentScheme);

            act.Should().Throw<ArgumentException>()
               .WithMessage("Invalid payment scheme (Parameter 'scheme')");
        }
    }
}
