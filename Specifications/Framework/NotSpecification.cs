namespace Specifications.Framework
{
    public class NotSpecification<T>: CompositeSpecification<T>
    {
        #region Private Member Variables

        private readonly ISpecification<T> _other;

        #endregion

        #region Constructors

        public NotSpecification(ISpecification<T> other)
        {
            _other = other;
        }

        #endregion
        
        #region Public Methods

        public override bool IsSatisfiedBy(T candidate)
            => _other.IsSatisfiedBy(candidate);

        #endregion

    }
}