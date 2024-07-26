using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class BattleUsable
    {
        public Consumable consumable;
        public Skill skill;
        public Action action;



        public BattleUsable(Consumable consumable)
        {
            this.action = null;
            this.skill = null;
            this.consumable = consumable;
        }


        public BattleUsable(Skill skill)
        {
            this.action = null;
            this.consumable = null;
            this.skill = skill;
        }

        public BattleUsable(Action action)
        {
            this.action = action;
            this.consumable = null;
            this.skill = null;
        }
    }
}
