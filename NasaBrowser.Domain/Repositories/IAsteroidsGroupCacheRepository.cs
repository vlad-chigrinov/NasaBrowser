using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;

namespace NasaBrowser.Domain.Repositories;

public interface IAsteroidsGroupCacheRepository : ICacheRepository<AsteroidsGroupRequest, IEnumerable<AsteroidsGroupResponse>>
{
    
}