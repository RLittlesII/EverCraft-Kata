using System.Collections.Generic;
using Evercraft.Classes;
using Evercraft.Dice;

namespace Evercraft.Modifiers.Class
{
    public interface IModifier<TClass> : Evercraft.Modifiers.IModifier<TClass>
        where TClass : ICharacterClass
    {
        int Modify(ICharacter<TClass> character);
    }
    
    public class MonkCharacter : ClassCharacter<Monk>
    {
        public MonkCharacter(IDieRoller roller, IAbilityFactory abilityFactory)
            : base(roller, abilityFactory)
        {
            HitPoints = 6;
        }

        public IEnumerable<Ability> Abilities { get; }
        public Monk Class { get; }

        protected override void ModifyHitPoint(int level)
        {
            HitPoints += DefaultHitPoints + 1 + Constitution.Modifier;
        }
    }
}