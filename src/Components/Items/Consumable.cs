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

        [JsonIgnore]
        public Cast[] casts;


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
                    description = "Restores 25 health points.";
                    value = 25;

                    casts = new Cast[1];
                    casts[0] = new Cast(Cast.CastTargetType.allySelf, 1, Cast.CastType.hp, 25);


                    textureID = 0;
                    break;
                case 1:
                    name = "Shiny Apple";
                    consumableType = ConsumableType.food;
                    description = "Restores 5 health points.";
                    value = 1;

                    casts = new Cast[1];
                    casts[0] = new Cast(Cast.CastTargetType.allySelf, 1, Cast.CastType.hp, 5);

                    textureID = 1;
                    break;
            }

            SetTexture();
        }
    }
}
