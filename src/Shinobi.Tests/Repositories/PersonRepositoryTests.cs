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
        var people= _sut?.Get();

        people.Count().Should().BeGreaterThan(9);
        people?.All(person => person.FirstName.Contains("John")).Should().BeTrue();
    }
    
    [Test]
    public void Verify_Person_Is_Null_When_None_Located()
    {
        _sut = IPersonRepositoryMock.GetMock(new PersonMockOptions() { ReturnSingle = true});

        var people= _sut?.Get();

        people?.Should().BeNull();
    }
    
    [Test]
    public void Verify_When_Person_Found_Id_Is_Correct()
    {
        _sut = IPersonRepositoryMock.GetMock(new PersonMockOptions() { ReturnSingle = true});

        var people= _sut?.Get(1);

        people?.PersonId.Should().Be(1);
    }
    
    [Test]
    public void Verify_When_Person_NotFound_Id_Null_Is_Returned()
    {
        _sut = IPersonRepositoryMock.GetMock(new PersonMockOptions() { ReturnSingle = true});

        var people= _sut?.Get(123);

        people?.PersonId.Should().BeNull();
    }
}