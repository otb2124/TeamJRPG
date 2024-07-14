using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{

    [Serializable]
    [JsonObject(IsReference = true)]
    public class Currency : Item
    {


        public Currency()
        {
            sprite = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.items, 0, new Microsoft.Xna.Framework.Vector2(0, 0), new Microsoft.Xna.Framework.Vector2(32, 32));
            type = ItemType.CURRENCY;
            IsStackable = true;
            value = 1;
        }
    }
}
