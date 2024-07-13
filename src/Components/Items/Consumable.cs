

using Newtonsoft.Json;
using System;

namespace TeamJRPG
{

    [Serializable]
    public class Consumable : Item
    {

        public enum ConsumableType { food, potion, throwable }


        [JsonIgnore]
        public ConsumableType consumableType;

        public Consumable(int consumableID, int amount)
        {
            this.amount = amount;
            this.itemID = consumableID;
            type = ItemType.CONSUMABLE;
            IsStackable = true;

            SetConsumable();
        }




        public void SetConsumable()
        {
            switch (itemID)
            {
                case 0:
                    name = "Lesser Healing Potion";
                    consumableType = ConsumableType.potion;
                    description = "Looks tasty.";
                    value = 25;

                    textureID = 0;
                    break;
                case 1:
                    name = "Shiny Apple";
                    consumableType = ConsumableType.food;
                    description = "Looks really tasty.";
                    value = 1;

                    textureID = 1;
                    break;
            }

            SetTexture();
        }
    }
}
