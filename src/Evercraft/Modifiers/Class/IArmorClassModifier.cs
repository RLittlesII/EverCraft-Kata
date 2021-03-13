using Evercraft.Classes;

namespace Evercraft.Modifiers.Class
{
    public interface IArmorClassModifier<TClass> : IClassModifier
        where TClass : ICharacterClass { }
}