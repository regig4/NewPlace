using Common.IntegrationEvents.Payment;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PaymentService;
using PaymentService.Infrastructure.MessageQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Services
{
    public class PaymentService : Payment.PaymentBase
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IMessageQueue _messageQueue;

        public PaymentService(ILogger<PaymentService> logger, IMessageQueue messageQueue)
        {
            _logger = logger;
            _messageQueue = messageQueue;
        }

        public override Task<PaymentReply> Pay(PaymentRequest request, ServerCallContext context)
        {
            var payment = Domain.Entities.Payment.CreateBonusForCreatingAccount(new Domain.Entities.User());
            // todo: await _thirdPartyService.MakePayment() set id
            payment.FinalizeBonusForCreatingAccount();
            return Task.FromResult(new PaymentReply
            {
                Id = payment.Id.ToString(),
                Msg = "Transaction successful",
                Currency = request.Currency,
                Value = request.Value
            });
        }

        public override Task<PaymentReply> Donate(PaymentRequest request, ServerCallContext context)
        {
            var payment = Domain.Entities.Payment.CreateDonation(request.UserId, request.Value, request.Currency);
            // todo: await _thirdPartyService.MakePayment() set id
            payment.Id = Guid.NewGuid();
            _messageQueue.Publish(new DonationSuccessfulEvent(payment.Id));
            return Task.FromResult(new PaymentReply
            {
                Currency = request.Currency,
                Id = payment.Id.ToString(),
                Msg = "Donation successful",
                Value = request.Value
            });
        }
    }
}
