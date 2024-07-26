using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class BattleActionMenu : UIComposite
    {


        public LiveEntity lent;

        public BattleActionMenu()
        {
            type = UICompositeType.BATTLE_MENU;

            Vector2 margin = new Vector2(20, 20);

            

            List<string> texts = new List<string> { "Skills", "Consumable Items", "Interract" };

            Vector2 textSize = Globals.assetSetter.fonts[0].MeasureString("Consumable Items");
            textSize.Y *= 2.2f;
            Vector2 framePos = new Vector2(margin.X, margin.Y * 2 + 64);
            Vector2 frameSize = new Vector2(textSize.X, textSize.Y*3 - 16);
            Frame actionFrame = new Frame(framePos, frameSize);
            children.Add(actionFrame);

            for (int i = 0; i < texts.Count; i++)
            {
                TextButton tb = new TextButton(texts[i], new Vector2(framePos.X, framePos.Y + textSize.Y*i), 0, 61+i, Color.White, 1);
                children.Add((tb));
            }
        }



        public override void Update()
        {
            base.Update();
        }
    }
}
