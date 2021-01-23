using System;
using Evercraft.Dice;
using FluentAssertions;
using NSubstitute;
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

        [Fact]
        public void WhenConstructed_StrengthShouldBeDefault()
        {
            // Given, When
            Character sut = new CharacterFixture();

            // Then
            sut.Strength
                .Score
                .Should()
                .Be(10);
        }

        [Fact]
        public void WhenConstructed_DexterityShouldBeDefault()
        {
            // Given, When
            Character sut = new CharacterFixture();

            // Then
            sut.Dexterity
                .Score
                .Should()
                .Be(10);
        }

        [Fact]
        public void WhenConstructed_ConstitutionShouldBeDefault()
        {
            // Given, When
            Character sut = new CharacterFixture();

            // Then
            sut.Constitution
                .Score
                .Should()
                .Be(10);
        }

        [Fact]
        public void WhenConstructed_WisdomShouldBeDefault()
        {
            // Given, When
            Character sut = new CharacterFixture();

            // Then
            sut.Wisdom
                .Score
                .Should()
                .Be(10);
        }

        [Fact]
        public void WhenConstructed_IntelligenceShouldBeDefault()
        {
            // Given, When
            Character sut = new CharacterFixture();

            // Then
            sut.Intelligence
                .Score
                .Should()
                .Be(10);
        }

        [Fact]
        public void WhenConstructed_CharismaShouldBeDefault()
        {
            // Given, When
            Character sut = new CharacterFixture();

            // Then
            sut.Charisma
                .Score
                .Should()
                .Be(10);
        }

        [Fact]
        public void WhenConstructed_ShouldHaveDefaultModifier()
        {
            // Given, When
            Character sut = new CharacterFixture();

            // Then
            sut.Charisma
                .Modifier
                .Should()
                .Be(0);
        }
    }
}