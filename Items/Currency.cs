using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Currency : Item
    {


        public Currency() : base("Dinar")
        {
            texture = Globals.assetSetter.textures[7][1][0];
            type = ItemType.CURRENCY;
        }
    }
}
