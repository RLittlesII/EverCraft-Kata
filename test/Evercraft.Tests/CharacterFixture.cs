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

        private Character Build() => new Character(_roller) {Name = _name};

        private IDieRoller _roller = Substitute.For<IDieRoller>();
        private string _name;
    }
}