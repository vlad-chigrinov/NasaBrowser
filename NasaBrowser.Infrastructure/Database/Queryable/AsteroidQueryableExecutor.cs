using Microsoft.EntityFrameworkCore;
using NasaBrowser.Application.QueryableWorkflow;
using NasaBrowser.Domain.Contracts.Responses;

namespace NasaBrowser.Infrastructure.Database.Queryable;

public class AsteroidQueryableExecutor: IQueryExecutor<AsteroidsGroupResponse>
{
    public async Task<IEnumerable<AsteroidsGroupResponse>> ExecuteAsync(IQueryable<AsteroidsGroupResponse> queryable, CancellationToken ct = default)
    {
        return await queryable.AsNoTracking().ToListAsync(ct);
    }
}