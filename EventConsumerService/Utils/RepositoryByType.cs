using Common.Dto;
using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.Infrastructure.Repositories;
using System;

namespace EventConsumerService.Utils
{
    class RepositoryByType
    {
        private RepositoryByType() { }

        public static RepositoryByType Instance { get; } = new RepositoryByType();

        public PaymentRepository this[Type type]
        {
            get
            {
                if (type == typeof(Payment))
                    return new PaymentRepository();
                else
                    throw new InvalidOperationException();
            }
        }
    }
}
