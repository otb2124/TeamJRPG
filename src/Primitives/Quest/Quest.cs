using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Quest
    {


        public string name;
        public string description;
        public Item reward;
        public int id;

        public bool isCompleted;
        public enum QuestType { primary, secondary, additional };
        public QuestType type;

        public Quest(int id)
        {
            this.id = id;
            isCompleted = false;
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
