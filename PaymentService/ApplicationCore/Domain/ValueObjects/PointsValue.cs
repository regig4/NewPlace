﻿using Common.ApplicationCore.Domain.ValueObjects;

namespace PaymentService.ApplicationCore.Domain.ValueObjects
{
    public record PointsValue(decimal Amount) : ValueObject
    {
        public static bool operator <(PointsValue a, PointsValue b)
        {
            return a.Amount < b.Amount;
        }

        public static bool operator >(PointsValue a, PointsValue b)
        {
            return a.Amount > b.Amount;
        }

        public static bool operator >=(PointsValue a, PointsValue b)
        {
            return a.Amount >= b.Amount;
        }

        public static bool operator <=(PointsValue a, PointsValue b)
        {
            return a.Amount <= b.Amount;
        }
    }
}
