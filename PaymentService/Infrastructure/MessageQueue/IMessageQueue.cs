using Common.IntegrationEvents.Payment;

namespace PaymentService.Infrastructure.MessageQueue
{
    public interface IMessageQueue
    {
        void Publish(DonationSuccessfulEvent @event);
    }
}
