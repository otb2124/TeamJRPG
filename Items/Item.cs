using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class Item
    {
        public int value;
        public string name;
        public string description;
        public Texture2D texture;
        public int textureID;

        public enum ItemType { WEAPON, ARMOR, CONSUMABLE, MATERIAL, VALUEABLE, QUEST, CURRENCY}
        public ItemType type;


        public bool IsStackable;
        public int amount = 1;


        public int itemID;
        public bool IsSlot;

        public Item() 
        {
            this.texture = Globals.assetSetter.textures[Globals.assetSetter.PLACEHOLDERS][1][0];
            this.name = "BLANK_NAME";
            this.description = "BLANK_DESCRIPTION";
            this.value = -4;

            IsStackable = false;
            IsSlot = false;
        }



        public void SetTexture()
        {
            if ((type == ItemType.WEAPON || type == ItemType.ARMOR) && itemID == 0)
            {
                texture = Globals.assetSetter.textures[Globals.assetSetter.PLACEHOLDERS][GetPlaceHolderID()][0];
            }
            else 
            {
                texture = Globals.assetSetter.textures[GetAssettypeByItemtype()][textureID][0];
            }
        }


        public int GetAssettypeByItemtype()
        {
            int type = 0;

            switch (this.type)
            {
                case ItemType.WEAPON:
                    type = Globals.assetSetter.ITEMS_WEAPONS;
                    break;
                case ItemType.ARMOR:
                    type = Globals.assetSetter.ITEMS_ARMOR;
                    break;
                case ItemType.CONSUMABLE:
                    type = Globals.assetSetter.ITEMS_CONSUMABLES;
                    break;
                case ItemType.MATERIAL:
                    type = Globals.assetSetter.ITEMS_MATERIALS;
                    break;
                case ItemType.VALUEABLE:
                    type = Globals.assetSetter.ITEMS_VALUABLES;
                    break;
                case ItemType.QUEST:
                    type = Globals.assetSetter.ITEMS_QUESTITEMS;
                    break;
                case ItemType.CURRENCY:
                    type = Globals.assetSetter.ITEMS_CURRENCY;
                    break;
            }

            return type;
        }

        public int GetPlaceHolderID()
        {
            int placeholderID = 0;

            switch (this.type)
            {
                case ItemType.WEAPON:
                    placeholderID = 10;
                    break;
                case ItemType.ARMOR:

                    switch (((Armor)this).slotType)
                    {
                        case Armor.SlotType.helmet:
                            placeholderID = 2;
                            break;
                        case Armor.SlotType.chestplate:
                            placeholderID = 3;
                            break;
                        case Armor.SlotType.boots:
                            placeholderID = 4;
                            break;
                        case Armor.SlotType.gloves:
                            placeholderID = 5;
                            break;
                        case Armor.SlotType.cape:
                            placeholderID = 6;
                            break;
                        case Armor.SlotType.belt:
                            placeholderID = 7;
                            break;
                        case Armor.SlotType.necklace:
                            placeholderID = 8;
                            break;
                        case Armor.SlotType.ring:
                            placeholderID = 9;
                            break;
                    }
                    break;
            }

            return placeholderID;
        }
    }
}
