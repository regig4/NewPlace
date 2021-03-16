using ApplicationCore.Services;
using Common.Dto;
using Common.IntegrationEvents.Payment;
using Grpc.Net.Client;
using MediatR;
using PaymentService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Models.Commands
{
    class DonateCommandHandler : IRequestHandler<DonateCommand, DonationConfirmation>
    {
        private readonly GrpcChannel _channel;
        private readonly IMessageQueue _messageQueue;

        public DonateCommandHandler(GrpcChannel channel, IMessageQueue messageQueue)
        {
            _channel = channel;
            _messageQueue = messageQueue;
        }
        
        public async Task<DonationConfirmation> Handle(DonateCommand request, CancellationToken cancellationToken)
        {
            var client = new Payment.PaymentClient(_channel);

            _ = client.DonateAsync(
                new PaymentRequest
                {
                    Value = request.Amount,
                    Currency = request.Currency,
                    UserId = request.UserId.ToString()
                });

            var donationSuccessfulEvent = await _messageQueue.WaitForEvent(new DonationSuccessfulEvent(new Guid())) as DonationSuccessfulEvent;

            DonationConfirmation reply = new DonationConfirmation
            {
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentId = donationSuccessfulEvent.Id,
                Status = DonationConfirmation.DonationStatus.Success,
                UserId = request.UserId
            };

            _channel.Dispose();

            return reply;
        }
    }
}
