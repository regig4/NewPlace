using Common.IntegrationEvents.Payment;
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
        private readonly IPaymentRepository _repository;

        public PaymentApplicationService(IMessageQueue messageQueue, IPaymentRepository repository)
        {
            _messageQueue = messageQueue;
            this._repository = repository;
        }

        public async Task<DonationResult> Donate(Guid userId, ulong amount, string currency)
        {
            var payment = Domain.Entities.Payment.CreateDonation(userId, amount, currency);
            // todo: await _thirdPartyService.MakePayment() set id
            payment.Id = Guid.NewGuid();
            payment.CompleteDonation();
            _messageQueue.Publish(new DonationSuccessfulEvent(payment.Id));
            _repository.Add(payment);
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
