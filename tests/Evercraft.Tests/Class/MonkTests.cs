using System.Linq;
using Evercraft.Classes;
using Evercraft.Dice;
using Evercraft.Mechanics;
using Evercraft.Modifiers.Class;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Evercraft.Tests.Class
{
    public class MonkTests : TestBase
    {
        [Fact(DisplayName = "Monk has a hit point modifier")]
        public void WhenConstructed_ThenCharacterHasHitPointModifier()
        {
            // Given, When
            MonkCharacter sut = Giyatsu.Character;

            // Then
            sut.Class
                .Modifiers
                .Should()
                .ContainSingle(x => x is IHitPointModifier);
        }

        [Fact(DisplayName = "Monk returns 1 as hit point modifier")]
        public void GivenMonk_WhenHitPointModifierCalled_ThenShouldReturnValue()
        {
            // Given
            MonkCharacter monk = Giyatsu.Character;
            var modifier = monk.Class.Modifiers.OfType<IHitPointModifier>().First();

            // When
            var result = modifier.Value();

            // Then
            result
                .Should()
                .Be(1);
        }

        [Fact(DisplayName = "Monk has 6 hit point per level")]
        public void WhenLevelUp_ThenHitPointsIncrease()
        {
            // Given
            MonkCharacter monkCharacterFixture = Giyatsu.Character;

            // When
            monkCharacterFixture
                .Experience
                .Increase(1001);

            // Then
            monkCharacterFixture
                .HitPoints
                .Should()
                .Be(12);
        }
        
        [Fact(DisplayName = "Monk does 3 points of damage instead of 1 when successfully attacking.")]
        public void GivenMonk_WhenSuccessfulAttack_ThenDoesThreePointsOfDamage()
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(15);
            MonkCharacter attacker = Giyatsu.Character.WithRoller(roller);
            var defender = Hate.Character;
            Attack attack = new AttackFixture()
                .WithAttacker(attacker)
                .WithDefender(defender);

            // Then
            defender
                .HitPoints
                .Should()
                .Be(2);
        }
    }
}