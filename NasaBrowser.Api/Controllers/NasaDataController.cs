using MediatR;
using Microsoft.AspNetCore.Mvc;
using NasaBrowser.Application.Queries.GroupAsteroids;
using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;

namespace NasaBrowser.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NasaDataController : ControllerBase
{
    private readonly ISender _sender;

    public NasaDataController(ISender sender)
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
}