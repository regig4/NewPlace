using System;
using PaymentService.Domain.Events;

namespace PaymentService.ApplicationCore.Domain.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public void Apply(IDomainEvent domainEvent);

    }
}