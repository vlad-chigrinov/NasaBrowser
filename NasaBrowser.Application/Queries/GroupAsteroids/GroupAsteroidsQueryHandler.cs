using MediatR;
using NasaBrowser.Application.AsteroidTransformations;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableWorkflow;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Application.Queries.GroupAsteroids;

public class GroupAsteroidsQueryHandler : IRequestHandler<GroupAsteroidsQuery, IEnumerable<AsteroidsGroupResponse>>
{
    private readonly IQueryProducer<Asteroid> _asteroidProducer;

    private readonly IQueryTransformer<AsteroidSequentialTransformation, Asteroid, AsteroidsGroupResponse>
        _asteroidsTransformer;

    private readonly IQueryExecutor<AsteroidsGroupResponse> _asteroidExecutor;

    private readonly IAsteroidsGroupCacheRepository _cacheRepository;

    public GroupAsteroidsQueryHandler(IQueryProducer<Asteroid> asteroidProducer,
        IQueryTransformer<AsteroidSequentialTransformation, Asteroid, AsteroidsGroupResponse> asteroidsTransformer,
        IQueryExecutor<AsteroidsGroupResponse> asteroidExecutor,
        IAsteroidsGroupCacheRepository cacheRepository)
    {
        _asteroidProducer = asteroidProducer;
        _asteroidExecutor = asteroidExecutor;
        _asteroidsTransformer = asteroidsTransformer;
        _cacheRepository = cacheRepository;
    }

    public async Task<IEnumerable<AsteroidsGroupResponse>> Handle(GroupAsteroidsQuery request, CancellationToken ct)
    {
        return await _cacheRepository.GetOrCreateAsync(request.GroupRequest, async token =>
        {
            var queryable = _asteroidsTransformer.Transform(new(_asteroidProducer.Queryable)
                { Request = request.GroupRequest });
            return await _asteroidExecutor.ExecuteAsync(queryable, ct);
        }, ct);
    }
}