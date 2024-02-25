using FluentAssertions;
using Shinobi.Core.Models;
using Shinobi.Core.Repositories;
using Shinobi.Tests.MockHelpers;

namespace Shinobi.Tests.Repositories;

public class NinjaRepositoryTests
{
    private INinjaRepository? _sut;

    [SetUp]
    public void Setup()
    {
        _sut = INinjaRepositoryMock.GetMock(new NinjaMockOptions() { ReturnCount = 10});
    }

    [Test]
    public void Verify_Ninja_Returned()
    {
        // When
        var people= _sut?.Get();
        
        // Then
        people?.Count().Should().BeGreaterThan(9);
        people?.All(ninja => 
            ninja.FirstName.Contains("John") 
            && ninja.LastName.Equals("Doe")
            && ninja.Level == 3
            ).Should().BeTrue();
    }
    
    [Test]
    public void Verify_Ninja_Is_Null_When_None_Located()
    {
        // Given
        _sut = INinjaRepositoryMock.GetMock(new NinjaMockOptions() { ReturnEmpty = true});

        // When
        var people= _sut.Get();

        // Then
        people.Should().BeEmpty();
    }
    
    [TestCase(1)]
    [TestCase(4)]
    [TestCase(8)]
    public void Verify_When_Ninja_Found_Id_Is_Correct(int id)
    {
        // Given
        _sut = INinjaRepositoryMock.GetMock(new NinjaMockOptions() { ReturnSingle = true});

        // When
        var people= _sut.Get(id);

        // Then
        people?.Id.Should().Be(id);
    }
    
    [Test]
    public void Verify_When_Ninja_NotFound_Null_Is_Returned()
    {
        // Given
        _sut = INinjaRepositoryMock.GetMock(new NinjaMockOptions() { ReturnSingle = true});

        // When
        var people= _sut.Get(123);

        // Then
        people.Should().BeNull();
    }

    [Test]
    public void Verify_A_Ninja_Can_Be_Registered()
    {
        // Given
        var ninja = new Ninja()
        {
            FirstName = "Larry",
            LastName = "Peterson",
            Level = 777
        };
        
        // When
        _sut?.Add(ninja);
        
        // Then
        var registeredNinja = _sut?.Get(ninja.Id);

        registeredNinja.Should().NotBeNull();
        registeredNinja?.FirstName.Should().Be(ninja.FirstName);
        registeredNinja?.LastName.Should().Be(ninja.LastName);
        registeredNinja?.Level.Should().Be(ninja.Level);
    }

    [Test]
    public void Verify_A_Ninja_Can_Be_Deleted()
    {
        // Given 
        _sut = INinjaRepositoryMock.GetMock(new NinjaMockOptions() { ReturnSingle = true});
        var getAllAfterAdd = _sut.Get();
        getAllAfterAdd.Count.Should().Be(1);
        
        // When
        _sut.Delete(getAllAfterAdd.First().Id);
        
        //Then
        var getAllAfterDelete = _sut.Get();
        getAllAfterDelete.Count.Should().Be(0);
    }
    
}