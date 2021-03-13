using Evercraft.Characters.Abilities;
using Evercraft.Characters.Classes;
using Evercraft.Dice;

namespace Evercraft.Characters
{
    public class FighterCharacter : Character<Fighter>
    {
        public FighterCharacter(IDieRoller roller, IAbilityFactory abilityFactory) 
            : base(roller, abilityFactory)
        {
            HitPoints = 10;
            Class = new Fighter(this);
        }

        protected override void ModifyHitPoint(int level)
        {
            HitPoints += DefaultHitPoints + 5;
        }
    }
}