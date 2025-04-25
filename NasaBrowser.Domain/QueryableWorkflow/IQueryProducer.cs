namespace NasaBrowser.Domain.QueryableWorkflow;

public interface IQueryProducer<TOuput>
{
    public IQueryable<TOuput> Queryable { get; }
}