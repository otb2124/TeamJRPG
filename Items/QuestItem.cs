using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class QuestItem : Item
    {

        public int questItemID;
        public int textureID;

        public QuestItem(int questItemID)
        {
            this.questItemID = questItemID;
            type = ItemType.QUEST;

            SetQuestItem();
        }




        public void SetQuestItem()
        {
            switch (questItemID)
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

        public void SetTexture()
        {
            texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_QUESTITEMS][textureID][0];
        }
    }
}
