using System;

namespace Specifications.Framework
{
    public abstract class CompositeSpecification<T>: ISpecification<T>
    {
        #region Public Methods

        public abstract bool IsSatisfiedBy(T candidate);

        public ISpecification<T> And(ISpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public ISpecification<T> AndNot(ISpecification<T> other)
        {
            return new AndNotSpecification<T>(this, other);
        }

        public ISpecification<T> Or(ISpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public ISpecification<T> OrNot(ISpecification<T> other)
        {
            return new OrNotSpecification<T>(this, other);
        }

        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        #endregion
    }
}