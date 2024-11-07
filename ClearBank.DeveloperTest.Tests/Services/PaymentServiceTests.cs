using AutoFixture;
using AutoFixture.Xunit2;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        [Theory]
        [AutoData]
        public void MakePayment_WithValidRequst_ReturnsSuccessful(
            PaymentService sut,
            IFixture fixture)
        {
            // Arrange
            Types.MakePaymentRequest request = fixture.Create<Types.MakePaymentRequest>();

            // Act
            var result = sut.MakePayment(request: request);
            
            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
        }

        [Theory]
        [AutoData]
        public void MakePayment_HasMatchingAccountNumber_ReturnsSuccessTrue(
            [Frozen] IAccountDataStore accountDataStore,
            PaymentService sut,
            IFixture fixture)
        {
            // Arrange
            string accountNumber = fixture.Create<string>();

            Account account = fixture
                .Build<Account>()
                .With(x => x.AccountNumber, accountNumber)
                .Create();

            MakePaymentRequest request = fixture
                .Build<MakePaymentRequest>()
                .With(x=> x.DebtorAccountNumber, accountNumber)
                .Create();

            accountDataStore
                .GetAccount(accountNumber)
                .Returns(account);

            // Act
            var result = sut.MakePayment(request: request);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
        }

        [Theory]
        [AutoData]
        public void MakePayment_NoMatchingAccountNumber_ReturnsSuccessFalse(
            [Frozen] IAccountDataStore accountDataStore,
            PaymentService sut,
            IFixture fixture)
        {
            // Arrange
            string accountNumber = fixture.Create<string>();

            Account account = fixture
                .Build<Account>()
                .With(x => x.AccountNumber, accountNumber)
                .Create();

            MakePaymentRequest request = fixture
                .Build<MakePaymentRequest>()
                .With(x => x.DebtorAccountNumber, accountNumber)
                .Create();

            accountDataStore
                .GetAccount(accountNumber)
                .Returns(null);

            // Act
            var result = sut.MakePayment(request: request);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
        }
    }
}
