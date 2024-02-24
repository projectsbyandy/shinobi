using Ardalis.GuardClauses;
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

        if (people.IsNullOrEmpty())
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
        bool saved;
        try
        { 
            saved = _ninjaRepository.Register(ninja);
        }
        catch (Exception ex)
        {
            _logger.LogError("Problem registering Ninja due to {Ex}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Unable to Register Ninja");
        }
        
        return saved
            ? CreatedAtAction(nameof(Get), new { ninjaId = ninja.Id}, ninja)
            : Conflict($"Ninja with Id: {ninja.Id} already exists");
    }
}