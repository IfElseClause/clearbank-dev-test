using AutoFixture.Xunit2;
using Xunit;
using FluentAssertions;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class ChapsPaymentSchemeValidatorTests
    {
        [Theory]
        [InlineAutoData(AllowedPaymentSchemes.Chaps, true)]
        [InlineAutoData(AllowedPaymentSchemes.FasterPayments, false)]
        [InlineAutoData(AllowedPaymentSchemes.Bacs, false)]
        internal void Validate_ReturnsExpectedResult_WhenCheckingIfAccountAllowsChapsPayments(
            AllowedPaymentSchemes allowedPaymentSchemes,
            bool expectedOutcome,
            ChapsPaymentSchemeValidator sut)
        {
            // Act
            bool result = sut.Validate(allowedPaymentSchemes: allowedPaymentSchemes);

            // Assert
            result.Should().Be(expectedOutcome);
        }
    }
}
