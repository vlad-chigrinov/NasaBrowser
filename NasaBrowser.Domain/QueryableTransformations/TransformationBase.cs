namespace NasaBrowser.Domain.QueryableTransformations
{
    public abstract class TransformationBase<TQuery, TResult>
    {
        public IQueryable<TQuery> Queryable { get; private set; }

        public TransformationBase(IQueryable<TQuery> queryable)
        {
            Queryable = queryable;
        }
    }
}
