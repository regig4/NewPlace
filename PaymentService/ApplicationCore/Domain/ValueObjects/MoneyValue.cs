using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Domain.ValueObjects
{
    public record MoneyValue(ulong Amount, string Currency) : ValueObject { }
}
