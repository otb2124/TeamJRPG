using Newtonsoft.Json;
using System;

namespace TeamJRPG
{

    [Serializable]
    [JsonObject(IsReference = true)]
    public class Consumable : Item
    {

        public enum ConsumableType { food, potion, throwable }


        [JsonIgnore]
        public ConsumableType consumableType;




        [JsonConstructor]
        public Consumable(int amount, int itemID)
        {
            this.amount = amount;
            this.itemID = itemID;
            this.type = ItemType.CONSUMABLE;
            this.IsStackable = true;

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
