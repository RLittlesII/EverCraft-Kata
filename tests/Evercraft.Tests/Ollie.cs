using Evercraft.Characters;
using Evercraft.Characters.Abilities;
using Evercraft.Characters.Classes;
using Evercraft.Dice;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;

namespace Evercraft.Tests
{
    internal class Ollie
    {
        public static FighterCharacterFixture Character() =>
            new FighterCharacterFixture()
                .WithName(nameof(Ollie));

        internal class FighterCharacterFixture : ITestFixtureBuilder
        {
            public static implicit operator FighterCharacter(FighterCharacterFixture fixture) => fixture.Build();

            public FighterCharacterFixture WithRoller(IDieRoller roller) => this.With(ref _roller, roller);

            public FighterCharacterFixture WithName(string name) => this.With(ref _name, name);

            public FighterCharacterFixture WithClass(ICharacterClass characterClass) =>
                this.With(ref _characterClass, characterClass);

            public FighterCharacterFixture WithFactory(IAbilityFactory factory) => this.With(ref _abilityFactory, factory);

            private IDieRoller _roller = Substitute.For<IDieRoller>();
            private IAbilityFactory _abilityFactory = new AbilityFactory();
            private string _name;
            private Ability _strength;
            private ICharacterClass _characterClass;
            private FighterCharacter Build() => new FighterCharacter(_roller, _abilityFactory);
        }
    }
}