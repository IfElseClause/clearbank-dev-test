using AutoFixture;
using AutoFixture.Xunit2;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly IPaymentSchemeValidatorFactory _validatorFactory;
        private readonly PaymentService _sut;

        public PaymentServiceTests()
        {
            _accountDataStore = Substitute.For<IAccountDataStore>();
            _validatorFactory = Substitute.For<IPaymentSchemeValidatorFactory>();
            _sut = new PaymentService(_accountDataStore, _validatorFactory);
        }

        [Theory, AutoData]
        public void MakePayment_WithValidRequest_ReturnsSuccessful(
            string accountNumber,
            IFixture fixture)
        {
            // Arrange
            var amount = fixture.Create<decimal>() % 1000 + 1;
            var balance = fixture.Create<decimal>() % 1000 + 1;

            var request = fixture.Build<MakePaymentRequest>()
                .With(x => x.Amount, amount)
                .With(x => x.DebtorAccountNumber, accountNumber)
                .With(x => x.PaymentScheme, PaymentScheme.FasterPayments)
                .Create();

            var account = fixture.Build<Account>()
                .With(x => x.Balance, balance)
                .With(x => x.AccountNumber, accountNumber)
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.FasterPayments)
                .Create();

            var validator = Substitute.For<IPaymentSchemeValidator>();
            validator
                .Validate(account.AllowedPaymentSchemes)
                .Returns(true);

            _accountDataStore
                .GetAccount(request.DebtorAccountNumber)
                .Returns(account);

            _validatorFactory
                .GetValidator(request.PaymentScheme)
                .Returns(validator);

            // Act
            var result = _sut.MakePayment(request);

            // Assert
            result.Success.Should().BeTrue();
            account.Balance.Should().Be(balance - amount);
            _accountDataStore.Received(1).UpdateAccount(account);
        }

        [Theory, AutoData]
        public void MakePayment_WithNullAccount_ReturnsUnsuccessful(
            string accountNumber,
            IFixture fixture)
        {
            // Arrange
            var amount = fixture.Create<decimal>() % 1000 + 1;
            var balance = fixture.Create<decimal>() % 1000 + 1;

            var request = fixture.Build<MakePaymentRequest>()
                .With(x => x.Amount, amount)
                .With(x => x.DebtorAccountNumber, accountNumber)
                .With(x => x.PaymentScheme, PaymentScheme.FasterPayments)
                .Create();

            _accountDataStore.GetAccount(request.DebtorAccountNumber).Returns((Account)null);

            // Act
            var result = _sut.MakePayment(request);

            // Assert
            result.Success.Should().BeFalse();
            _accountDataStore.DidNotReceive().UpdateAccount(Arg.Any<Account>());
        }

        [Theory, AutoData]
        public void MakePayment_WithUnsupportedPaymentScheme_ReturnsUnsuccessful(
            string accountNumber,
            IFixture fixture)
        {
            // Arrange
            var amount = fixture.Create<decimal>() % 1000 + 1;
            var balance = fixture.Create<decimal>() % 1000 + 1;

            var request = fixture.Build<MakePaymentRequest>()
                .With(x => x.Amount, amount)
                .With(x => x.DebtorAccountNumber, accountNumber)
                .With(x => x.PaymentScheme, PaymentScheme.FasterPayments)
                .Create();

            var account = fixture.Build<Account>()
                .With(x => x.Balance, balance)
                .With(x => x.AccountNumber, accountNumber)
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Chaps)
                .Create();

            var validator = Substitute.For<IPaymentSchemeValidator>();
            validator
                .Validate(account.AllowedPaymentSchemes)
                .Returns(false);

            _accountDataStore
                .GetAccount(request.DebtorAccountNumber)
                .Returns(account);

            _validatorFactory
                .GetValidator(request.PaymentScheme)
                .Returns(validator);

            // Act
            var result = _sut.MakePayment(request);

            // Assert
            result.Success.Should().BeFalse();
            _accountDataStore.DidNotReceive().UpdateAccount(Arg.Any<Account>());
        }

        [Theory, AutoData]
        public void MakePayment_WithSufficientBalance_DeductsAccountBalance(
            string accountNumber,
            IFixture fixture)
        {
            // Arrange
            var amount = fixture.Create<decimal>() % 1000 + 1;
            var balance = fixture.Create<decimal>() % 1000 + 1;

            var request = fixture.Build<MakePaymentRequest>()
                .With(x => x.Amount, amount)
                .With(x => x.DebtorAccountNumber, accountNumber)
                .With(x => x.PaymentScheme, PaymentScheme.FasterPayments)
                .Create();

            var account = fixture.Build<Account>()
                .With(x => x.Balance, balance)
                .With(x => x.AccountNumber, accountNumber)
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.FasterPayments)
                .Create();

            var validator = Substitute.For<IPaymentSchemeValidator>();
            validator
                .Validate(account.AllowedPaymentSchemes)
                .Returns(true);

            _accountDataStore
                .GetAccount(request.DebtorAccountNumber)
                .Returns(account);

            _validatorFactory
                .GetValidator(request.PaymentScheme)
                .Returns(validator);

            // Act
            var result = _sut.MakePayment(request);

            // Assert
            result.Success.Should().BeTrue();
            account.Balance.Should().Be(balance - amount);
        }

        [Theory, AutoData]
        public void MakePayment_WithSuccessfulPayment_CallsUpdateAccount(
            string accountNumber,
            IFixture fixture)
        {
            // Arrange
            var amount = fixture.Create<decimal>() % 1000 + 1;
            var balance = fixture.Create<decimal>() % 1000 + 1;

            var request = fixture.Build<MakePaymentRequest>()
                .With(x => x.Amount, amount)
                .With(x => x.DebtorAccountNumber, accountNumber)
                .With(x => x.PaymentScheme, PaymentScheme.FasterPayments)
                .Create();

            var account = fixture.Build<Account>()
                .With(x => x.Balance, balance)
                .With(x => x.AccountNumber, accountNumber)
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.FasterPayments)
                .Create();

            var validator = Substitute.For<IPaymentSchemeValidator>();
            validator
                .Validate(account.AllowedPaymentSchemes)
                .Returns(true);

            _accountDataStore
                .GetAccount(request.DebtorAccountNumber)
                .Returns(account);

            _validatorFactory
                .GetValidator(request.PaymentScheme)
                .Returns(validator);

            // Act
            var result = _sut.MakePayment(request);

            // Assert
            _accountDataStore.Received(1).UpdateAccount(account);
        }

        [Theory, AutoData]
        public void MakePayment_WithExceptionInTransactionScope_RollsBackTransaction(
                string accountNumber,
                IFixture fixture)
        {
            // Arrange
            decimal initialBalance = 200m;
            decimal amount = 100m;

            var request = fixture.Build<MakePaymentRequest>()
                .With(x => x.Amount, amount)
                .With(x => x.DebtorAccountNumber, accountNumber)
                .With(x => x.PaymentScheme, PaymentScheme.FasterPayments)
                .Create();

            var account = fixture.Build<Account>()
                .With(x => x.Balance, initialBalance)
                .With(x => x.AccountNumber, accountNumber)
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.FasterPayments)
                .Create();

            var validator = Substitute.For<IPaymentSchemeValidator>();
            validator
                .Validate(account.AllowedPaymentSchemes)
                .Returns(true);

            _accountDataStore
                .GetAccount(request.DebtorAccountNumber)
                .Returns(account);

            _validatorFactory
                .GetValidator(request.PaymentScheme)
                .Returns(validator);

            _accountDataStore
                .When(x => x.UpdateAccount(account))
                .Do(_ => throw new Exception("Simulated failure during transaction"));

            // Act
            var result = _sut.MakePayment(request);

            // Assert
            result.Success.Should().BeFalse();
            account.Balance.Should().Be(initialBalance, "transaction should have rolled back due to exception");
        }
    }
}
