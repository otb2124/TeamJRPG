using Newtonsoft.Json;
using System;


namespace TeamJRPG
{


    [Serializable]
    public class Skill
    {

        public enum SkillType { attacking, defending, boosting }
        [JsonIgnore]
        public SkillType type { get; set; }
        public enum CastType { self, singleTarget, groupTarget, allEnemy, allAlly, all }
        [JsonIgnore]
        public CastType castType { get; set; }


        public int skillId;

        [JsonIgnore]
        public string name;
        [JsonIgnore]
        public string description;

        public int skillLevel;


        [JsonConstructor]
        public Skill(int skillId)
        {
            this.skillId = skillId;
            this.skillLevel = 1;
            SetSkill();
        }



        public void SetSkill()
        {
            switch (skillId)
            {
                case 0:
                    name = "Common Hit";
                    description = "Character hits a chosen enemy with his might and strength.";
                    type = SkillType.attacking;
                    castType = CastType.singleTarget;

                    break;
                case 1:
                    name = "Block";
                    description = "Character gethers his all power and endurance to block the next incomming hit completely or partly.";
                    type = SkillType.defending;
                    castType = CastType.self;

                    break;
                case 2:
                    name = "Heal";
                    description = "Character restores a part of health for chosen ally or enemy.";
                    type = SkillType.boosting;
                    castType = CastType.singleTarget;

                    break;
            }
        }
    }
}
