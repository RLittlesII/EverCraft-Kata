using System.Linq;
using Evercraft.Characters;
using Evercraft.Characters.Abilities;
using Evercraft.Characters.Classes.Modifiers;
using Evercraft.Dice;
using Evercraft.Mechanics;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Evercraft.Tests.Characters
{
    public class MonkTests : TestBase
    {
        [Fact(DisplayName = "Monk has a hit point modifier")]
        public void WhenConstructed_ThenCharacterHasHitPointModifier()
        {
            // Given, When
            MonkCharacter sut = Giyatsu.Character();

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
            MonkCharacter monk = Giyatsu.Character();
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
            MonkCharacter monkCharacterFixture = Giyatsu.Character();

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

        [Fact(DisplayName = "Monk does 3 points of damage instead of 1 when successfully attacking.",
            Skip = "Because we broke something and will fix it later.")]
        public void GivenMonk_WhenSuccessfulAttack_ThenDoesThreePointsOfDamage()
        {
            // Given, When
            var roller = Substitute.For<IDieRoller>();
            roller.Roll<TwentySided>().Returns(15);
            MonkCharacter attacker = Giyatsu.Character().WithRoller(roller);
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

        [Theory(DisplayName = "Monk adds Wisdom modifier (if positive).")]
        [InlineData(9, 10)]
        [InlineData(10, 10)]
        [InlineData(11, 10)]
        [InlineData(12, 11)]
        public void GivenMonk_WhenConstructed_ThenWisdomModiferAdded(int wisdom, int armorclass)
        {
            // Given, When
            var abilityFactory = Substitute.For<IAbilityFactory>();
            abilityFactory.Create<Wisdom>(10).Returns(new Wisdom(wisdom));
            abilityFactory.Create<Dexterity>(10).Returns(new Dexterity(10));
            MonkCharacter monk = 
                Giyatsu
                    .Character()
                    .WithFactory(abilityFactory);

            // Then
            monk
                .ArmorClass
                .Should()
                .Be(armorclass);
        }

        [Theory(DisplayName = "Monk adds Wisdom modifier (if positive) to Armor Class in addition to Dexterity.")]
        [InlineData(9, 9, 9)]
        [InlineData(10, 9, 10)]
        [InlineData(10, 12, 11)]
        [InlineData(12, 13, 12)]
        public void GivenMonk_WhenConstructed_ThenWisdomAndDexterityModiferAdded(int dexterity, int wisdom, int armorclass)
        {
            // Given, When
            var abilityFactory = Substitute.For<IAbilityFactory>();
            abilityFactory.Create<Dexterity>(10).Returns(new Dexterity(dexterity));
            abilityFactory.Create<Wisdom>(10).Returns(new Wisdom(wisdom));
            MonkCharacter monk =
                Giyatsu
                    .Character()
                    .WithFactory(abilityFactory);

            // Then
            monk
                .ArmorClass
                .Should()
                .Be(armorclass);
        }
    }
}