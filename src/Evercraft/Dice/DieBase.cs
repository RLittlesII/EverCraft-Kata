using System;

namespace Evercraft
{
    public abstract class DieBase : IDie
    {
        protected static readonly Random NumberGenerator = new Random();

        protected abstract int Roll();

        int IDie.Roll() => Roll();
    }
}