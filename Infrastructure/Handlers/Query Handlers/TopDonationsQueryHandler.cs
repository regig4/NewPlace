using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Application.Queries;
using Common.Dto;
using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Models.Commands
{
    public class TopDonationsQueryHandler : IRequestHandler<TopDonationsQuery, List<PaymentDto>>
    {
        public TopDonationsQueryHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<List<PaymentDto>> Handle(TopDonationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                HttpClientHandler? httpHandler = new HttpClientHandler
                {
                    // Return `true` to allow certificates that are untrusted/invalid
                    ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                GrpcChannel? channel = GrpcChannel.ForAddress(Configuration.GetServiceUri("catalogservice") ?? new Uri("https://localhost:5001"),
                    new GrpcChannelOptions { HttpClient = new HttpClient(httpHandler) });
                CatalogService.Catalog.CatalogClient? client = new CatalogService.Catalog.CatalogClient(channel);
                CatalogService.TopDonationResult? result = await client.TopDonationsAsync(new CatalogService.TopDonationsRequest { Count = request.Count });
                channel.Dispose();
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
