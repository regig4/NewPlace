using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Domain.Enums
{
    public enum TransactionType
    {
        Donation,
        CreatedAccountBonus,
        BuyingPoints,
        PromotionPayment
    }
}
