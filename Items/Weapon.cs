using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Weapon : Item
    {


        public Weapon(string name) : base(name) 
        {
            texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_WEAPONS][0][0];
            type = ItemType.WEAPON;
        }
    }
}
