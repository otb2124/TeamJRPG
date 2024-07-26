using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Cast
    {
        public enum CastType { hp, mana, block,
                talk, leave, join
        };

        public CastType castType;

        public float amount;

        public int targetAmount;

        public enum CastTargetType { self, enemy, ally, allySelf, any, anySelf }
        public CastTargetType castTargetType { get; set; }


        public Cast(CastTargetType castType, int targetAmount, CastType attribute, float amount) 
        {
            this.castType = attribute;
            this.amount = amount;
            this.targetAmount = targetAmount;
            this.castTargetType = castType;
        }

        public Cast(CastTargetType castType, int targetAmount, CastType attribute)
        {
            this.castType = attribute;
            this.amount = 1;
            this.targetAmount = targetAmount;
            this.castTargetType = castType;
        }
    }
}
