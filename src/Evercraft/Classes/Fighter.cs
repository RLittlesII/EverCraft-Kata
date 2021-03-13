
using System.Collections.Generic;
using Evercraft.Modifiers.Class;

namespace Evercraft.Classes
{
    public class Fighter : ICharacterClass
    {
        public IEnumerable<IClassModifier> Modifiers { get; set; }
    }
}