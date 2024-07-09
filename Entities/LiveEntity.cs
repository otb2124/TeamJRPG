using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class LiveEntity : Entity
    {

        public Weapon weapon1;
        public Weapon weapon2;
        public Armor[] armor;
        public static readonly int CHESTPLATE = 1, HELMET = 0, BOOTS = 2, GLOVES = 3, CAPE = 4, NECKLACE = 6, BELT = 5, RING1 = 7, RING2 = 8;




        //exp stats
        public int level = 0;
        public int currentExp = 0;
        public int skillPoints = 0;


        //battle attributes
        public int strength = 0;
        public int dexterity = 0;
        public int wisdom = 0;
        public int currentMana = 0;
        public int maxMana = 100;
        public int currentHP = 0;
        public int maxHP = 100;

        //fightingSkills
        public float onehandedSkill = 0;
        public float twohandedSkill = 0;
        public float bowSkill = 0;
        public float crossbowSkill = 0;
        public float magicSkill = 0;


        public LiveEntity(Vector2 position) : base(position)
        {

        }




        public int GetExpToNextLevel()
        {
            return (level + 1) * 1000;
        }


        public float GetTotalPhysicalDamage()
        {
            float totalArmorDMG = 0;

            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDMG += armor[i].PhysicalDMG;
            }

            return weapon1.PhysicalDMG + weapon2.PhysicalDMG + totalArmorDMG;
        }


        public float GetTotalMagicalDamage()
        {
            float totalArmorDMG = 0;

            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDMG += armor[i].MagicalDMG;
            }

            return weapon1.MagicalDMG + weapon2.MagicalDMG + totalArmorDMG;
        }


        public float GetTotalFireDamage()
        {
            float totalArmorDMG = 0;

            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDMG += armor[i].FireDMG;
            }

            return weapon1.FireDMG + weapon2.FireDMG + totalArmorDMG;
        }



        public float GetTotalColdDamage()
        {
            float totalArmorDMG = 0;

            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDMG += armor[i].ColdDMG;
            }

            return weapon1.ColdDMG + weapon2.ColdDMG + totalArmorDMG;

        }


        public float GetTotalLightningDamage()
        {
            float totalArmorDMG = 0;

            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDMG += armor[i].LightningDMG;
            }

            return weapon1.LightningDMG + weapon2.LightningDMG + totalArmorDMG;
        }



        public float GetTotalPhysicalDefense()
        {
            float totalArmorDEF = 0;

            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDEF += armor[i].PhysicalDEF;
            }

            return weapon1.PhysicalDEF + weapon2.PhysicalDEF + totalArmorDEF;
        }


        public float GetTotalMagicalDefense()
        {
            float totalArmorDEF = 0;

            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDEF += armor[i].MagicalDEF;
            }

            return weapon1.MagicalDEF + weapon2.MagicalDEF + totalArmorDEF;
        }


        public float GetTotalFireDefense()
        {
            float totalArmorDEF = 0;

            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDEF += armor[i].FireDEF;
            }

            return weapon1.FireDEF + weapon2.FireDEF + totalArmorDEF;
        }



        public float GetTotalColdDefense()
        {
            float totalArmorDEF = 0;

            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDEF += armor[i].ColdDEF;
            }

            return weapon1.ColdDEF + weapon2.ColdDEF + totalArmorDEF;

        }


        public float GetTotalLightningDefense()
        {
            float totalArmorDEF = 0;

            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDEF += armor[i].LightningDEF;
            }

            return weapon1.LightningDEF + weapon2.LightningDEF + totalArmorDEF;
        }



        public float GetAvgCritChance()
        {
            float totalArmorCrit = 0;

            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorCrit += armor[i].critChance;
            }

            return (weapon1.critChance + weapon2.critChance + totalArmorCrit) / (armor.Length + 2);
        }

    }
}
