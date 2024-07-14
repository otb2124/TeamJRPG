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
    public class Equipment : Item
    {
        

        //damage
        public float PhysicalDMG = 0;
        public float MagicalDMG = 0;
        public float FireDMG = 0;
        public float ColdDMG = 0;
        public float LightningDMG = 0;
        public float SkinDMGMULTIPLIER = 1;
        public float ArmorDMGMULTIPLIER = 1;

        //def
        public float PhysicalDEF = 0;
        public float MagicalDEF = 0;
        public float FireDEF = 0;
        public float ColdDEF = 0;
        public float LightningDEF = 0;

        //Crit
        public float critChance = 0;
    }
}
