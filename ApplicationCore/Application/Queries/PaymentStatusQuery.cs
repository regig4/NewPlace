using System;
using Common.Dto;

namespace ApplicationCore.Application.Queries
{
    public record PaymentStatusQuery(Guid PaymentId) : IQuery<PaymentStatusResponse>;
}
