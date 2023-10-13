using System;
using Common.IntegrationEvents.Payment;
using FluentAssertions;
using Moq;
using PaymentService.ApplicationCore.Application.Repositories;
using PaymentService.ApplicationCore.Application.Services;
using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.Infrastructure.MessageQueue;
using Xbehave;

namespace Tests.FunctionalTests
{
    public class PaymentServiceFeature
    {
        [Scenario]
        public void DonateTest(Mock<IMessageQueue> msgQueueMock, Mock<IEventRepository> eventRepositoryMock, PaymentApplicationService service, Guid id, DonationResult result)
        {
            "Given mocked message queue".x(() => msgQueueMock = new Mock<IMessageQueue>());
            "And mocked event queue".x(() => eventRepositoryMock = new Mock<IEventRepository>());
            "And payment service".x(() =>
                service = new PaymentApplicationService(msgQueueMock.Object, eventRepositoryMock.Object));

            "When donating 100 USD".x(async () => result = await service.Donate(Guid.NewGuid(), 100, "USD"));

            "Then message about donation was published".x(() => msgQueueMock.Verify(q => q.Publish(It.IsAny<DonationSuccessfulEvent>())));

            "And we get the confirmation of payment"
                .x(() => result.Should().NotBeNull());

            "And we get correct amount on confirmation"
                .x(() => result.Amount.Should().Be(100));

            "And we get payment id on confirmation"
                .x(() => result.PaymentId.Should().NotBe(Guid.Empty));
        }
    }
}
