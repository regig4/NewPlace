using Common.ApplicationCore.Domain.Entities;
using Common.ApplicationCore.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Application.Repositories
{
    public interface IEventRepository
    {
        Task<List<IDomainEvent>> GetEvents(Guid entityId);
        Task SaveEvents(Entity entity);
    }
}
