using AutoFixture;
using AutoFixture.Xunit2;
using ClearBank.DeveloperTest.Services;
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
    }
}
