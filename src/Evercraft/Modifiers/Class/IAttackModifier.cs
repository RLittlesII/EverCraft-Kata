using Evercraft.Classes;

namespace Evercraft.Modifiers.Class
{
    public interface IAttackModifier<TClass> : IModifier<TClass>
        where TClass : ICharacterClass { }
}