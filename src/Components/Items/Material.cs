using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeamJRPG.Consumable;

namespace TeamJRPG
{

    [Serializable]
    [JsonObject(IsReference = true)]
    public class Material : Item
    {

        public Material(int itemID, int amount)
        {
            this.amount = amount;
            this.itemID = itemID;
            type = ItemType.MATERIAL;
            IsStackable = true;

            SetMaterial();
        }


        public void SetMaterial()
        {
            switch (itemID)
            {
                case 0:
                    name = "Green Leaves";
                    description = "Bunch of green leaves used to cook food.";
                    value = 0;

                    textureID = 0;
                    break;
            }

            SetTexture();
        }

    }
}
