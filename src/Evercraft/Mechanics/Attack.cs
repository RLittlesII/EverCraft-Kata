using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Evercraft.Mechanics
{
    public class Attack : ReactiveObject
    {
        public Attack(Character attacker, Character defender)
        {
            Attacker = attacker;
            Defender = defender;

            this.WhenPropertiesValueChanges(x => x.Attacker,
                    x => x.Defender,
                    (attack, defend) => (attacker, defender))
                .Where(x => x.attacker != null && x.defender != null)
                .Subscribe(ExecuteAttack);
        }

        [Reactive] public Character Attacker { get; private set; }

        [Reactive] public Character Defender { get; private set;}

        private static void ExecuteAttack((Character attacker, Character defender) attack)
        {
            var attackRoll = attack.attacker.Attack();
            if (attackRoll >= attack.defender.ArmorClass)
            {
                attack.defender.Damaged(attackRoll == 20 ? 2 : 1);
            }
        }
    }
}