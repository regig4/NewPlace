using System;
using System.Threading.Tasks;
using PaymentService.Infrastructure.Repositories;
using Xunit;

namespace Tests.IntegrationTests
{
    public class PaymentRepositoryTests
    {
        [Fact]
        public async Task PaymentRepositoryShouldNotThrowDbUpdateException()
        {
            PaymentRepository repo = new PaymentRepository();
            Guid guid = Guid.Parse("068FF0B4-AEE2-49B5-96E1-C30E2BF9141C");
            PaymentService.ApplicationCore.Domain.Entities.Payment p = PaymentService.ApplicationCore.Domain.Entities.Payment.CreateDonation(guid, 40, "USD");
            await repo.Add(p);
        }
    }
}
