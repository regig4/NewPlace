using Common.ApplicationCore.Domain.Entities;
using PaymentService.ApplicationCore.Domain.ValueObjects;

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
