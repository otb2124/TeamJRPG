using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Valuable : Item
    {


        public Valuable(string name) : base(name)
        {
            texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_VALUABLES][0][0];
            type = ItemType.VALUEABLE;
        }
    }
}
