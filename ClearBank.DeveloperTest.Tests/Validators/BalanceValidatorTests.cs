using AutoFixture;
using AutoFixture.Xunit2;
using ClearBank.DeveloperTest.Types;
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
        internal void Validate_ReturnsExpectedResult_WhenCheckingIfAccountAllowsBacsPayments(
            decimal balance,
            decimal paymentAmount,
            bool expectedOutcome,
            BalanceValidator sut,
            IFixture fixture)
        {
            // Arrange
            var account = fixture.Build<Account>()
                .With(x => x.Balance, balance)
                .Create();

            // Act
            bool result = sut.HasSufficientBalance(account: account, amount: paymentAmount);

            // Assert
            result.Should().Be(expectedOutcome);
        }
    }
}
