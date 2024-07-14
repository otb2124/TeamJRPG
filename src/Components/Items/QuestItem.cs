using Newtonsoft.Json;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{

    [Serializable]
    [JsonObject(IsReference = true)]
    public class QuestItem : Item
    {


        public QuestItem(int itemID)
        {
            this.itemID = itemID;
            type = ItemType.QUEST;
            IsStackable = false;

            SetQuestItem();
        }




        public void SetQuestItem()
        {
            switch (itemID)
            {
                case 0:
                    name = "Broken Sword of Astora";
                    description = "Once a great blade.\nIt's owner used to be a great knight.\nReal shit";
                    value = -4;

                    textureID = 0;
                    break;
            }

            SetTexture();
        }

    }
}
