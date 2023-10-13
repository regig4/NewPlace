using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.ApplicationCore.Domain.Entities;
using Common.ApplicationCore.Domain.Events;

namespace PaymentService.ApplicationCore.Application.Repositories
{
    public interface IEventRepository
    {
        Task<List<IDomainEvent>> GetEvents(Guid entityId);
        Task SaveEvents(Entity entity);
    }
}
