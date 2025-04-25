using MediatR;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Application.Queries.AvailableRecClasses;

public class AvailableRecClassesQueryHandler : IRequestHandler<AvailableRecClassesQuery, AvailableRecClassesResponse>
{
    private readonly IRecClassecDataSource _dataSource;

    public AvailableRecClassesQueryHandler(IRecClassecDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<AvailableRecClassesResponse> Handle(AvailableRecClassesQuery request, CancellationToken ct)
    {
        return await _dataSource.GetAsync(ct);
    }
}