using Newtonsoft.Json;
using System;


namespace TeamJRPG
{


    [Serializable]
    public class Skill
    {

        public int skillId;

        [JsonIgnore]
        public string name;
        [JsonIgnore]
        public string description;

        [JsonIgnore]
        public Cast[] casts;

        public int skillLevel;


        [JsonConstructor]
        public Skill(int skillId)
        {
            this.skillId = skillId;
            SetSkill();
        }



        public void SetSkill()
        {
            switch (skillId)
            {
                case 0:
                    name = "Common Hit";
                    description = "Character hits a chosen enemy with their might and strength.";

                    casts = new Cast[1];
                    casts[0] = new Cast(Cast.CastTargetType.anySelf, 1, Cast.CastType.hp, -90);

                    break;
                case 1:
                    name = "Block";
                    description = "Character gethers their all power and endurance to block the next incomming hit completely or partly.";

                    casts = new Cast[1];
                    casts[0] = new Cast(Cast.CastTargetType.self, 1, Cast.CastType.block, 10);


                    break;
                case 2:
                    name = "Heal";
                    description = "Character restores 10 health points for two characters.";

                    casts = new Cast[1];
                    casts[0] = new Cast(Cast.CastTargetType.allySelf, 2, Cast.CastType.hp, 1);

                    break;
            }
        }
    }
}
