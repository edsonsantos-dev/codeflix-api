using Bogus;

namespace Codeflix.Catalog.UnitTests.Domain.Fixturies;

public abstract class BaseFixture
{
    public Faker Faker { get; private set; }

    public BaseFixture() =>
        Faker = new Faker("pt_BR");
}