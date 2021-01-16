using System;
using System.Collections.Generic;

namespace Evercraft.Dice
{
    public class DieRoller : IDieRoller
    {
        private static readonly Dictionary<Type, IDie> Dice = new Dictionary<Type, IDie>
        {
            {typeof(TwentySided), new TwentySided()}
        };

        public int Roll<T>() where T : IDie => Dice[typeof(T)].Roll();
    }
}