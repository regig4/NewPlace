using Common.IntegrationEvents.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.MessageQueue
{
    public interface IMessageQueue
    {
        void Publish(DonationSuccessfulEvent @event);
    }
}
