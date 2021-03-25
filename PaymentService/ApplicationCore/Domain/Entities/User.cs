using PaymentService.ApplicationCore.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.ApplicationCore.Domain.Entities
{
    public class User : Entity
    {
        public PointsValue PointsValue { get; private set; }

        public bool HasEnoughPointsForPromotion(PointsValue recommendationPrice)
        {
            return PointsValue >= recommendationPrice;
        }
    }
}
