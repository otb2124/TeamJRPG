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

        public int armorID;
        public int textureID;

        public Armor(int armorID)
        {
            this.armorID = armorID;
            type = ItemType.ARMOR;

            SetArmor();
        }


        public void SetArmor()
        {
            switch (armorID)
            {
                case 0:
                    name = "Armor Slot";
                    description = "";
                    break;
                case 1:
                    name = "Black Cotton Coat";
                    armorType = ArmorType.chestplate;
                    description = "A regular cotton black coat.\nWorn by strongest knights.";
                    value = 150;

                    textureID = 0;
                    break;
            }

            SetTexture();
        }

        public void SetTexture()
        {
            if (armorID != 0)
            {

                texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_ARMOR][textureID][0];
            }
            else
            {
                texture = Globals.assetSetter.textures[Globals.assetSetter.PLACEHOLDERS][0][0];
            }
        }
    }
}
