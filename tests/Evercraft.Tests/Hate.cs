using Evercraft.Characters;
using Evercraft.Tests.Characters;

namespace Evercraft.Tests
{
    public class Hate
    {
        public static Character Character { get; } =
            new CharacterFixture()
                .WithName($"King {nameof(Hate)}");
    }
}