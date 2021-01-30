using System.Runtime.InteropServices;
using FluentAssertions;
using ReactiveUI;
using Xunit;

namespace Evercraft.Tests
{
    public class ExperienceTests : TestBase
    {
        [Fact]
        public void WhenCurrentChanged_ShouldAccumulateTotal()
        {
            // Given
            var sut = new Experience();

            // When
            sut.Increase(10);
            sut.Increase(10);
            sut.Increase(10);

            // Then
            sut.Total
                .Should()
                .Be(30);
        }

        [Theory]
        [InlineData(1000, 0)]
        [InlineData(1010, 10)]
        [InlineData(3050, 50)]
        public void WhenIncreased_CurrentShouldBeLessThenOneThousand(int increase, int current)
        {
            // Given
            var sut = new Experience();

            // When
            sut.Increase(increase);

            // Then
            sut.Current
                .Should()
                .Be(current);
        }
    }

    public abstract class TestBase
    {
        protected TestBase() => RxApp.DefaultExceptionHandler = new EvercraftExceptionHandler();
    }
}