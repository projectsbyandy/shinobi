using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
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
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var locatedNinja = _ninjaRepository.Get(id);

        return locatedNinja is null
            ? NotFound($"Ninja with {id} not found")
            : Ok(locatedNinja);    
    }
    
    [HttpPost]
    public IActionResult Register(Ninja ninja)
    {
        if (NinjaExists(ninja.FirstName, ninja.LastName))
            return BadRequest($"Ninja with 'FirstName: {ninja.FirstName}' and 'LastName: {ninja.LastName}' already exists");
        
        try
        { 
            _ninjaRepository.Add(ninja);
        }
        catch (Exception ex)
        {
            _logger.LogError("Problem registering Ninja due to {Ex}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Unable to Register Ninja");
        }

        return CreatedAtAction(nameof(Get), 
            new { ninjaId = ninja.Id }, ninja);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var ninja = _ninjaRepository.Get(id);
        
        if (ninja is null)
            return NotFound($"Ninja with 'Id:{id}' does not exist");
        
        _ninjaRepository.Delete(ninja.Id);

        return Ok("Deleted Successfully");
    }

    private bool NinjaExists(string firstName, string lastName)
    {
        return _ninjaRepository.Get().Any(existing
            => existing.FirstName.Equals(firstName)
               && existing.LastName.Equals(lastName));
    }
}