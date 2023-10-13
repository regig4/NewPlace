using System;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Application.Repositories
{
    public interface IPaymentRepository
    {
        Task Add(Domain.Entities.Payment payment);
        Task Update(Domain.Entities.Payment payment);
        Task<Domain.Entities.Payment> Get(Guid id);
    }
}
