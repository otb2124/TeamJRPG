using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeamJRPG.Armor;

namespace TeamJRPG
{
    public class Consumable : Item
    {

        public enum ConsumableType { food, potion, throwable }
        public ConsumableType consumableType;
        public int consumableID;
        public int textureID;

        public Consumable(int consumableID)
        {
            this.consumableID = consumableID;
            type = ItemType.CONSUMABLE;

            SetConsumable();
        }




        public void SetConsumable()
        {
            switch (consumableID)
            {
                case 0:
                    name = "Lesser Healing Potion";
                    consumableType = ConsumableType.potion;
                    description = "Looks tasty.";
                    value = 25;

                    textureID = 0;
                    break;
            }

            SetTexture();
        }

        public void SetTexture()
        {
            texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_CONSUMABLES][textureID][0];
        }
    }
}
