using MediatR;
using Microsoft.AspNetCore.Mvc;
using NasaBrowser.Application.Queries.AvailableRecClasses;
using NasaBrowser.Application.Queries.AvailableYears;
using NasaBrowser.Application.Queries.GroupAsteroids;
using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;

namespace NasaBrowser.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AsteroidsController : ControllerBase
{
    private readonly ISender _sender;

    public AsteroidsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IEnumerable<AsteroidsGroupResponse>> Get(
        [FromQuery] AsteroidsGroupRequest request,
        CancellationToken ct)
    {
        return await _sender.Send(new GroupAsteroidsQuery { GroupRequest = request }, ct);
    }

    [HttpGet("years")]
    public async Task<AvailableYearsResponse> GetAvailableYears(CancellationToken ct)
    {
        return await _sender.Send(new AvailableYearsQuery(), ct);
    }
    
    [HttpGet("recclasses")]
    public async Task<AvailableRecClassesResponse> GetAvailableRecClasses(CancellationToken ct)
    {
        return await _sender.Send(new AvailableRecClassesQuery(), ct);
    }
}