using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Armor : Item
    {

        public enum ArmorType { helmet, chestplate, boots, gloves, cape, belt, necklace, ring }
        public ArmorType armorType;

        public Armor(string name) : base(name)
        {
            texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_ARMOR][0][0];
            type = ItemType.ARMOR;
            armorType = ArmorType.helmet;
        }
    }
}
