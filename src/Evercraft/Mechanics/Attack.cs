using System;
using System.Reactive.Linq;
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

            var successfulAttackRoll =
                this.WhenPropertyValueChanges(x => x.AttackRoll)
                .CombineLatest(whenCharactersChanged,
                    (attackRoll, characters) => (attackRoll, characters))
                .Where(x => x.attackRoll.Modified >= x.characters.defender.ArmorClass + x.characters.defender.Dexterity.Modifier);
            
            successfulAttackRoll
                .Subscribe(_ => _.characters.attacker.GainExperience());

            successfulAttackRoll
                .Select(_ => (_.attackRoll, _.characters.defender))
                .Subscribe(ExecuteAttack);
        }

        [Reactive] public Character Attacker { get; private set; }

        [Reactive] public Character Defender { get; private set;}

        public RollEvent AttackRoll { [ObservableAsProperty] get; }

        private static void ExecuteAttack((RollEvent roll, Character defender) attack)
        {
            var rollModifier = attack.roll.Modifier > 0 ? attack.roll.Modifier : 0;
            var damage = attack.roll.Roll == 20 ? rollModifier * 2 + 2 : rollModifier + 1;
            attack.defender.TakeDamaged(damage);
        }
    }

    public class RollEvent
    {

        public RollEvent(int roll, int strengthModifier, int classModifier, int levelModifier)
        {
            Roll = roll;
            Modifier = strengthModifier + classModifier + levelModifier;
            Modified = Roll + Modifier;
        }
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