namespace NasaBrowser.Domain.Common;

public interface IMapper<in TSource, out TDestination>
{
    public TDestination Map(TSource source);
}