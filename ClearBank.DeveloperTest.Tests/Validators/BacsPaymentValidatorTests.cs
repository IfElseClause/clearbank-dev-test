﻿using AutoFixture.Xunit2;
using AutoFixture;
using Xunit;
using FluentAssertions;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class BacsPaymentValidatorTests
    {
        [Theory]
        [AutoData]
        public void Validate_AccountHasAllowsBacsPayments_ReturnsTrue(
            BacsPaymentValidator sut,
            IFixture fixture)
        {
            // Arrange
            Account account = fixture
                .Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.Bacs)
                .Create();
            MakePaymentRequest paymentRequest = fixture.Create<MakePaymentRequest>();

            // Act
            bool result = sut.Validate(account: account, paymentRequest: paymentRequest);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [AutoData]
        public void Validate_AccountDoesNotAllowBacsPayments_ReturnsFalse(
            BacsPaymentValidator sut,
            IFixture fixture)
        {
            // Arrange
            Account account = fixture
                .Build<Account>()
                .With(x => x.AllowedPaymentSchemes, AllowedPaymentSchemes.FasterPayments)
                .Create();

            MakePaymentRequest paymentRequest = fixture.Create<MakePaymentRequest>();

            // Act
            bool result = sut.Validate(account: account, paymentRequest: paymentRequest);

            // Assert
            result.Should().BeTrue();
        }
    }
}