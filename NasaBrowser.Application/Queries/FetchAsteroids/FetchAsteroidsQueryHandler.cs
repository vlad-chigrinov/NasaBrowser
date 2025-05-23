﻿using MediatR;
using NasaBrowser.Application.Models.AsteroidJsonModel;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Application.Queries.FetchAsteroids;

public class FetchAsteroidsQueryHandler : IRequestHandler<FetchAsteroidsQuery, IEnumerable<AsteroidJsonDTO>>
{
    private readonly IDataSource<IEnumerable<AsteroidJsonDTO>> _dataSource;

    public FetchAsteroidsQueryHandler(IDataSource<IEnumerable<AsteroidJsonDTO>> dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<IEnumerable<AsteroidJsonDTO>> Handle(FetchAsteroidsQuery request, CancellationToken ct)
    {
        return await _dataSource.GetAsync(ct);
    }
}