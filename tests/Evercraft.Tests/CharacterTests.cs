using System;
using Evercraft.Characters;
using Evercraft.Characters.Abilities;
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
            sut.TakeDamaged(5);

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
        public void WhenConstructed_ConstitutionModifierShouldAddHitPoints()
        {
            // Given, When
            var factory = Substitute.For<IAbilityFactory>();
            factory.Create<Constitution>(Arg.Any<int>()).Returns(new Constitution(15));
            Character sut = new CharacterFixture().WithFactory(factory);

            // Then
            sut.HitPoints
                .Should()
                .Be(7);
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
        
        [Fact]
        public void WhenConstructed_ShouldBeLevelOne()
        {
            // Given, When
            var factory = Substitute.For<IAbilityFactory>();
            factory.Create<Constitution>(Arg.Any<int>()).Returns(new Constitution(15));
            Character sut = new CharacterFixture().WithFactory(factory);

            // Then
            sut.Level   
                .Should()
                .Be(1);
        }

        [Theory]
        [InlineData(1000, 2)]
        [InlineData(2000, 3)]
        [InlineData(3000, 4)]
        [InlineData(4000, 5)]
        public void WhenExperienceGained_ShouldGainLevel(int experience, int level)
        {
            // Given
            Character sut = new CharacterFixture();

            // When
            sut.Experience.Increase(experience);

            // Then
            sut.Level
                .Should()
                .Be(level);
        }

        [Fact]
        public void WhenLevelUp_HitPointsIncrease()
        {
            // Given
            Character sut = new CharacterFixture();

            // When
            sut.Experience.Increase(1000);

            // Then
            sut.HitPoints.Should().Be(10);
        }

        [Fact]
        public void WhenLevelUp_AttackModifierIncrease()
        {
            // Given
            var result = 0;
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(10);
            Character sut = new CharacterFixture().WithRoller(roller);
            sut.Experience.Increase(1000);

            // When
            sut.Attack()
                .Subscribe(_ =>
                     result = _.Modifier);

            // Then
            result
                .Should()
                .Be(1);
        }
    }
}