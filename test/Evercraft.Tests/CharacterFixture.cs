using Evercraft.Dice;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;

namespace Evercraft.Tests
{
    internal class CharacterFixture : ITestFixtureBuilder
    {
        public static implicit operator Character(CharacterFixture fixture) => fixture.Build();

        public CharacterFixture WithRoller(IDieRoller roller) => this.With(ref _roller, roller);

        public CharacterFixture WithName(string name) => this.With(ref _name, name);

        public CharacterFixture WithFactory(IAbilityFactory factory) => this.With(ref _abilityFactory, factory);
        private Character Build() => new Character(_roller, _abilityFactory) {Name = _name};

        private IDieRoller _roller = Substitute.For<IDieRoller>();
        private IAbilityFactory _abilityFactory = new AbilityFactory();
        private string _name;
        private Ability _strength;
    }
}