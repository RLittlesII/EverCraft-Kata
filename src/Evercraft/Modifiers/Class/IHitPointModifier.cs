using Evercraft.Classes;

namespace Evercraft.Modifiers.Class
{
    public interface IHitPointModifier<TClass> : IModifier<TClass>
        where TClass : ICharacterClass { }
}