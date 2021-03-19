using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public record PaymentDto(string UserName, ulong Amount, string Currency);
}
