using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Evercraft
{
    public class Attack : ReactiveObject
    {
        public Attack(Character attacker, Character defender)
        {
            Attacker = attacker;
            Defender = defender;

            this.WhenAnyValue(x => x.Attacker,
                    x => x.Defender,
                    (attacker, defender) => (attacker, defender))
                .Where(x => x.attacker != null && x.defender != null)
                .InvokeCommand(this, x => x.AttackCommand);
            AttackCommand = ReactiveCommand.Create(ExecuteAttack);
        }

        private void ExecuteAttack()
        {
            var attackRoll = Attacker.Attack();
            if (attackRoll == 20 || attackRoll > Defender.ArmorClass)
            {
                Defender.Damaged();
            }
        }

        public ReactiveCommand<Unit, Unit> AttackCommand { get; set; }

        [Reactive] public Character Attacker { get; private set; }
        [Reactive] public Character Defender { get; private set; }

        public void RollAttack()
        {
        }
    }
}