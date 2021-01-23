using System;
using System.Reactive.Linq;
using System.Security.Cryptography;
using Evercraft.Dice;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Evercraft
{
    public class Character : ReactiveObject
    {
        private readonly IDieRoller _roller;

        public Character(IDieRoller roller)
        {
            _roller = roller;

            this.WhenPropertyValueChanges(x => x.HitPoints)
                .Select(x => x <= 0)
                .ToPropertyEx(this, x => x.IsDead);

            Strength = Dexterity = Constitution = Wisdom = Intelligence = Charisma = new Ability();
        }

        [Reactive] public string Name { get; set; }

        [Reactive] public Alignment Alignment { get; set; }

        [Reactive] public int ArmorClass { get; set; } = 10;

        [Reactive] public int HitPoints { get; set; } = 5;

        public Ability Strength { get; }
        
        public Ability Dexterity { get; }
        
        public Ability Constitution { get; }
        
        public Ability Wisdom { get; }
        
        public Ability Intelligence { get; }
        
        public Ability Charisma { get; }

        public bool IsDead { [ObservableAsProperty] get; }

        public int Attack() => _roller.Roll<TwentySided>();

        public void Damaged(int damage) => HitPoints -= damage;
    }
}