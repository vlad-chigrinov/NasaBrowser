using MediatR;
using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;

namespace NasaBrowser.Application.Queries.GroupAsteroids;

public class GroupAsteroidsQuery : IRequest<IEnumerable<AsteroidGroupResponse>>
{
    public required AsteroidsGroupRequest GroupRequest { get; set; }
}