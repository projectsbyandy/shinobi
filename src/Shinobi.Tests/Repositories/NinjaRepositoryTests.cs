using FluentAssertions;
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
        people?.All(ninja => ninja.FirstName.Contains("John")).Should().BeTrue();
    }
    
    [Test]
    public void Verify_Ninja_Is_Null_When_None_Located()
    {
        // Given
        _sut = INinjaRepositoryMock.GetMock(new NinjaMockOptions() { ReturnSingle = true});

        // When
        var people= _sut.Get();

        // Then
        people.Should().BeEmpty();
    }
    
    [Test]
    public void Verify_When_Ninja_Found_Id_Is_Correct()
    {
        // Given
        _sut = INinjaRepositoryMock.GetMock(new NinjaMockOptions() { ReturnSingle = true});

        // When
        var people= _sut.Get(1);

        // Then
        people?.Id.Should().Be(1);
    }
    
    [Test]
    public void Verify_When_Ninja_NotFound_Null_Is_Returned()
    {
        // Given
        _sut = INinjaRepositoryMock.GetMock(new NinjaMockOptions() { ReturnSingle = true});

        // When
        var people= _sut.Get(123);

        // Then
        people?.Should().BeNull();
    }
}