using System;
using System.Net;
using System.Windows.Input;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Evercraft
{
    public class Character : ReactiveObject
    {
        private readonly IDiceRoller _diceRoller;
        public Character(IDiceRoller rollDice)
        {
            _diceRoller = rollDice;
            ArmorClass = 10;
            HitPoints = 5;
        }
        [Reactive] public string Name { get; set; }

        [Reactive] public Alignment Alignment { get; set; }
        [Reactive] public int ArmorClass { get; private set; }
        [Reactive] public int HitPoints { get; private set; }

        public void Damaged()
        {
            HitPoints -= 1;
        }

        public int Attack() => _diceRoller.Roll<TwentySided>();
    }
}