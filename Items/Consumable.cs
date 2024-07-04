using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Consumable : Item
    {


        public Consumable(string name) : base(name)
        {
            texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_CONSUMABLES][0][0];
            type = ItemType.CONSUMABLE;
        }
    }
}
