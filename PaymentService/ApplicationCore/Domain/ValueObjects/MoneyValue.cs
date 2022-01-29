using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ApplicationCore.Domain.ValueObjects;

namespace PaymentService.ApplicationCore.Domain.ValueObjects
{
    public record MoneyValue(ulong Amount, string Currency) : ValueObject { }
}
