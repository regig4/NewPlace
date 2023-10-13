using Common.ApplicationCore.Domain.ValueObjects;

namespace PaymentService.ApplicationCore.Domain.ValueObjects
{
    public record MoneyValue(ulong Amount, string Currency) : ValueObject { }
}
