using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Weapon : Item
    {

        public int weaponID;
        public int textureID;


        public Weapon(int weaponID)
        {
            this.weaponID = weaponID;
            type = ItemType.WEAPON;
            SetWeapon();
        }




        public void SetWeapon()
        {
            switch (weaponID)
            {
                case 0:
                    name = "Weapon Slot";
                    description = "";
                    break;
                case 1:
                    name = "Sabre";
                    description = "A common weapon in desert areas.";
                    value = 420;

                    textureID = 0;
                    break;
            }

            SetTexture();
        }

        public void SetTexture()
        {
            if (weaponID != 0)
            {

                texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_WEAPONS][textureID][0];
            }
            else
            {
                texture = Globals.assetSetter.textures[Globals.assetSetter.PLACEHOLDERS][0][0];
            }
        }
    }
}
