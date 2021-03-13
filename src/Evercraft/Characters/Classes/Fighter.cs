using System.Collections.Generic;
using Evercraft.Characters.Classes.Modifiers;

namespace Evercraft.Characters.Classes
{
    public class Fighter : ICharacterClass
    {
        public IEnumerable<IClassModifier> Modifiers { get; set; }
    }
}