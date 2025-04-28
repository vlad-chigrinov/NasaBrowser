using NasaBrowser.Application.QueryableWorkflow.QueryableTransformations;

namespace NasaBrowser.Application.QueryableWorkflow;

public interface IQueryTransformer<TTransformation, TQuery, TQueryResult>
    where TTransformation : TransformationBase<TQuery, TQueryResult>
{
    IQueryable<TQueryResult> Transform(TTransformation transformation);
}