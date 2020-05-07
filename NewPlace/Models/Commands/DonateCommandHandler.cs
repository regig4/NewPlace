﻿using Grpc.Net.Client;
using MediatR;
using PaymentService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Models.Commands
{
    class DonateCommandHandler : IRequestHandler<DonateCommand, DonationConfirmation>
    {
        public async Task<DonationConfirmation> Handle(DonateCommand request, CancellationToken cancellationToken)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5003");
            var client = new Greeter.GreeterClient(channel);
            var response = await client.SayHelloAsync(new HelloRequest { Name = "world!" });
            return await Task.FromResult(new DonationConfirmation(1, 10, "USD", DonationConfirmation.DonationStatus.Success));
        }
    }
}
