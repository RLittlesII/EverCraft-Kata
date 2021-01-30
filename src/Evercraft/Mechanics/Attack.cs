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

            Attacker
                .Attack()
                .ToPropertyEx(this, x => x.AttackRoll);

            var whenCharactersChanged =
                this.WhenPropertiesValueChanges(x => x.Attacker,
                    x => x.Defender,
                    (attack, defend) => (attacker, defender));

            this.WhenPropertyValueChanges(x => x.AttackRoll)
                .CombineLatest(whenCharactersChanged, (attackEvent, characters) => (attackEvent, characters.defender))
                .Where(x => x.attackEvent.Modified >= x.defender.ArmorClass + x.defender.Dexterity.Modifier)
                .Subscribe(ExecuteAttack);
        }

        [Reactive] public Character Attacker { get; private set; }

        [Reactive] public Character Defender { get; private set;}

        public RollEvent AttackRoll { [ObservableAsProperty] get; }

        private static void ExecuteAttack((RollEvent roll, Character defender) attack)
        {
            var rollModifier = attack.roll.Modifier > 0 ? attack.roll.Modifier : 0;
            var damage = attack.roll.Roll == 20 ? rollModifier * 2 + 2 : rollModifier + 1;
            attack.defender.Damaged(damage);
        }
    }

    public class RollEvent
    {
        public RollEvent(int roll, int modifier)
        {
            Roll = roll;
            Modifier = modifier;
            Modified = Roll + Modifier;
        }

        public int Roll { get; }
        public int Modified { get; }
        public int Modifier { get; }
    }
}