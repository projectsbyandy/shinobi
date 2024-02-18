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
    
    public ShinobiSchoolController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var personResponse = new PersonResponse()
        {
            Persons = _personRepository.Get()
        };

        return personResponse.Persons.IsNullOrEmpty()
            ? NotFound("No Ninjas found")
            : Ok(personResponse.Persons);    
    }
    
    [HttpGet("{personId}")]
    public IActionResult Get(int personId)
    {
        var locatedPerson = _personRepository.Get(personId);

        return locatedPerson is null
            ? NotFound($"Ninja with {personId} not found")
            : Ok(locatedPerson);    
    }
}