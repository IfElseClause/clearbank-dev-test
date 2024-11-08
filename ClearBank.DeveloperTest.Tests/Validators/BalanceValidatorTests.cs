using AutoFixture.Xunit2;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class BalanceValidatorTests
    {
        [Theory]
        [InlineAutoData(100, 50, true)]
        [InlineAutoData(50, 50, true)]
        [InlineAutoData(0, 0, true)]
        [InlineAutoData(0, 50, false)]
        [InlineAutoData(-100, 50, false)]
        [InlineAutoData(100, 150, false)]
        internal void HasSufficientBalance(
            decimal balance,
            decimal paymentAmount,
            bool expectedOutcome,
            BalanceValidator sut)
        {
            // Act
            bool result = sut.HasSufficientBalance(balance: balance, amount: paymentAmount);

            // Assert
            result.Should().Be(expectedOutcome);
        }
    }
}
