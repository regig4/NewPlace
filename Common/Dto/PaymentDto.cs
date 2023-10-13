using System;

namespace Common.Dto
{
    public record PaymentDto(Guid UserId, decimal Amount, string Currency);
}
