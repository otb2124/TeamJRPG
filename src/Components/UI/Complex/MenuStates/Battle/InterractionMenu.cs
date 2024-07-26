using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class InterractionMenu : UIComposite
    {


        public LiveEntity lent;
        public List<TextButton> buttons;

        public InterractionMenu()
        {
            type = UICompositeType.BATTLE_INTERRACTION_MENU;

            Vector2 margin = new Vector2(20, 20);

            lent = Globals.battleManager.all[Globals.battleManager.turnQueue.ElementAt(0)];

            Vector2 textSize = Globals.assetSetter.fonts[0].MeasureString("Persuade To Leave");
            textSize.Y *= 2.2f;
            Vector2 framePos = new Vector2(margin.X + 160, margin.Y * 2 + 64);
            Vector2 frameSize = new Vector2(textSize.X, textSize.Y * lent.actions.Count - 16);
            Frame actionFrame = new Frame(framePos, frameSize);
            children.Add(actionFrame);

            buttons = new List<TextButton>();

            for (int i = 0; i < lent.actions.Count; i++)
            {
                TextButton tb = new TextButton(lent.actions[i].text, new Vector2(framePos.X, framePos.Y + textSize.Y * i), 0, -1, Color.White, 1);
                buttons.Add(tb);
            }
            children.AddRange(buttons);
        }



        public override void Update()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Activated)
                {
                    Globals.battleManager.currentUsable = new BattleUsable(lent.actions[i]);
                    Globals.battleManager.InitializeTargetList();
                    Globals.uiManager.currentMenuState = UIManager.MenuState.battle_target_menu;
                    Globals.uiManager.MenuStateNeedsChange = true;
                }
            }
            base.Update();
        }
    }
}
