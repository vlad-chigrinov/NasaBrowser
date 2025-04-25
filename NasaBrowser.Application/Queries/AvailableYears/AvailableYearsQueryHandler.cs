using MediatR;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Application.Queries.AvailableYears;

public class AvailableYearsQueryHandler : IRequestHandler<AvailableYearsQuery, AvailableYearsResponse>
{
    private readonly IYearsDataSource _dataSource;

    public AvailableYearsQueryHandler(IYearsDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<AvailableYearsResponse> Handle(AvailableYearsQuery request, CancellationToken ct)
    {
        return await _dataSource.GetAsync(ct);
    }
}