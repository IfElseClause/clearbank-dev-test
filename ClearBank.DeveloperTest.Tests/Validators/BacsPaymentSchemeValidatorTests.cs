using AutoFixture.Xunit2;
using Xunit;
using FluentAssertions;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class BacsPaymentSchemeValidatorTests
    {
        [Theory]
        [InlineAutoData(AllowedPaymentSchemes.Bacs, true)]
        [InlineAutoData(AllowedPaymentSchemes.Chaps, false)]
        [InlineAutoData(AllowedPaymentSchemes.FasterPayments, false)]
        internal void Validate_ReturnsExpectedResult_WhenCheckingIfAccountAllowsBacsPayments(
            AllowedPaymentSchemes allowedPaymentSchemes,
            bool expectedOutcome,
            BacsPaymentSchemeValidator sut)
        {
            // Act
            bool result = sut.Validate(allowedPaymentSchemes: allowedPaymentSchemes);

            // Assert
            result.Should().Be(expectedOutcome);
        }
    }
}
