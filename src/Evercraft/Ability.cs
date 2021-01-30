using System;
using System.Collections.Generic;

namespace Evercraft
{
    public abstract class Ability
    {
        private static readonly Dictionary<int, int> Table = new Dictionary<int, int>()
        {
            {1, -5},
            {2, -4},
            {3, -4},
            {4, -3},
            {5, -3},
            {6, -2},
            {7, -2},
            {8, -1},
            {9, -1},
            {10, 0},
            {11, 0},
            {12, 1},
            {13, 1},
            {14, 2},
            {15, 2},
            {16, 3},
            {17, 3},
            {18, 4},
            {19, 4},
            {20, 5},
        };

        protected Ability(int score = 10)
        {
            Score = score;
        }

        public int Score { get; }

        public int Modifier => Table[Score];
    }

    public class Strength : Ability
    {
        public Strength(int score)
            : base(score)
        {
        }
    }

    public class Dexterity : Ability
    {
        public Dexterity(int score)
            : base(score)
        {
        }
    }

    public class Constitution : Ability
    {
        public Constitution(int score)
            : base(score)
        {
        }
    }

    public class Wisdom : Ability
    {
        public Wisdom(int score)
            : base(score)
        {
        }
    }

    public class Intelligence : Ability
    {
        public Intelligence(int score)
            : base(score)
        {
        }
    }

    public class Charisma : Ability
    {
        public Charisma(int score)
            : base(score)
        {
        }
    }

    public interface IAbilityFactory
    {
        T Create<T>(int score)
            where T : Ability;
    }

    public class AbilityFactory : IAbilityFactory
    {
        public T Create<T>(int score = 10) where T : Ability => (T) Activator.CreateInstance(typeof(T), score);
    }
}