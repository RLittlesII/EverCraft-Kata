using Evercraft.Classes;
using Evercraft.Classes.Traits;
using FluentAssertions;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Xunit;

namespace Evercraft.Tests.Class
{
    public class FighterTests
    {
        [Fact]
        public void WhenConstructed_ShouldHaveAttackTrait()
        {
            // Given
            Fighter sut = new FighterFixture();

            // When
            ((IClass)sut).Define();

            // Then
            sut.Traits
                .Should()
                .Contain(x => x is AttackTrait);
        }

        [Fact]
        public void WhenConstructed_ShouldHaveHitPointTrait()
        {
            // Given
            Fighter sut = new FighterFixture();

            // When
            ((IClass)sut).Define();

            // Then
            sut.Traits
                .Should()
                .Contain(x => x is HitPointTrait);
        }
    }

    internal class FighterFixture : ITestFixtureBuilder
    {
        public static implicit operator Fighter(FighterFixture fixture) => fixture.Build();

        private Fighter Build() => new Fighter();
    }
}