using System;
using System.Reactive.Concurrency;
using FluentAssertions;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Xunit;

namespace Evercraft.Tests
{
    public class CharacterTests
    {
        [Fact]
        public void WhenNameChanged_ShouldHaveValue()
        {
            // Given
            Character character = new CharacterFixture();

            // When
            character.Name = "Bishop Magic";

            // Then
            character
                .Name
                .Should()
                .Be("Bishop Magic");
        }

        [Fact]
        public void WhenAlignmentChanged_ShouldHaveValue()
        {
            // Given
            Character character = new CharacterFixture();

            // When
            character.Alignment = Alignment.Evil;

            // Then
            character
                .Alignment
                .Should()
                .Be(Alignment.Evil);
        }

        [Fact]
        public void WhenConstructed_ShouldHaveArmorClass()
        {
            // Given, When
            Character character = new CharacterFixture();

            // Then
            character
                .ArmorClass
                .Should()
                .Be(10);
        }

        [Fact]
        public void WhenConstructed_ShouldHaveHitPoints()
        {
            // Given, When
            Character character = new CharacterFixture();

            // Then
            character
                .HitPoints
                .Should()
                .Be(5);
        }
        
        [Fact]
        public void WhenAttack_ShouldReturnRoll()
        {
            // Given
            Character character = new CharacterFixture();

            // When
            var result = character.Attack();

            // Then
            result
                .Should()
                .BeGreaterOrEqualTo(0);
        }
                
        [Fact]
        public void WhenDamaged_ShouldLoseHitPoints()
        {
            // Given
            Character character = new CharacterFixture();

            // When
            character.Damaged();

            // Then
            character
                .HitPoints
                .Should()
                .Be(4);
        }
    }

    internal class CharacterFixture : ITestFixtureBuilder
    {
        private IDiceRoller _rollDice = new DiceRoller();
        public static implicit operator Character(CharacterFixture fixture) => fixture.Build();

        public CharacterFixture WithRoller(IDiceRoller rollDice) => this.With(ref _rollDice, rollDice);
        private Character Build() => new Character(_rollDice);
    }
}