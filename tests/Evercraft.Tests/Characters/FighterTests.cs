using Evercraft.Characters;
using Evercraft.Dice;
using Evercraft.Mechanics;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Evercraft.Tests.Characters
{
    public class FighterTests : TestBase
    {
        [Fact(DisplayName = "Fighter starts with 10 hit points")]
        public void GivenFighter_WhenConstructed_ThenHasTenHitPoints()
        {
            // Given, When
            FighterCharacter sut = Ollie.Character();

            // Then
            sut.HitPoints
                .Should()
                .Be(10);
        }

        [Fact(DisplayName = "Fighter has 10 hit point per level")]
        public void GivenFighter_WhenLevelUp_ThenHitPointsIncrease()
        {
            // Given
            FighterCharacter sut = Ollie.Character();

            // When
            sut
                .Experience
                .Increase(1001);

            // Then
            sut
                .HitPoints
                .Should()
                .Be(20);
        }

        [Fact(DisplayName = "Fighter attacks roll is increased by 1 for every level instead of every other level.")]
        public void GivenFighter_WhenAttack_ThenModifierExists()
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(15);
            FighterCharacter attacker = Ollie.Character().WithRoller(roller);
            var defender = Hate.Character;
            Attack attack = new AttackFixture()
                .WithAttacker(attacker)
                .WithDefender(defender);

            // Then
            attack
                .AttackRoll
                .Modifier
                .Should()
                .Be(1);
        }
    }
}