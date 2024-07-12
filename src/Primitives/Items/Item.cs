﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class Item
    {
        public int value;
        public string name;
        public string description;
        public Sprite sprite;
        public int textureID;

        public enum ItemType { WEAPON, ARMOR, CONSUMABLE, MATERIAL, VALUEABLE, QUEST, CURRENCY}
        public ItemType type;


        public bool IsStackable;
        public int amount = 1;


        public int itemID;
        public bool IsSlot;

        public Item() 
        {
            this.sprite = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.placeholders, 0, new Vector2(32, 0), new Vector2(0, 0));
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
                sprite = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.placeholders, 0, new Vector2(GetPlaceHolderID() * 32, 32*2), new Vector2(32, 32));
            }
            else 
            {
                sprite = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.items, GetAssettypeByItemtype(), new Vector2(0, textureID * 32), new Vector2(32, 32));
            }
        }


        public int GetAssettypeByItemtype()
        {
            int type = 0;

            switch (this.type)
            {
                case ItemType.WEAPON:
                    type = 0;
                    break;
                case ItemType.ARMOR:
                    type = 1;
                    break;
                case ItemType.CONSUMABLE:
                    type = 2;
                    break;
                case ItemType.MATERIAL:
                    type = 3;
                    break;
                case ItemType.VALUEABLE:
                    type = 4;
                    break;
                case ItemType.QUEST:
                    type = 5;
                    break;
                case ItemType.CURRENCY:
                    type = 6;
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
                    placeholderID = 8;
                    break;
                case ItemType.ARMOR:

                    switch (((Armor)this).slotType)
                    {
                        case Armor.SlotType.helmet:
                            placeholderID = 0;
                            break;
                        case Armor.SlotType.chestplate:
                            placeholderID = 1;
                            break;
                        case Armor.SlotType.boots:
                            placeholderID = 2;
                            break;
                        case Armor.SlotType.gloves:
                            placeholderID = 3;
                            break;
                        case Armor.SlotType.cape:
                            placeholderID = 4;
                            break;
                        case Armor.SlotType.belt:
                            placeholderID = 5;
                            break;
                        case Armor.SlotType.necklace:
                            placeholderID = 6;
                            break;
                        case Armor.SlotType.ring:
                            placeholderID = 7;
                            break;
                    }
                    break;
            }

            return placeholderID;
        }
    }
}