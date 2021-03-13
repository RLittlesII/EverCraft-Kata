using System;
using Evercraft.Characters;
using Evercraft.Characters.Abilities;
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
        public void WhenAttack_ShouldDefenderTakesMinimumDamage()
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(15);
            var factory = Substitute.For<IAbilityFactory>();
            factory.Create<Strength>(Arg.Any<int>()).Returns(new Strength(5));
            Character attacker = new CharacterFixture().WithFactory(factory).WithRoller(roller);
            Attack sut = new AttackFixture().WithAttacker(attacker);

            // Then
            sut.Defender
                .HitPoints
                .Should()
                .Be(4);
        }

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

        [Theory]
        [InlineData(12, 21)]
        [InlineData(20, 25)]
        public void WhenAttack_ShouldApplyStrengthToRoll(int strength, int modified)
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(20);
            var factory = Substitute.For<IAbilityFactory>();
            factory.Create<Strength>(Arg.Any<int>()).Returns(new Strength(strength));
            factory.Create<Constitution>(Arg.Any<int>()).Returns(new Constitution(10));
            Character attacker = new CharacterFixture().WithFactory(factory).WithRoller(roller);
            Attack sut = new AttackFixture().WithAttacker(attacker);

            // Then
            sut.AttackRoll
                .Modified
                .Should()
                .Be(modified);
        }

        [Fact]
        public void WhenAttack_ShouldApplyStrengthToDamage()
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(15);
            var factory = Substitute.For<IAbilityFactory>();
            factory.Create<Strength>(Arg.Any<int>()).Returns(new Strength(12));
            factory.Create<Constitution>(Arg.Any<int>()).Returns(new Constitution(10));
            Character attacker = new CharacterFixture().WithFactory(factory).WithRoller(roller);
            Attack sut = new AttackFixture().WithAttacker(attacker);

            // Then
            sut.Defender
                .HitPoints
                .Should()
                .Be(3);
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
        [Fact]
        public void WhenCriticalAttack_ShouldApplyDoubleStrength()
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(20);
            var factory = Substitute.For<IAbilityFactory>();
            factory.Create<Strength>(Arg.Any<int>()).Returns(new Strength(12));
            Character attacker = new CharacterFixture().WithFactory(factory).WithRoller(roller);
            Attack sut = new AttackFixture().WithAttacker(attacker);

            // Then
            sut.Defender
                .HitPoints
                .Should()
                .Be(1);
        }

        [Theory]
        [InlineData(10, 12)]
        [InlineData(11, 12)]
        public void WhenDefend_ShouldApplyDexterity(int roll, int dexterity)
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(roll);
            var factory = Substitute.For<IAbilityFactory>();
            factory.Create<Strength>(Arg.Any<int>()).Returns(new Strength(10));
            factory.Create<Constitution>(Arg.Any<int>()).Returns(new Constitution(10));
            factory.Create<Dexterity>(Arg.Any<int>()).Returns(new Dexterity(dexterity));
            Character attacker = new CharacterFixture().WithFactory(factory).WithRoller(roller);
            Attack sut = new AttackFixture().WithDefender(attacker);

            // Then
            sut.Defender
                .HitPoints
                .Should()
                .Be(5);
        }

        [Fact]
        public void WhenAttackSuccessful_AttackerGainsExperience()
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(15);
            var factory = Substitute.For<IAbilityFactory>();
            factory.Create<Strength>(Arg.Any<int>()).Returns(new Strength(5));
            Character attacker = new CharacterFixture().WithFactory(factory).WithRoller(roller);
            Attack sut = new AttackFixture().WithAttacker(attacker);

            // Then
            sut.Attacker
                .Experience
                .Current
                .Should()
                .Be(10);
        }
    }

    internal class AttackFixture : ITestFixtureBuilder
    {
        private Character _attacker = new CharacterFixture().WithName("Bishop Magic");
        private Character _defender = new CharacterFixture().WithName("King Hate");

        public static implicit operator Attack(AttackFixture fixture) => fixture.Build();

        public AttackFixture WithAttacker(Character attacker) => this.With(ref _attacker, attacker);
        public AttackFixture WithDefender(Character defender) => this.With(ref _defender, defender);
        private Attack Build() =>
            new Attack(_attacker, _defender);
    }
}