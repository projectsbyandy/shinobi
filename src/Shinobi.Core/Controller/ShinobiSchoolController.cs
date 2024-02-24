using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shinobi.Core.Models;
using Shinobi.Core.Repositories;

namespace Shinobi.Core.Controller;

[Route("api/[controller]")]
[ApiController]
public class ShinobiSchoolController : ControllerBase
{
    private readonly INinjaRepository _ninjaRepository;
    private readonly ILogger<ShinobiSchoolController> _logger;
    
    public ShinobiSchoolController(INinjaRepository ninjaRepository, ILogger<ShinobiSchoolController> logger)
    {
        _ninjaRepository = ninjaRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var people = _ninjaRepository.Get();

        if (people.Any() is false)
            return NotFound("No Ninjas found");

        return Ok(new NinjaResponse()
        {
            Ninjas = Guard.Against.Null(people)
        });
    }
    
    [HttpGet("{ninjaId}")]
    public IActionResult Get(int ninjaId)
    {
        var locatedNinja = _ninjaRepository.Get(ninjaId);

        return locatedNinja is null
            ? NotFound($"Ninja with {ninjaId} not found")
            : Ok(locatedNinja);    
    }
    
    [HttpPost]
    public IActionResult Register(Ninja ninja)
    {
        if (_ninjaRepository.Get().Any(existing
            => existing.FirstName.Equals(ninja.FirstName) 
               && existing.LastName.Equals(ninja.LastName)))
        {
            return Conflict($"Ninja with FirstName: {ninja.FirstName} and LastName: {ninja.LastName} already exists");
        }
        
        try
        { 
            _ninjaRepository.Register(ninja);
        }
        catch (Exception ex)
        {
            _logger.LogError("Problem registering Ninja due to {Ex}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Unable to Register Ninja");
        }

        return CreatedAtAction(nameof(Get), 
            new { ninjaId = ninja.Id }, ninja);
    }
}