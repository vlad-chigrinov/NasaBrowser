using MediatR;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableTransformations;

namespace NasaBrowser.Application.Queries.GroupAsteroids;

public class GroupAsteroidsQueryHandler : IRequestHandler<GroupAsteroidsQuery, IEnumerable<AsteroidGroupResponse>>
{
    private readonly IQueryProducer<Asteroid> _asteroidProducer;
    private readonly IQueryExecutor<AsteroidGroupResponse> _asteroidExecutor;
    
    private readonly IQueryTransformer<Asteroid, Asteroid, AsteroidFilterTransformation> _filterTransformer;
    private readonly IQueryTransformer<IGrouping<int, Asteroid>, Asteroid, AsteroidGroupTransformation> _groupTransformer;
    private readonly IQueryTransformer<AsteroidGroupResponse, IGrouping<int, Asteroid>, AsteroidAggregateTransformation> _aggregateTransformer;
    private readonly IQueryTransformer<AsteroidGroupResponse, AsteroidGroupResponse, AsteroidSortTransformation> _sortTransformer;

    public GroupAsteroidsQueryHandler(
        IQueryProducer<Asteroid> asteroidProducer,
        IQueryExecutor<AsteroidGroupResponse> asteroidExecutor,
        IQueryTransformer<Asteroid, Asteroid, AsteroidFilterTransformation> filterTransformer,
        IQueryTransformer<IGrouping<int, Asteroid>, Asteroid, AsteroidGroupTransformation> groupTransformer,
        IQueryTransformer<AsteroidGroupResponse, IGrouping<int, Asteroid>, AsteroidAggregateTransformation> aggregateTransformer,
        IQueryTransformer<AsteroidGroupResponse, AsteroidGroupResponse, AsteroidSortTransformation> sortTransformer
        )
    {
        _asteroidProducer = asteroidProducer;
        _asteroidExecutor = asteroidExecutor;
        _filterTransformer = filterTransformer;
        _groupTransformer = groupTransformer;
        _aggregateTransformer = aggregateTransformer;
        _sortTransformer = sortTransformer;
    }

    public async Task<IEnumerable<AsteroidGroupResponse>> Handle(GroupAsteroidsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _asteroidProducer.Queryable;

        var filtered =_filterTransformer.Transform(queryable, new ()
        {
            RecClass = request.GroupRequest.RecClass,
            NamePart = request.GroupRequest.NamePart,
            StartYear = request.GroupRequest.StartYear,
            EndYear = request.GroupRequest.EndYear,
        });

        var grouped = _groupTransformer.Transform(filtered, new ());
        
        var aggregated = _aggregateTransformer.Transform(grouped, new ());
        
        var sorted = _sortTransformer.Transform(aggregated, new ()
        {
            SortBy = request.GroupRequest.SortBy,
            Desc = request.GroupRequest.Desc
        });
        
        return await _asteroidExecutor.ExecuteAsync(sorted);
    }
}