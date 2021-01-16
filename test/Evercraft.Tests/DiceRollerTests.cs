using Evercraft.Dice;
using FluentAssertions;
using Xunit;

namespace Evercraft.Tests
{
    public class DiceRollerTests
    {
        public class TwentySidedTests
        {
            [Fact]
            public void WhenRoll_ShouldBeGreaterThanZero()
            {
                // Given
                var roller = new TwentySided();

                // When
                var result = ((IDie)roller).Roll();

                // Then
                result
                    .Should()
                    .BeGreaterThan(0);
            }

            [Fact]
            public void WhenRoll_ShouldBeLessThanOrEqualToTwenty()
            {
                // Given
                var roller = new TwentySided();

                // When
                var result = ((IDie)roller).Roll();

                // Then
                result
                    .Should()
                    .BeLessOrEqualTo(20);
            }
        }
    }
}