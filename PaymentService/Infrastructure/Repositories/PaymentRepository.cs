using Microsoft.EntityFrameworkCore;
using PaymentService.ApplicationCore.Application.Repositories;
using PaymentService.Infrastructure.Factories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public async Task Add(ApplicationCore.Domain.Entities.Payment payment)
        {
            try
            {
                using var context = ContextFactory.Instance.Create();
                payment.Payer = null;
                context.Payments.Add(payment);
                await context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                await Update(payment);
            }
        }

        public async Task<ApplicationCore.Domain.Entities.Payment> Get(Guid id)
        {
            using var context = ContextFactory.Instance.Create();
            var payment = await context.Payments.Include(p => p.Payer).FirstOrDefaultAsync(p => p.Id == id);
            return payment;
        }

        public async Task Update(ApplicationCore.Domain.Entities.Payment payment)
        {
            try
            {
                using var context = ContextFactory.Instance.Create();
                payment.Payer = null;
                context.Payments.Update(payment);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
