using System.Collections.Generic;
using Evercraft.Characters.Classes.Modifiers;

namespace Evercraft.Characters.Classes
{
    // public interface IClass
    // {
    //     void Define();
    //
    //     void Adjust<T>() where T : ITrait;
    //
    //     IReadOnlyCollection<ITrait> Traits { get; }
    // }

    public interface ICharacterClass
    {
        // Modify a Characters Ability
        // Modify a Characters Attacks
        // Modify a Characters Critical damage
        // Modify a Characters Ability, Attack or Critical base on Targets Class
        // Abilities
        // Limitations
        
        IEnumerable<IClassModifier> Modifiers  { get; set; }
    }
}