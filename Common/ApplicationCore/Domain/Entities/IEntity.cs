using System;
using Common.ApplicationCore.Domain.Events;

namespace Common.ApplicationCore.Domain.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public void Apply(IDomainEvent domainEvent);

    }
}