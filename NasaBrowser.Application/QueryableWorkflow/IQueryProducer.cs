namespace NasaBrowser.Application.QueryableWorkflow;

public interface IQueryProducer<TOuput>
{
    public IQueryable<TOuput> Queryable { get; }
}