using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{


    [Serializable]
    [JsonObject(IsReference = true)]
    public class Valuable : Item
    {

        public Valuable(int itemID, int amount)
        {
            this.amount = amount;
            this.itemID = itemID;
            type = ItemType.VALUEABLE;
            IsStackable = true;

            SetValuable();
        }



        public void SetValuable()
        {
            switch (itemID)
            {
                case 0:
                    name = "Diamond";
                    description = "Shiny.";
                    value = 300;

                    textureID = 0;
                    break;
            }

            SetTexture();
        }
    }
}
