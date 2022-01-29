using System;
using System.Threading.Tasks;
using Common.ApplicationCore.Domain.Entities;

namespace PaymentService.ApplicationCore.Application.Repositories
{
    public interface IRepository<out T> where T : IEntity
    {
        Task<IEntity> Get(Guid id);
        Task Add(IEntity entity);
        Task Update(IEntity entity);
    }
}