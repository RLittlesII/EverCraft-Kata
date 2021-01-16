using System;

namespace Evercraft.Dice
{
    public abstract class DieBase : IDie
    {
        protected static readonly Random NumberGenerator = new Random();

        protected abstract int Roll();

        int IDie.Roll() => Roll();
    }
}