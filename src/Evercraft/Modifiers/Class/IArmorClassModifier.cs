using Evercraft.Classes;

namespace Evercraft.Modifiers.Class
{
    public interface IArmorClassModifier<TClass> : IModifier<TClass>
        where TClass : ICharacterClass { }
}