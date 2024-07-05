using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeamJRPG.Consumable;

namespace TeamJRPG
{
    public class Material : Item
    {


        public int materialID;
        public int textureID;

        public Material(int materialID)
        {
            this.materialID = materialID;
            type = ItemType.MATERIAL;

            SetMaterial();
        }


        public void SetMaterial()
        {
            switch (materialID)
            {
                case 0:
                    name = "Green Leaves";
                    description = "Bunch of green leaves used to cook food.";
                    value = 2;

                    textureID = 0;
                    break;
            }

            SetTexture();
        }

        public void SetTexture()
        {
            texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_MATERIALS][textureID][0];
        }
    }
}
