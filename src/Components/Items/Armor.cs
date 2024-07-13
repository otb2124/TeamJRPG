using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Armor : Equipment
    {

        public enum SlotType { helmet, chestplate, boots, gloves, cape, belt, necklace, ring }
        public SlotType slotType;

        public Armor(int itemID)
        {
            this.itemID = itemID;
            type = ItemType.ARMOR;
            IsStackable = false;

            SetArmor();
        }


        public void SetArmor()
        {
            switch (itemID)
            {
                case 0:
                    name = "Nothing";
                    description = "";
                    IsSlot = true;
                    break;
                case 1:
                    name = "Barbarian Helmet";
                    slotType = SlotType.helmet;
                    description = "Scary metal helmet.\nWorn by strongest knights.";
                    value = 50;

                    PhysicalDEF = 10;
                    MagicalDEF = 0;
                    FireDEF = 0;
                    LightningDEF = 0;
                    ColdDEF = 0;

                    textureID = 0;
                    break;
                case 2:
                    name = "Mage's Coat";
                    slotType = SlotType.chestplate;
                    description = "Mysical cotton black coat.\nWorn by strongest knights.";
                    value = 150;

                    PhysicalDEF = 15;
                    MagicalDEF = 15;
                    FireDEF = 10;
                    LightningDEF = 10;
                    ColdDEF = 10;

                    textureID = 1;
                    break;
                case 3:
                    name = "Knight's Sabatons";
                    slotType = SlotType.boots;
                    description = "Strong metal sabatons.\nWorn by strongest knights.";
                    value = 90;

                    PhysicalDEF = 25;
                    MagicalDEF = 5;
                    FireDEF = 5;
                    LightningDEF = 0;
                    ColdDEF = 0;

                    textureID = 2;
                    break;
                case 4:
                    name = "Thief's Gloves";
                    slotType = SlotType.gloves;
                    description = "Light skin gloves.\nWorn by strongest knights.";
                    value = 40;

                    PhysicalDEF = 5;
                    MagicalDEF = 5;
                    FireDEF = 0;
                    LightningDEF = 0;
                    ColdDEF = 5;

                    textureID = 3;
                    break;
                case 5:
                    name = "Hunter's Cape";
                    slotType = SlotType.cape;
                    description = "Half-fur cape.\nWorn by strongest knights.";
                    value = 60;

                    PhysicalDEF = 1;
                    MagicalDEF = 5;
                    FireDEF = 5;
                    LightningDEF = 0;
                    ColdDEF = 10;

                    textureID = 4;
                    break;
                case 6:
                    name = "Blacksmith's belt";
                    slotType = SlotType.belt;
                    description = "Old skin belt.\nWorn by strongest knights.";
                    value = 30;

                    PhysicalDEF = 1;
                    MagicalDEF = 5;
                    FireDEF = 15;
                    LightningDEF = 0;
                    ColdDEF = 10;

                    textureID = 5;
                    break;
                case 7:
                    name = "Nobile's necklace";
                    slotType = SlotType.necklace;
                    description = "Shiny golden necklace.\nWorn by strongest knights.";
                    value = 300;

                    PhysicalDEF = 0;
                    MagicalDEF = 25;
                    FireDEF = 25;
                    LightningDEF = 25;
                    ColdDEF = 0;

                    textureID = 6;
                    break;
                case 8:
                    name = "Father's ring";
                    slotType = SlotType.ring;
                    description = "Old golden ring.\nWorn by strongest knights.";
                    value = 210;

                    PhysicalDEF = 0;
                    MagicalDEF = 0;
                    FireDEF = 0;
                    LightningDEF = 0;
                    ColdDEF = 25;

                    textureID = 7;
                    break;
                case 9:
                    name = "Uncle's ring";
                    slotType = SlotType.ring;
                    description = "Old golden ring.\nWorn by strongest knights.";
                    value = 200;

                    PhysicalDEF = 0;
                    MagicalDEF = 0;
                    FireDEF = 0;
                    LightningDEF = 25;
                    ColdDEF = 0;

                    textureID = 7;
                    break;
            }

            SetTexture();
        }

    }
}
