using System;
using Evercraft.Dice;
using FluentAssertions;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Xunit;

namespace Evercraft.Tests
{
    public class CharacterTests
    {
        [Fact]
        public void WhenNameChanged_ShouldRaisePropertyChanged()
        {
            // Given
            Character sut = new CharacterFixture();

            sut.Changed
                .Subscribe(changedEvent =>

                    // Then
                    changedEvent
                        .PropertyName
                        .Should()
                        .Be(nameof(sut.Name)));
            // When
            sut.Name = "Bishop Magic";
        }

        [Fact]
        public void WhenAlignmentChanged_ShouldRaisePropertyChanged()
        {
            // Given
            Character sut = new CharacterFixture();

            sut.Changed
                .Subscribe(changedEvent =>

                    // Then
                    changedEvent
                        .PropertyName
                        .Should()
                        .Be(nameof(sut.Alignment)));
            // When
            sut.Alignment = Alignment.Neutral;
        }
        
        [Fact]
        public void WhenConstructed_ShouldHaveArmor()
        {
            // Given, When
            Character sut = new CharacterFixture();

            // Then
            sut.HitPoints.Should().Be(5);
        }

        [Fact]
        public void WhenConstructed_ShouldHaveHitPoints()
        {
            // Given, When
            Character sut = new CharacterFixture();

            // Then
            sut.ArmorClass.Should().Be(10);
        }

        [Fact]
        public void WhenCharacterAttack_ShouldCallDiceRoller()
        {
            // Given
            var roller = Substitute.For<IDieRoller>();
            Character sut = new CharacterFixture().WithRoller(roller);

            // When
            sut.Attack();

            // Then
            roller
                .Received()
                .Roll<TwentySided>();
        }
        
        [Fact]
        public void WhenConstructed_ShouldNotBeDead()
        {
            // Given, When
            Character sut = new CharacterFixture();

            // Then
            sut.IsDead
                .Should()
                .BeFalse();
        }

        [Fact]
        public void WhenCharacterMortallyDamaged_ShouldDead()
        {
            // Given
            Character sut = new CharacterFixture();

            // When
            sut.Damaged(5);

            // Then
            sut.IsDead
                .Should()
                .BeTrue();
        }
    }

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