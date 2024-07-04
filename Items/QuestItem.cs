using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class QuestItem : Item
    {


        public QuestItem(string name) : base(name)
        {
            texture = Globals.assetSetter.textures[Globals.assetSetter.ITEMS_QUESTITEMS][0][0];
            type = ItemType.QUEST;
        }
    }
}
