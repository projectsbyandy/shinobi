using FluentAssertions;
using Shinobi.Core.Repositories;
using Shinobi.Tests.MockHelpers;

namespace Shinobi.Tests.Repositories;

public class PersonRepositoryTests
{
    private IPersonRepository? _sut;

    [SetUp]
    public void Setup()
    {
        _sut = IPersonRepositoryMock.GetMock(new PersonMockOptions() { ReturnCount = 10});
    }

    [Test]
    public void Verify_Person_Returned()
    {
        // When
        var people= _sut?.Get();
        
        // Then
        people?.Count().Should().BeGreaterThan(9);
        people?.All(person => person.FirstName.Contains("John")).Should().BeTrue();
    }
    
    [Test]
    public void Verify_Person_Is_Null_When_None_Located()
    {
        // Given
        _sut = IPersonRepositoryMock.GetMock(new PersonMockOptions() { ReturnSingle = true});

        // When
        var people= _sut?.Get();

        // Then
        people?.Should().BeNull();
    }
    
    [Test]
    public void Verify_When_Person_Found_Id_Is_Correct()
    {
        // Given
        _sut = IPersonRepositoryMock.GetMock(new PersonMockOptions() { ReturnSingle = true});

        // When
        var people= _sut?.Get(1);

        // Then
        people?.PersonId.Should().Be(1);
    }
    
    [Test]
    public void Verify_When_Person_NotFound_Null_Is_Returned()
    {
        // Given
        _sut = IPersonRepositoryMock.GetMock(new PersonMockOptions() { ReturnSingle = true});

        // When
        var people= _sut?.Get(123);

        // Then
        people?.Should().BeNull();
    }
}