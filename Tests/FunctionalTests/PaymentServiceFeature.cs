using PaymentService.Infrastructure.MessageQueue;
using FluentAssertions;
using Moq;
using System;
using Xbehave;
using PaymentService.ApplicationCore.Application.Services;
using PaymentService.ApplicationCore.Domain.Entities;
using Common.IntegrationEvents.Payment;

namespace Tests.FunctionalTests
{
    public class PaymentServiceFeature
    {
        [Scenario]
        public void DonateTest(Mock<IMessageQueue> mock, PaymentApplicationService service, Guid id, DonationResult result)
        {
            "Given mocked message queue".x(() => mock = new Mock<IMessageQueue>());

            "And given payment service".x(() =>
                service = new PaymentApplicationService(mock.Object));

            "When donating 100 USD".x(async () => result = await service.Donate(Guid.NewGuid(), 100, "USD"));

            "Then message about donation was published".x(() => mock.Verify(q => q.Publish(It.IsAny<DonationSuccessfulEvent>())));

            "And we get the confirmation of payment"
                .x(() => result.Should().NotBeNull());

            "And we get correct amount on confirmation"
                .x(() => result.Amount.Should().Be(100));

            "And we get payment id on confirmation"
                .x(() => result.PaymentId.Should().NotBe(Guid.Empty));
        }
    }
}
