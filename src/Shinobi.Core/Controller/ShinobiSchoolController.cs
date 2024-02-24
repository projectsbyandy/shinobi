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
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<ShinobiSchoolController> _logger;
    
    public ShinobiSchoolController(IPersonRepository personRepository, ILogger<ShinobiSchoolController> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var people = _personRepository.Get();

        if (people.IsNullOrEmpty())
            return NotFound("No Ninjas found");

        return Ok(new PersonResponse()
        {
            People = Guard.Against.Null(people)
        });
    }
    
    [HttpGet("{personId}")]
    public IActionResult Get(int personId)
    {
        var locatedPerson = _personRepository.Get(personId);

        return locatedPerson is null
            ? NotFound($"Ninja with {personId} not found")
            : Ok(locatedPerson);    
    }
    
    [HttpPost]
    public IActionResult Create(Person person)
    {
        bool saved;
        try
        { 
            saved = _personRepository.Post(person);
        }
        catch (Exception ex)
        {
            _logger.LogError("Problem creating Person due to {Ex}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "Unable to Create Person");
        }
        
        return saved
            ? CreatedAtAction(nameof(Get), new { personId = person.PersonId}, person)
            : Conflict($"Person with Id: {person.PersonId} already exists");
    }
}