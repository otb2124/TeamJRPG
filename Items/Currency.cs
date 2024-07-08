using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Currency : Item
    {


        public Currency()
        {
            texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_CURRENCY][0][0];
            type = ItemType.CURRENCY;
            IsStackable = true;
            value = 1;
        }
    }
}
