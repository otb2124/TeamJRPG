using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class Item
    {
        public int value;
        public string name;
        public Texture2D texture;
        public enum ItemType { WEAPON, ARMOR, CONSUMABLE, MATERIAL, VALUEABLE, QUEST, CURRENCY}
        public ItemType type;

        public Item(string name) 
        {
            this.name = name;
            this.value = 1;
        }
    }
}
