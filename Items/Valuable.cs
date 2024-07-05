using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Valuable : Item
    {

        public int valuableID;
        public int textureID;

        public Valuable(int valuableID)
        {
            this.valuableID = valuableID;
            type = ItemType.VALUEABLE;
            SetValuable();
        }



        public void SetValuable()
        {
            switch (valuableID)
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

        public void SetTexture()
        {
            texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_VALUABLES][textureID][0];
        }
    }
}
