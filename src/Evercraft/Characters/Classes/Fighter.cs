using System.Collections.Generic;
using Evercraft.Characters.Classes.Modifiers;

namespace Evercraft.Characters.Classes
{
    public class Fighter : ICharacterClass
    {
        public Fighter(FighterCharacter fighterCharacter)
        {
            Modifiers = new List<IClassModifier>{new AttackModifier(fighterCharacter)};
        }

        public IEnumerable<IClassModifier> Modifiers { get; set; }
        
        
        private class AttackModifier : IAttackModifier
        {
            private readonly FighterCharacter _fighterCharacter;

            public AttackModifier(FighterCharacter fighterCharacter)
            {
                _fighterCharacter = fighterCharacter;
            }

            public int Value() => 1 * _fighterCharacter.Level;
        }
    }
}