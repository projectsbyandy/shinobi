using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
    private IMock<ILogger<ShinobiSchoolController>> _loggerMock;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<INinjaRepository>();
        _loggerMock = new Mock<ILogger<ShinobiSchoolController>>();
    }

    #region GetAll Endpoint
    
    [Test]
    public void Verify_All_Ninjas_Can_Be_Retrieved()
    {
        // Given
        _repositoryMock?.Setup(repository => repository.Get()).Returns(new List<Ninja>()
        {
            new()
            {
                Id = 1,
                FirstName = "Robson",
                LastName = "Etchfield",
                Level = 12
            },
            new()
            {
                Id = 2,
                FirstName = "Obie",
                LastName = "Smith",
                Level = 11
            }
        });
        _sut = new ShinobiSchoolController(_repositoryMock!.Object, _loggerMock.Object);

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
        // Given
        _repositoryMock?.Setup(repository => repository.Get()).Returns([]);
        _sut = new ShinobiSchoolController(_repositoryMock!.Object, _loggerMock.Object);

        // When
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
        
        _repositoryMock?.Setup(repository => repository.Get(1)).Returns(
            new Ninja()
            {
                Id = 1,
                FirstName = "Robson",
                LastName = "Etchfield",
                Level = 12
            }
        );
        _sut = new ShinobiSchoolController(_repositoryMock!.Object, _loggerMock.Object);

        // When
        var response = _sut?.Get(1);

        // Then
        response.Should().BeOfType<OkObjectResult>();
        var okObjectResult = response as OkObjectResult;
        okObjectResult?.StatusCode.Should().Be(200);
        
        var ninja = okObjectResult?.Value as Ninja;
        ninja.Should().NotBeNull();
        
        ninja?.Id.Should().Be(1);
        ninja?.FirstName.Should().Be("Robson");
        ninja?.LastName.Should().Be("Etchfield");
        ninja?.Level.Should().Be(12);

        
    }
    

    #endregion
    [Test]
    public void Verify_Create_Ninja_Mandatory_Fields_Validation()
    {
    }

    [Test]
    public void Verify_Create_Ninja_Existing_Check_Validation()
    {
    }
}