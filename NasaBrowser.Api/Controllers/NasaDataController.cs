using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NasaBrowser.Infrastructure.Database;

namespace NasaBrowser.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NasaDataController : ControllerBase
{
    private readonly AsteroidsDbContext _dbContext;

    public NasaDataController(AsteroidsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Meteorite>> Get(CancellationToken ct)
    {
        return await _dbContext.Meteorites.ToListAsync(ct);
    }
}