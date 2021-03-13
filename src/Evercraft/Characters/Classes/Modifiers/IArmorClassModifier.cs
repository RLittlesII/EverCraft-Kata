namespace Evercraft.Characters.Classes.Modifiers
{
    public interface IArmorClassModifier<TClass> : IClassModifier
        where TClass : ICharacterClass { }
}