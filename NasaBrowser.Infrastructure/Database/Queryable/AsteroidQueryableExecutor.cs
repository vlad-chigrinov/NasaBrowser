using Microsoft.EntityFrameworkCore;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.QueryableWorkflow;

namespace NasaBrowser.Infrastructure.Database.Queryable;

public class AsteroidQueryableExecutor: IQueryExecutor<AsteroidsGroupResponse>
{
    public async Task<IEnumerable<AsteroidsGroupResponse>> ExecuteAsync(IQueryable<AsteroidsGroupResponse> queryable, CancellationToken ct = default)
    {
        return await queryable.ToListAsync(ct);
    }
}