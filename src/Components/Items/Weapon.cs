using Newtonsoft.Json;
using System;




namespace TeamJRPG
{
    [Serializable]
    [JsonObject(IsReference = true)]
    public class Weapon : Equipment
    {
        public enum SlotType { oneHanded, twoHanded }

        [JsonIgnore]
        public SlotType slotType;
        public enum WeaponType { sword, mace, axe, staff, shield, bow, crossbow, magicStaff }

        [JsonIgnore]
        public WeaponType weaponType;



        [JsonConstructor]
        public Weapon(int itemID)
        {
            this.itemID = itemID;
            type = ItemType.WEAPON;
            IsStackable = false;

            SetWeapon();
        }




        public void SetWeapon()
        {
            switch (itemID)
            {
                case 0:
                    name = "Nothing";
                    description = "";
                    IsSlot = true;
                    break;
                case 1:
                    name = "Sabre";
                    weaponType = WeaponType.sword;
                    slotType = SlotType.oneHanded;
                    description = "A common weapon in desert areas.";
                    value = 420;

                    PhysicalDMG = 5;

                    textureID = 0;
                    break;
                case 2:
                    name = "Barbarian Axe";
                    weaponType = WeaponType.axe;
                    slotType = SlotType.twoHanded;
                    description = "A common weapon in mountain areas.";
                    value = 540;

                    PhysicalDMG = 10;

                    textureID = 1;
                    break;
                case 3:
                    name = "Wooden Shield";
                    weaponType = WeaponType.shield;
                    slotType = SlotType.oneHanded;
                    description = "A common shield in forest areas.";
                    value = 120;

                    PhysicalDEF = 15;
                    MagicalDEF = -15;
                    FireDEF = 10;
                    LightningDEF = 10;
                    ColdDEF = 10;

                    textureID = 3;
                    break;
                case 4:
                    name = "Rain Bow";
                    weaponType = WeaponType.bow;
                    slotType = SlotType.twoHanded;
                    description = "A common weapon in rainy areas.";
                    value = 145;

                    PhysicalDMG = 15;
                    MagicalDMG = 10;

                    textureID = 2;
                    break;
            }

            SetDMGMultipliers();
            SetTexture();
        }


        public void SetDMGMultipliers()
        {
            switch (weaponType)
            {
                case WeaponType.sword:
                    SkinDMGMULTIPLIER = 1.5f;
                    ArmorDMGMULTIPLIER = 0.5f;
                    break;
                case WeaponType.mace:
                    SkinDMGMULTIPLIER = 0.5f;
                    ArmorDMGMULTIPLIER = 1f;
                    break;
                case WeaponType.axe:
                    SkinDMGMULTIPLIER = 1f;
                    ArmorDMGMULTIPLIER = 1f;
                    break;
                case WeaponType.staff:
                    SkinDMGMULTIPLIER = 1.25f;
                    ArmorDMGMULTIPLIER = 0.75f;
                    break;
                case WeaponType.bow:
                    SkinDMGMULTIPLIER = 1.9f;
                    ArmorDMGMULTIPLIER = 0.1f;
                    break;
                case WeaponType.crossbow:
                    SkinDMGMULTIPLIER = 1.75f;
                    ArmorDMGMULTIPLIER = 0.25f;
                    break;
                default:
                    break;
            }
        }

        
    }
}
