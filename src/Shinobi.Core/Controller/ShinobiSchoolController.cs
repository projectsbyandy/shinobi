using Microsoft.AspNetCore.Mvc;
using Shinobi.Core.Data;
using Shinobi.Core.Models;

namespace Shinobi.Core.Controller;

[Route("api/[controller]")]
[ApiController]
public class ShinobiSchoolController : ControllerBase
{
    private readonly ShinobiContext _shinobiContext;

    public ShinobiSchoolController(ShinobiContext shinobiContext)
    {
        _shinobiContext = shinobiContext;
    }
    
    [HttpGet]
    public PersonResponse Get()
    {
        var personResponse = new PersonResponse();
        try
        {
            personResponse.IsSuccess = true;
            personResponse.Persons = _shinobiContext.Persons.ToList();
        }
        catch (Exception ex)
        {
            personResponse.IsSuccess = false;
            personResponse.Message = ex.Message;
        }
        return personResponse;
    }
}