using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;


namespace TeamJRPG
{

    [Serializable]
    [JsonObject(IsReference = true)]
    public class Item
    {
        [JsonIgnore]
        public int value;
        [JsonIgnore]
        public string name;
        [JsonIgnore]
        public string description;

        [JsonIgnore]
        public Sprite sprite;
        [JsonIgnore]
        public int textureID;

        public enum ItemType { WEAPON, ARMOR, CONSUMABLE, MATERIAL, VALUEABLE, QUEST, CURRENCY}
        [JsonIgnore]
        public ItemType type;

        [JsonIgnore]
        public bool IsStackable;
        public int amount = 1;


        public int itemID;

        [JsonIgnore]
        public bool IsSlot;

        public Item() 
        {
            this.sprite = Globals.textureManager.GetSprite(TextureManager.SheetCategory.placeholders, 0, new Vector2(32, 0), new Vector2(0, 0));
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
                sprite = Globals.textureManager.GetSprite(TextureManager.SheetCategory.placeholders, 0, new Vector2(GetPlaceHolderID() * 32, 32*2), new Vector2(32, 32));
            }
            else 
            {
                sprite = Globals.textureManager.GetSprite(TextureManager.SheetCategory.items, GetAssettypeByItemtype(), new Vector2(0, textureID * 32), new Vector2(32, 32));
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
