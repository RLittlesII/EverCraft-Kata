using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Evercraft.Tests
{
    public class AttackTests
    {
        [Fact]
        public void WhenAttack_ShouldTakeDamage()
        {
            // Given
            var roller = Substitute.For<IDiceRoller>();
            roller.Roll<TwentySided>().Returns(11);
            Character attacker = new CharacterFixture().WithRoller(roller);
            Character defender = new CharacterFixture();
            Attack attack = new Attack(attacker, defender);

            // When
            attack.AttackCommand.Execute().Subscribe();

            // Then
            defender.HitPoints.Should().BeLessThan(5);
        }

        [Fact]
        public void WhenAttack_ShouldNotTakeDamage()
        {
            // Given
            var roller = Substitute.For<IDiceRoller>();
            roller.Roll<TwentySided>().Returns(10);
            Character attacker = new CharacterFixture().WithRoller(roller);
            Character defender = new CharacterFixture();
            Attack attack = new Attack(attacker, defender);

            // When
            attack.AttackCommand.Execute().Subscribe();

            // Then
            defender.HitPoints.Should().Be(5);
        }

        [Fact]
        public void WhenCriticalAttack_ShouldTakeDamage()
        {
            // Given
            var roller = Substitute.For<IDiceRoller>();
            roller.Roll<TwentySided>().Returns(20);
            Character attacker = new CharacterFixture().WithRoller(roller);
            Character defender = new CharacterFixture();
            Attack attack = new Attack(attacker, defender);

            // When
            attack.AttackCommand.Execute().Subscribe();

            // Then
            defender.HitPoints.Should().BeLessThan(5);
        }
    }
}