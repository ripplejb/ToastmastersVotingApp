using System;

namespace Specifications.Framework
{
    public class AndSpecification<T>: CompositeSpecification<T>
    {
        #region Private Member Variables

        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        #endregion

        #region Constructors

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        #endregion
        
        #region Public Methods

        public override bool IsSatisfiedBy(T candidate)
            => _left.IsSatisfiedBy(candidate) && _right.IsSatisfiedBy(candidate);

        #endregion
    }
}