using System.Linq;
using Evercraft.Classes;
using Evercraft.Modifiers.Class;
using FluentAssertions;
using Xunit;

namespace Evercraft.Tests.Class
{
    public class MonkTests
    {
        [Fact]
        public void WhenConstructed_ThenCharacterHasHitPoints()
        {
            // Given, When
            Monk sut = new Monk();

            // Then
            sut.Modifiers
                .Should()
                .ContainSingle(x => x is IHitPointModifier<Monk>);
        }

        [Fact]
        public void GivenMonk_WhenHitPointModifierCalled_ThenShouldReturnModifier()
        {
            // Given
            Monk sut = new Monk();

            // When
            var modifier = sut.Modifiers.OfType<IHitPointModifier<Monk>>().First();

            var result = modifier.Modify(Giyatsu.Character);
            // Then
            result
                .Should()
                .Be(1);
        }

        [Fact]
        public void WhenLevelUp_ThenHitPointsIncrease()
        {
            // Given, When
            Giyatsu
                .Character
                .Experience
                .Increase(1001);

            // Then
            Giyatsu
                .Character
                .HitPoints
                .Should()
                .Be(12);
        }
    }
}