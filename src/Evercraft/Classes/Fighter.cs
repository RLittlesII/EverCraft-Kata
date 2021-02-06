using System.Collections.Generic;
using Evercraft.Classes.Traits;

namespace Evercraft.Classes
{
    public class Fighter : ClassBase
    {
        protected override void Define()
        {
            ClassTraits = new List<ITrait>
            {
                new AttackTrait(),
                new HitPointTrait()
            };
        }
    }
}