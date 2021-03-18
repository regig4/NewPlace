using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Application.Repositories
{
    public interface IPaymentRepository
    {
        int Add(Domain.Entities.Payment payment);
    }
}
