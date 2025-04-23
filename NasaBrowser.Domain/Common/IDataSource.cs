namespace NasaBrowser.Domain.Common;

public interface IDataSource<out T>
{
    public T Get();
}