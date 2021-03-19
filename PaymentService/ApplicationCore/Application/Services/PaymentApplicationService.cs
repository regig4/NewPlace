using Common.IntegrationEvents.Payment;
using PaymentService.ApplicationCore.Application.Repositories;
using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.Infrastructure.MessageQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Application.Services
{
    public class PaymentApplicationService : IPaymentApplicationService
    {
        private readonly IMessageQueue _messageQueue;
        private readonly IEventRepository _eventRepository;

        public PaymentApplicationService(IMessageQueue messageQueue, IEventRepository eventRepository)
        {
            _messageQueue = messageQueue;
            _eventRepository = eventRepository;
        }

        public async Task<DonationResult> Donate(Guid userId, ulong amount, string currency)
        {
            var payment = Domain.Entities.Payment.CreateDonation(userId, amount, currency);
            // todo: await _thirdPartyService.MakePayment() get id, for now id of donatationcreated is id of entity
            //payment.Id = Guid.NewGuid();
            payment.CompleteDonation(payment.Id);
            _messageQueue.Publish(new DonationSuccessfulEvent(payment.Id));
            await _eventRepository.SaveEvents(payment);
            payment.CommitAllEvents();

            return new DonationResult
            (
                payment.Id,
                userId,
                amount,
                currency
            );
        }
    }
}
