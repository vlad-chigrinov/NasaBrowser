namespace NasaBrowser.Domain.Common;

public interface IQueryProducer<out TOuput>
{
    public IQueryable<TOuput> Queryable { get; }
}