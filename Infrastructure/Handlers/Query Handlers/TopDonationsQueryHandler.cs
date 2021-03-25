using ApplicationCore.Application.Commands;
using ApplicationCore.Application.Queries;
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
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Models.Commands
{
    public class TopDonationsQueryHandler : IRequestHandler<TopDonationsQuery, List<PaymentDto>>
    {
        public async Task<List<PaymentDto>> Handle(TopDonationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var httpHandler = new HttpClientHandler();
                // Return `true` to allow certificates that are untrusted/invalid
                httpHandler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                var channel = GrpcChannel.ForAddress("https://localhost:5003",
                    new GrpcChannelOptions { HttpClient = new HttpClient(httpHandler) });
                var client = new CatalogService.Catalog.CatalogClient(channel);
                var result = await client.TopDonationsAsync(new CatalogService.TopDonationsQuery { Count = request.Count });
                return result.Payments.Select(p => new PaymentDto
                (
                    Guid.Parse(p.UserId),
                    decimal.Parse(p.Amount),
                    p.Currency
                )).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
