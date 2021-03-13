using System;

namespace Evercraft.Characters.Abilities
{
    public class AbilityFactory : IAbilityFactory
    {
        public T Create<T>(int score = 10) where T : Ability => (T) Activator.CreateInstance(typeof(T), score);
    }
}