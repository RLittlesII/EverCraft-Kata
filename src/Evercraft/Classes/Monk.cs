using System;
using System.Collections;
using System.Collections.Generic;
using Evercraft.Modifiers.Class;

namespace Evercraft.Classes
{
    public class Monk : ICharacterClass
    {
        // Modify Damage + 2
        // Modify Armor Class + Wisdom Modifier

        // Modify Hit Points + 1 per level
        // Modify Attack Roll + 2 every 3 levels
        public Monk()
        {
            Modifiers = new[] {new HitPointModifier()};
        }
        public IEnumerable<Modifiers.IModifier<Monk>> Modifiers { get; set; }

        private class HitPointModifier : IHitPointModifier<Monk>
        {
            public int Modify(ICharacter<Monk> character)
            {
                return 1;
            }
        }
    }
}