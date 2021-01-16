using System;
using Evercraft.Dice;
using Evercraft.Mechanics;
using FluentAssertions;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Xunit;

namespace Evercraft.Tests
{
    public class AttackTests
    {
        [Fact]
        public void WhenAttack_DefenderShouldTakeDamage()
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(11);
            Character attacker = new CharacterFixture().WithRoller(roller);
            Attack sut = new AttackFixture().WithAttacker(attacker);

            // Then
            sut.Defender
                .HitPoints
                .Should()
                .Be(4);
        }

        [Fact]
        public void WhenAttack_DefenderShouldNotTakeDamage()
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(9);
            Character attacker = new CharacterFixture().WithRoller(roller);
            Attack sut = new AttackFixture().WithAttacker(attacker);

            // Then
            sut.Defender
                .HitPoints
                .Should()
                .Be(5);
        }

        [Fact]
        public void WhenCriticalAttack_ShouldTakeDoubleDamage()
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(20);
            Character attacker = new CharacterFixture().WithRoller(roller);
            Attack sut = new AttackFixture().WithAttacker(attacker);

            // Then
            sut.Defender
                .HitPoints
                .Should()
                .Be(3);
        }
    }

    internal class AttackFixture : ITestFixtureBuilder
    {
        private Character _attacker = new CharacterFixture().WithName("Bishop Magic");

        public static implicit operator Attack(AttackFixture fixture) => fixture.Build();

        public AttackFixture WithAttacker(Character attacker) => this.With(ref _attacker, attacker);
        private Attack Build() =>
            new Attack(_attacker,
            new CharacterFixture().WithName("King Hate"));
    }
}