namespace NasaBrowser.Domain.Common;

public interface IQueryTransformer<out TOutput, in TInput, in TOptions>
{
    public IQueryable<TOutput> Transform(IQueryable<TInput> queryable, TOptions options);
}