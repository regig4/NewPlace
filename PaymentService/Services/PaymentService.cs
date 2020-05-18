using Grpc.Core;
using Microsoft.Extensions.Logging;
using PaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Services
{
    public class PaymentService : Payment.PaymentBase
    {
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;
        }

        public override Task<PaymentReply> Pay(PaymentRequest request, ServerCallContext context)
        {
            // todo
            return Task.FromResult(new PaymentReply
            {
                Msg = "Transaction successful",
                Currency = request.Currency,
                Value = request.Value
            });
        }
    }
}
