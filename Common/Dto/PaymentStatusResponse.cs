﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public record PaymentStatusResponse(Guid PaymentId, string Status);

}