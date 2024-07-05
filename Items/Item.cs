using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class Item
    {
        public int value;
        public string name;
        public string description;
        public Texture2D texture;
        public enum ItemType { WEAPON, ARMOR, CONSUMABLE, MATERIAL, VALUEABLE, QUEST, CURRENCY}
        public ItemType type;

        public Item() 
        {
            this.texture = Globals.assetSetter.textures[Globals.assetSetter.PLACEHOLDERS][1][0];
            this.name = "BLANK_NAME";
            this.description = "BLANK_DESCRIPTION";
            this.value = -4;
        }
    }
}
