using MediatR;
using NasaBrowser.Application.Services;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Application.Queries.GroupAsteroids;

public class GroupAsteroidsQueryHandler : IRequestHandler<GroupAsteroidsQuery, IEnumerable<AsteroidGroupResponse>>
{
    private readonly IQueryProducer<Asteroid> _asteroidProducer;

    private readonly IQueryTransformer<AsteroidSequentialTransformation, Asteroid, AsteroidGroupResponse>
        _asteroidsTransformer;

    private readonly IQueryExecutor<AsteroidGroupResponse> _asteroidExecutor;

    public GroupAsteroidsQueryHandler(IQueryProducer<Asteroid> asteroidProducer,
        IQueryTransformer<AsteroidSequentialTransformation, Asteroid, AsteroidGroupResponse> asteroidsTransformer,
        IQueryExecutor<AsteroidGroupResponse> asteroidExecutor)
    {
        _asteroidProducer = asteroidProducer;
        _asteroidExecutor = asteroidExecutor;
        _asteroidsTransformer = asteroidsTransformer;
    }

    public async Task<IEnumerable<AsteroidGroupResponse>> Handle(GroupAsteroidsQuery request,
        CancellationToken cancellationToken)
    {
        var queryable = _asteroidsTransformer.Transform(new (_asteroidProducer.Queryable) {Request = request.GroupRequest});
        return await _asteroidExecutor.ExecuteAsync(queryable);
    }
}