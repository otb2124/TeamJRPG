using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Armor : Item
    {


        public Armor(string name) : base(name)
        {
            texture = Globals.assetSetter.textures[7][2][0];
            type = ItemType.WEAPON;
        }
    }
}
