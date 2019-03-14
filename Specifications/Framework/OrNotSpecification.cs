namespace Specifications.Framework
{
    public class OrNotSpecification<T>: CompositeSpecification<T>
    {
        #region Private Member Variables

        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        #endregion

        #region Constructors

        public OrNotSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        #endregion
        
        #region Public Methods

        public override bool IsSatisfiedBy(T candidate)
            => _left.IsSatisfiedBy(candidate) || !_right.IsSatisfiedBy(candidate);

        #endregion
    }
}