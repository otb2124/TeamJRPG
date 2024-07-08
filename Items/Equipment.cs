using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Equipment : Item
    {
        

        //damage
        public float PhysicalDMG = 0;
        public float MagicalDMG = 0;
        public float SkinDMGMULTIPLIER = 1;
        public float ArmorDMGMULTIPLIER = 1;

        //def
        public float PhysicalDEF = 0;
        public float MagicalDEF = 0;
        public float FireDEF = 0;
        public float ColdDEF = 0;
        public float LightningDEF = 0;


    }
}
