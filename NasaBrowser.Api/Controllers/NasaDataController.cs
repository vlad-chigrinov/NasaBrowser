using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NasaBrowser.Api.DataAccess;

namespace NasaBrowser.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NasaDataController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public NasaDataController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Meteorite>> Get(CancellationToken ct)
    {
        return await _dbContext.Meteorites.ToListAsync(ct);
    }
}