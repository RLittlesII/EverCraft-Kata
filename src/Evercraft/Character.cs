using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Security.Cryptography;
using DynamicData.Binding;
using Evercraft.Dice;
using Evercraft.Mechanics;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Evercraft
{
    public class Character : ReactiveObject
    {
        private readonly IDieRoller _roller;
        private readonly IAbilityFactory _abilityFactory;

        public Character(IDieRoller roller, IAbilityFactory abilityFactory)
        {
            _roller = roller;
            _abilityFactory = abilityFactory;

            this.WhenPropertyValueChanges(x => x.HitPoints)
                .Select(x => x <= 0)
                .ToPropertyEx(this, x => x.IsDead);

            Strength = _abilityFactory.Create<Strength>(10);
            Dexterity = _abilityFactory.Create<Dexterity>(10);
            Constitution = _abilityFactory.Create<Constitution>(10);
            Wisdom = _abilityFactory.Create<Wisdom>(10);
            Intelligence = _abilityFactory.Create<Intelligence>(10);
            Charisma = _abilityFactory.Create<Charisma>(10);

            this.WhenPropertyValueChanges(x => x.Constitution)
                .Take(1)
                .Where(x => x != null && x.Modifier > 0)
                .Subscribe(x => HitPoints += x.Modifier);
        }

        [Reactive] public string Name { get; set; }

        [Reactive] public Alignment Alignment { get; set; }

        [Reactive] public int ArmorClass { get; set; } = 10;

        [Reactive] public int HitPoints { get; private set; } = 5;

        public Ability Strength { get; }
        
        public Ability Dexterity { get; }
        
        [Reactive] public Ability Constitution { get; private set; }
        
        public Ability Wisdom { get; }
        
        public Ability Intelligence { get; }
        
        public Ability Charisma { get; }

        public bool IsDead { [ObservableAsProperty] get; }

        // public RollEvent Attack() => new RollEvent(_roller.Roll<TwentySided>(), Strength.Modifier);

        public IObservable<RollEvent> Attack() => Observable.Return(new RollEvent(_roller.Roll<TwentySided>(), Strength.Modifier));

        public void Damaged(int damage) => HitPoints -= damage;
    }
}