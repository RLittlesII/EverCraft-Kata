using Evercraft.Classes;
using Evercraft.Dice;
using Evercraft.Modifiers.Class;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;

namespace Evercraft.Tests
{
    internal class Giyatsu
    {
        public static MonkCharacterFixture Character { get; } =
            new MonkCharacterFixture()
                .WithName(nameof(Giyatsu));

        internal class MonkCharacterFixture : ITestFixtureBuilder
        {
            public static implicit operator MonkCharacter(MonkCharacterFixture fixture) => fixture.Build();

            public MonkCharacterFixture WithRoller(IDieRoller roller) => this.With(ref _roller, roller);

            public MonkCharacterFixture WithName(string name) => this.With(ref _name, name);

            public MonkCharacterFixture WithClass(ICharacterClass characterClass) =>
                this.With(ref _characterClass, characterClass);

            public MonkCharacterFixture WithFactory(IAbilityFactory factory) => this.With(ref _abilityFactory, factory);

            private IDieRoller _roller = Substitute.For<IDieRoller>();
            private IAbilityFactory _abilityFactory = new AbilityFactory();
            private string _name;
            private Ability _strength;
            private ICharacterClass _characterClass;
            private MonkCharacter Build() => new MonkCharacter(_roller, _abilityFactory);
        }
    }
}