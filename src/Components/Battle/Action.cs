using Newtonsoft.Json;


namespace TeamJRPG
{
    public class Action
    {



        [JsonIgnore]
        public Cast cast;

        [JsonIgnore]
        public string text;

        public int actionType;


        [JsonConstructor]
        public Action(int actionType) 
        {
            this.actionType = actionType;
            SetAction();
        }


        
        public void SetAction()
        {
            switch (actionType)
            {
                case 0:
                    cast = new Cast(Cast.CastTargetType.any, 1, Cast.CastType.talk);
                    text = "Talk";
                    break;
                case 1:
                    cast = new Cast(Cast.CastTargetType.any, 1, Cast.CastType.leave);
                    text = "Persuade to Leave";
                    break;
                case 2:
                    cast = new Cast(Cast.CastTargetType.enemy, 1, Cast.CastType.join);
                    text = "Persuade to Join";
                    break;
            }
        }
    }
}
