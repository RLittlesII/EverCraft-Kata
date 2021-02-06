using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Evercraft.Classes;
using Evercraft.Dice;
using Evercraft.Mechanics;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Evercraft
{
    public class Character : ReactiveObject
    {
        private const int DefaultHitPoints = 5;
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

            Experience = new Experience();

            this.WhenPropertyValueChanges(x => x.Constitution)
                .Take(1)
                .Where(x => x != null && x.Modifier > 0)
                .Subscribe(x => HitPoints += x.Modifier);

            this.WhenPropertyValueChanges(x => x.Experience.Total)
                .Select(_ => Math.DivRem(_, 1000, out var remainder) + 1)
                .ToPropertyEx(this, character => character.Level);

            this.WhenPropertyValueChanges(x => x.Level)
                .Where(level => level > 1)
                .Distinct()
                .Subscribe(_ =>
                {
                    HitPoints += DefaultHitPoints + Constitution.Modifier;
                });
        }

        public int Level { [ObservableAsProperty] get; }

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

        public Experience Experience { get; }

        public bool IsDead { [ObservableAsProperty] get; }

        public IObservable<RollEvent> Attack() => Observable.Return(new RollEvent(_roller.Roll<TwentySided>(), Strength.Modifier + Math.DivRem(Level, 2, out var remainder)));

        public void GainExperience() => Experience.Increase(10);

        public void TakeDamaged(int damage) => HitPoints -= damage;
    }

    public interface ICharacter<T>
        where T : IClass
    {
        IEnumerable<Ability> Abilities { get; }
         T Class { get; }
    }

    public class ClassCharacter<T> : ICharacter<T>
        where T : IClass
    {
        public IEnumerable<Ability> Abilities { get; }

        public T Class { get; }
    }
        
}