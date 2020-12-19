using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Evercraft
{
    public class DiceRoller : IDiceRoller
    {
        private readonly Dictionary<Type, IDie> _dice = new Dictionary<Type, IDie>
        {
            {typeof(TwentySided), new TwentySided()}
        };
        public int Roll<T>() where T : IDie => _dice[typeof(T)].Roll();
    }
}