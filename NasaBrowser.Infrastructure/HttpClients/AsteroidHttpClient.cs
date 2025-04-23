using NasaBrowser.Domain.Common;
using NasaBrowser.Infrastructure.InnerModels.AsteroidJsonModel;

namespace NasaBrowser.Infrastructure.HttpClients;

public class AsteroidHttpClient : IDataSource<IEnumerable<AsteroidJsonDTO>>
{
    public IEnumerable<AsteroidJsonDTO> Get()
    {
        throw new NotImplementedException();
    }
}