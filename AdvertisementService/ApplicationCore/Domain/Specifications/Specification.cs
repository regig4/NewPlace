using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }
    }
}
