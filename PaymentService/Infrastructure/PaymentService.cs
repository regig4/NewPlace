using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PaymentService.ApplicationCore.Application.Services;

namespace PaymentService.Services
{
    public class PaymentService : Payment.PaymentBase
    {
        private readonly IPaymentApplicationService _applicationService;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IPaymentApplicationService applicationService, ILogger<PaymentService> logger)
        {
            _applicationService = applicationService;
            _logger = logger;
        }

        public override Task<PaymentReply> Pay(PaymentRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        public override async Task<PaymentReply> Donate(PaymentRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.UserId, out Guid parsedUserId))
            {
                throw new ArgumentException("Invalid user id", nameof(request.UserId));
            }

            ApplicationCore.Domain.Entities.DonationResult result = await _applicationService.Donate(parsedUserId, request.Value, request.Currency);

            return new PaymentReply
            {
                Currency = request.Currency,
                Id = result.PaymentId.ToString(),
                Msg = "Donation successful",
                Value = request.Value
            };
        }
    }
}
