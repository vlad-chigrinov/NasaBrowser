using NasaBrowser.Domain.QueryableWorkflow.QueryableTransformations;

namespace NasaBrowser.Domain.QueryableWorkflow;

public interface IQueryTransformer<TTransformation, TQuery, TQueryResult>
    where TTransformation : TransformationBase<TQuery, TQueryResult>
{
    IQueryable<TQueryResult> Transform(TTransformation transformation);
}