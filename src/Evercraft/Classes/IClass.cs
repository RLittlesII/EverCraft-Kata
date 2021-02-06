using System.Collections.Generic;
using Evercraft.Classes.Traits;

namespace Evercraft.Classes
{
    public interface IClass
    {
        void Define();

        void Adjust<T>() where T : ITrait;

        IReadOnlyCollection<ITrait> Traits { get; }
    }
}