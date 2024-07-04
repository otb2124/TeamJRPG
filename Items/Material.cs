using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Material : Item
    {


        public Material(string name) : base(name)
        {
            texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_MATERIALS][0][0];
            type = ItemType.MATERIAL;
        }
    }
}
