using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Evercraft.Characters.Abilities;
using Evercraft.Characters.Classes;
using Evercraft.Dice;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;

namespace Evercraft.Characters
{
    public class MonkCharacter : Character<Monk>
    {
        public MonkCharacter(IDieRoller roller, IAbilityFactory abilityFactory)
            : base(roller, abilityFactory)
        {
            HitPoints = 6;
            Class = new Monk();
            
            // Armor Class = 10 + Wisdom[<10]
            int SelectArmorClassModifier((Ability dexterity, Ability wisdom) x)
            {
                var result = 0;
                if (x.wisdom.Modifier > 0)
                {
                    result += x.wisdom.Modifier;
                }

                result += x.dexterity.Modifier;
                return result;
            }

            this.WhenPropertiesValueChanges(
                    x => x.Dexterity,
                    x => x.Wisdom,
                    (dexterity, wisdom) => (dexterity, wisdom))
                .Where(x => x.wisdom != null && x.dexterity != null)
                .Select(SelectArmorClassModifier)
                .Subscribe(abilities => ArmorClass = 10 + abilities)
                .DisposeWith(CharacterSubscriptions);
        }

        protected override void ModifyHitPoint(int level)
        {
            HitPoints += DefaultHitPoints + 1 + Constitution.Modifier;
        }
    }
}