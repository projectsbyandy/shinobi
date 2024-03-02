using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Shinobi.Core.Controller;
using Shinobi.Core.Models;
using Shinobi.Core.Repositories;

namespace Shinobi.Tests.Controllers;

public class ShinobiSchoolControllerTests
{
    private ShinobiSchoolController? _sut;
    private Mock<INinjaRepository>? _repositoryMock;
    private IMock<ILogger<ShinobiSchoolController>>? _loggerMock;
    private readonly Ninja _existingNinja = new Ninja("Robson", "Etchfield")
    {
        Id = 1,
        Level = 12
    };
    
    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<INinjaRepository>();
        _loggerMock = new Mock<ILogger<ShinobiSchoolController>>();
        
        _repositoryMock?.Setup(repository => repository.Get()).Returns([]);
        _repositoryMock?.Setup(repository => repository.Get(1)).Returns(
            _existingNinja
        );
        _sut = new ShinobiSchoolController(_repositoryMock!.Object, _loggerMock!.Object);
    }

    #region GetAll Endpoint
    
    [Test]
    public void Verify_All_Ninjas_Can_Be_Retrieved()
    {
        // Given
        _repositoryMock?.Setup(repository => repository.Get()).Returns(new List<Ninja>()
        {
            new("Robson", "Etchfield")
            {
                Id = 1,
                Level = 12
            },
            new("Obie", "Smith")
            {
                Id = 2,
                Level = 11
            }
        });
        _sut = new ShinobiSchoolController(_repositoryMock!.Object, _loggerMock!.Object);

        // When
        var response = _sut?.GetAll();

        // Then
        response.Should().BeOfType<OkObjectResult>();
        var objectResult = response as ObjectResult;
        objectResult?.StatusCode.Should().Be(200);
        
        var data = objectResult?.Value as NinjaResponse;
        data.Should().NotBeNull();

        data?.Ninjas.ToList().Count.Should().Be(2);
    }

    [Test]
    public void Verify_All_Ninjas_Response_When_Empty()
    {
        // Given // When
        var response = _sut?.GetAll();

        // Then
        response.Should().BeOfType<NotFoundObjectResult>();
        var notFoundObjectResult = response as NotFoundObjectResult;
        notFoundObjectResult.Should().NotBeNull();
        
        notFoundObjectResult?.StatusCode.Should().Be(404);
        notFoundObjectResult?.Value.Should().Be("No Ninjas found");
    }
    #endregion

    #region Get Endpoint

    [Test]
    public void Verify_Ninja_Can_Be_Retrieved()
    {
        // Given
        _sut = new ShinobiSchoolController(_repositoryMock!.Object, _loggerMock!.Object);

        // When
        var response = _sut?.Get(1);

        // Then
        response.Should().BeOfType<OkObjectResult>();
        var okObjectResult = response as OkObjectResult;
        okObjectResult?.StatusCode.Should().Be(200);
        
        var ninja = okObjectResult?.Value as Ninja;
        ninja.Should().NotBeNull();
        
        ninja?.Id.Should().Be(_existingNinja.Id);
        ninja?.FirstName.Should().Be(_existingNinja.FirstName);
        ninja?.LastName.Should().Be(_existingNinja.LastName);
        ninja?.Level.Should().Be(_existingNinja.Level);
    }
    #endregion

    [Test]
    public void Verify_Create_Ninja_Mandatory_Fields_Repository_DbUpdateException_Returns_Server_Error()
    {
        // Given
        _repositoryMock?.Setup(repository => 
            repository.Add(It.IsAny<Ninja>())).Throws<DbUpdateException>();
        _sut = new ShinobiSchoolController(_repositoryMock!.Object, _loggerMock!.Object);
        
        // When
        var response = _sut?.Register(new Ninja("Emily", "Execept"));

        // Then
        response.Should().BeOfType<ObjectResult>();
        var objectResult = response as ObjectResult;
        
        objectResult?.StatusCode.Should().Be(500);
        objectResult?.Value.Should().Be("Unable to Register Ninja");
    }
    
    [Test]
    public void Verify_Create_Ninja_Existing_Check_Returns_Bad_Request()
    {
        // Given
        _repositoryMock?.Setup(repository => repository.Get()).Returns(new List<Ninja>()
        {
            _existingNinja
        });
        
        _sut = new ShinobiSchoolController(_repositoryMock!.Object, _loggerMock!.Object);

        // When
        var response = _sut?.Register(_existingNinja);
        
        // Then
        response.Should().BeOfType<BadRequestObjectResult>();
        var badRequestObjectResult = response as BadRequestObjectResult;
        
        badRequestObjectResult?.Value.Should().Be($"Ninja with 'FirstName: {_existingNinja.FirstName}' and 'LastName: {_existingNinja.LastName}' already exists");
    }
    
    [Test]
    public void Verify_A_Ninja_Can_Be_Registered_Successfully_Ninja()
    {
        // Given
        var referenceNinja = new Ninja("Ted", "Boss")
        {
            Id = 1,
            Level = 12
        };
        
        _sut = new ShinobiSchoolController(_repositoryMock!.Object, _loggerMock!.Object);

        // When
        var response = _sut?.Register(referenceNinja);
        
        // Then
        response.Should().BeOfType<CreatedAtActionResult>();
        var createdAtActionResult = response as CreatedAtActionResult;

        createdAtActionResult?.StatusCode.Should().Be(201);
        createdAtActionResult?.Value.Should().Be(referenceNinja);
    }
}