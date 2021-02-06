using System.Collections.Generic;
using System.Collections.ObjectModel;
using Evercraft.Classes.Traits;

namespace Evercraft.Classes
{
    public abstract class ClassBase : IClass
    {
        protected ClassBase()
        {
            ClassTraits = new List<ITrait>();
        }

        public IReadOnlyCollection<ITrait> Traits => new ReadOnlyCollection<ITrait>(ClassTraits);

        protected List<ITrait> ClassTraits { get; set; }

        protected virtual void Define(){}

        protected virtual void Adjust<T>(){}

        void IClass.Adjust<T>() => Adjust<T>();

        void IClass.Define() => Define();
    }
}