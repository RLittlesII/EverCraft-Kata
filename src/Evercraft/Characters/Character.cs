using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Evercraft.Characters.Abilities;
using Evercraft.Characters.Classes;
using Evercraft.Characters.Classes.Modifiers;
using Evercraft.Dice;
using Evercraft.Mechanics;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Evercraft.Characters
{
    public class Character : ReactiveObject
    {
        protected const int DefaultHitPoints = 5;
        protected readonly IDieRoller _roller;
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

            LevelUp = ReactiveCommand.Create<int>(ExecuteLevelUp);
            this.WhenPropertyValueChanges(x => x.Level)
                .Where(level => level > 1)
                .Distinct()
                .InvokeCommand(LevelUp);
        }
        public ReactiveCommand<int, Unit> LevelUp { get; set; }

        public int Level { [ObservableAsProperty] get; }

        [Reactive] public string Name { get; set; }

        [Reactive] public Alignment Alignment { get; set; }

        [Reactive] public int ArmorClass { get; protected set; } = 10;

        [Reactive] public int HitPoints { get; protected set; } = 5;

        [Reactive] public Ability Strength { get; private set; }
        
        [Reactive] public Ability Dexterity { get; private set; }
        
        [Reactive] public Ability Constitution { get; private set; }
        
        [Reactive] public Ability Wisdom { get; private set; }
        
        [Reactive] public Ability Intelligence { get; private set; }
        
        [Reactive] public Ability Charisma { get; private set; }

        [Reactive] public Experience Experience { get; private set; }

        public bool IsDead { [ObservableAsProperty] get; }

        public virtual IObservable<RollEvent> Attack() => Observable.Return(new RollEvent(_roller.Roll<TwentySided>(), Strength.Modifier + Math.DivRem(Level, 2, out var remainder)));

        public void GainExperience() => Experience.Increase(10);

        public void TakeDamaged(int damage) => HitPoints -= damage;
        
        protected virtual void ModifyHitPoint(int level)
        {
            HitPoints += DefaultHitPoints + Constitution.Modifier;
        }

        private void ExecuteLevelUp(int level)
        {
            ModifyHitPoint(level);
        }
    }

    public interface ICharacter<T>
        where T : ICharacterClass
    {
         T Class { get; }
    }

    public abstract class Character<T> : Character, ICharacter<T>
        where T : ICharacterClass
    {
        public T Class { get; protected set; }

        protected Character(IDieRoller roller, IAbilityFactory abilityFactory)
            : base(roller, abilityFactory)
        {
        }

        public override IObservable<RollEvent> Attack() =>
            Observable.Create<RollEvent>(observer =>
            {
                var classAttackModifier = Class.Modifiers.FirstOrDefault(x => x is IAttackModifier)?.Value() ?? 0;
                var rollEvent =
                    new RollEvent(_roller.Roll<TwentySided>(),
                        Strength.Modifier,
                        classAttackModifier,
                        Math.DivRem(Level, 2, out var remainder));
                observer.OnNext(rollEvent);
                observer.OnCompleted();

                return Disposable.Empty;
            });

        protected CompositeDisposable CharacterSubscriptions = new CompositeDisposable();
    }
}