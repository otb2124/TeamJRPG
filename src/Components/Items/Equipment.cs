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
        [JsonIgnore]
        public float PhysicalDMG = 0;
        [JsonIgnore]
        public float MagicalDMG = 0;
        [JsonIgnore]
        public float FireDMG = 0;
        [JsonIgnore]
        public float ColdDMG = 0;
        [JsonIgnore]
        public float LightningDMG = 0;
        [JsonIgnore]
        public float SkinDMGMULTIPLIER = 1;
        [JsonIgnore]
        public float ArmorDMGMULTIPLIER = 1;

        //def
        [JsonIgnore]
        public float PhysicalDEF = 0;
        [JsonIgnore]
        public float MagicalDEF = 0;
        [JsonIgnore]
        public float FireDEF = 0;
        [JsonIgnore]
        public float ColdDEF = 0;
        [JsonIgnore]
        public float LightningDEF = 0;

        //Crit
        [JsonIgnore]
        public float critChance = 0;
    }
}
