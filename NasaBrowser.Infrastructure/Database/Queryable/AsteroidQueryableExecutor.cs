using Microsoft.EntityFrameworkCore;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.QueryableWorkflow;

namespace NasaBrowser.Infrastructure.Database.Queryable;

public class AsteroidQueryableExecutor: IQueryExecutor<AsteroidGroupResponse>
{
    public async Task<IEnumerable<AsteroidGroupResponse>> ExecuteAsync(IQueryable<AsteroidGroupResponse> queryable, CancellationToken ct = default)
    {
        return await queryable.ToListAsync(ct);
    }
}