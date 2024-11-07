using AutoFixture.Xunit2;
using Xunit;
using FluentAssertions;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class FasterPaymentSchemeValidatorTests
    {
        [Theory]
        [InlineAutoData(AllowedPaymentSchemes.FasterPayments, true)]
        [InlineAutoData(AllowedPaymentSchemes.Chaps, false)]
        [InlineAutoData(AllowedPaymentSchemes.Bacs, false)]
        internal void Validate_ReturnsExpectedResult_WhenCheckingIfAccountAllowsFasterPayments(
            AllowedPaymentSchemes allowedPaymentSchemes,
            bool expectedOutcome,
            FasterPaymentSchemeValidator sut)
        {
            // Act
            bool result = sut.Validate(allowedPaymentSchemes: allowedPaymentSchemes);

            // Assert
            result.Should().Be(expectedOutcome);
        }
    }
}
