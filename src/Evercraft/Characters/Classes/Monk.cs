using System.Collections.Generic;
using Evercraft.Characters.Classes.Modifiers;

namespace Evercraft.Characters.Classes
{
    public class Monk : ICharacterClass
    {
        // Modify Damage + 2
        // Modify Armor Class + Wisdom Modifier

        // Modify Hit Points + 1 per level
        // Modify Attack Roll + 2 every 3 levels
        public Monk()
        {
            Modifiers = new List<IClassModifier> {new HitPointModifier(), new AttackModifier()};
        }

        public IEnumerable<IClassModifier> Modifiers { get; set; }

        private class HitPointModifier : IHitPointModifier
        {
            public int Value() => 1;
        }
        
        private class AttackModifier : IAttackModifier
        {
            public int Value() => 2;
        }
    }
}