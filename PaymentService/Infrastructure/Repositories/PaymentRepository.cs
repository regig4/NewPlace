using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentService.ApplicationCore.Application.Repositories;
using PaymentService.Infrastructure.Factories;

namespace PaymentService.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public async Task Add(ApplicationCore.Domain.Entities.Payment payment)
        {
            using global::Infrastructure.PaymentDb context = ContextFactory.Instance.Create();
            context.Payments.Add(payment);
            context.Entry(payment.Payer).State = EntityState.Unchanged;
            await context.SaveChangesAsync();
        }

        public async Task<ApplicationCore.Domain.Entities.Payment> Get(Guid id)
        {
            using global::Infrastructure.PaymentDb context = ContextFactory.Instance.Create();
            ApplicationCore.Domain.Entities.Payment payment = await context.Payments.Include(p => p.Payer).FirstOrDefaultAsync(p => p.Id == id);
            return payment;
        }

        public async Task Update(ApplicationCore.Domain.Entities.Payment payment)
        {
            try
            {
                using global::Infrastructure.PaymentDb context = ContextFactory.Instance.Create();
                context.Payments.Update(payment);
                context.Entry(payment.Payer).State = EntityState.Unchanged;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
