using Infrastructure;
using PaymentService.ApplicationCore.Application.Repositories;
using PaymentService.Infrastructure.Factories;
using PaymentService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbehave;
using Xunit;

namespace Tests.IntegrationTests
{
    public class PaymentRepositoryTests
    {
        [Fact]
        public async Task PaymentRepositoryShouldNotThrowDbUpdateException()
        {
            var repo = new PaymentRepository();
            var guid = Guid.Parse("068FF0B4-AEE2-49B5-96E1-C30E2BF9141C");
            var p = PaymentService.ApplicationCore.Domain.Entities.Payment.CreateDonation(guid, 40, "USD");
            await repo.Add(p);
        }
    }
}
