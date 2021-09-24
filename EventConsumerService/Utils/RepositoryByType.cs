using Common.Dto;
using PaymentService.ApplicationCore.Application.Repositories;
using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.Infrastructure.Repositories;
using System;

namespace EventConsumerService.Utils
{
    class RepositoryByType
    {
        private RepositoryByType() { }

        public static RepositoryByType Instance { get; } = new RepositoryByType();

        public object this[Type type]
        {
            get
            {
                return type switch 
                {
                    { Name: nameof(Payment) } => new PaymentRepository(),
                    _ => null
                };
            }
        }
    }
}
