using System;
using System.Collections.Generic;
using Evercraft.Classes;
using Evercraft.Dice;
using Evercraft.Mechanics;

namespace Evercraft.Modifiers.Class
{
    public class MonkCharacter : Character<Monk>
    {
        public MonkCharacter(IDieRoller roller, IAbilityFactory abilityFactory)
            : base(roller, abilityFactory)
        {
            HitPoints = 6;
            Class = new Monk();
        }

        public IEnumerable<Ability> Abilities { get; }

        public Monk Class { get; }

        protected override void ModifyHitPoint(int level)
        {
            HitPoints += DefaultHitPoints + 1 + Constitution.Modifier;
        }
    }
}