using System;
using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.Infrastructure.Repositories;

namespace EventConsumerService.Utils
{
    internal class RepositoryByType
    {
        private RepositoryByType() { }

        public static RepositoryByType Instance { get; } = new RepositoryByType();

        public object this[Type type] => type switch
        {
            { Name: nameof(Payment) } => new PaymentRepository(),
            _ => null
        };
    }
}
