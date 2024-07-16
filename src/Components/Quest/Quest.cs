using Newtonsoft.Json;
using System;


namespace TeamJRPG
{
    [Serializable]
    public class Quest
    {

        [JsonIgnore]
        public string name;
        [JsonIgnore]
        public string description;
        [JsonIgnore]
        public Item reward;

        
        public int id;
        public bool isCompleted;

        public enum QuestType { primary, secondary, additional };
        [JsonIgnore]
        public QuestType type;


        [JsonConstructor]
        public Quest(int id, bool isCompleted)
        {
            this.id = id;
            this.isCompleted = isCompleted;
            SetQuest();
        }



        public void SetQuest()
        {
            switch (id)
            {
                case 0:
                    name = "The Rescue";
                    description = "Dear Whoever,\n\nMy son has got into goblin's trap in a dark scary dungeon. Please help.\nThe reward will be great for sure\n\nThank you.";
                    type = QuestType.primary;
                    reward = new Consumable(0, 5);
                    break;
                case 1:
                    name = "Beat Up";
                    description = "Dear Whoever,\n\nBeat up Johnny in Riverwood.\nThe reward will be ok\n\nJamie.";
                    type = QuestType.secondary;
                    reward = new Consumable(1, 1);
                    break;
                case 2:
                    name = "Necromancy among us, Necromancers among us";
                    description = "Dear Sir or Madam,\n\nOur precioues city, Rookridge, thrived withot filthy necromancer mages that have inhabbited a nearby cavern from recent. Get rid of them.\nThe reward will be ok\n\nJamie.";
                    type = QuestType.additional;
                    reward = new Weapon(1);
                    break;
            }
        }
    }
}
