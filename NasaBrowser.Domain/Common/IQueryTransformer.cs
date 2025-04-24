using NasaBrowser.Domain.QueryableTransformations;

namespace NasaBrowser.Domain.Common;

public interface IQueryTransformer<TTransformation, TQuery, TQueryResult>
    where TTransformation : TransformationBase<TQuery, TQueryResult>
{
    IQueryable<TQueryResult> Transform(TTransformation transformation);
}