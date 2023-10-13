using System;

namespace Common.Dto
{
    public record PaymentStatusResponse(Guid PaymentId, string Status);

}
