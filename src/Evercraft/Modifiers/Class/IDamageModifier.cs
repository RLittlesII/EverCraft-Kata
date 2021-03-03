using Evercraft.Classes;

namespace Evercraft.Modifiers.Class
{
    public interface IDamageModifier<TClass> : IModifier<TClass>
        where TClass : ICharacterClass { }
}