using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class BattleMenu : UIComposite
    {


        public LiveEntity lent;
        public List<Button> skillButtons;

        public BattleMenu() 
        {
            type = UICompositeType.BATTLE_MENU;



            Vector2 margin = new Vector2(20, 20);



            Button escapeButton = new Button(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 2, new Vector2(0, 32), new Vector2(32, 32)), margin, 2, 60, new List<string> { "Escape", Globals.battleManager.EscapeChance * 100 + "%" });
            children.Add(escapeButton);

            Vector2 framePos = new Vector2(margin.X, margin.Y * 2 + 64);
            Vector2 frameSize = new Vector2(64 - 32, Globals.camera.viewport.Height - (margin.Y * 2 + 64) - margin.Y * 2 - LiveEntity.DEFAULT_HUMANOID_BODY_SPRITE_SIZE.Y - 40);
            Frame skillFrame = new Frame(framePos, frameSize);
            children.Add(skillFrame);

            skillButtons = new List<Button>();

            lent = Globals.battleManager.all[Globals.battleManager.turnQueue.ElementAt(0)];

            for (int i = 0; i < lent.skills.Count; i++)
            {
                Vector2 skillSpritePos = Vector2.Zero;
                switch (lent.skills[i].name)
                {
                    case "Common Attack":
                        skillSpritePos = Vector2.Zero;
                        break;
                    case "Block":
                        skillSpritePos = new Vector2(0, 32); 
                        break;
                    case "Heal":
                        skillSpritePos = new Vector2(0, 64);
                        break;
                }

                Sprite skillSprite = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 3, skillSpritePos, new Vector2(32, 32));

                Button skillButton = new Button(skillSprite, new Vector2(framePos.X, framePos.Y + 64 * i), 2, -1, new List<string> { lent.skills[i].name, lent.skills[i].description });
                skillButtons.Add(skillButton);
                
            }

            children.AddRange(skillButtons);
        }



        public override void Update()
        {
            for (int i = 0; i < skillButtons.Count; i++)
            {
                if (skillButtons[i].Activated)
                {
                    Globals.battleManager.CastSkill(lent, lent.skills[i]);
                }
            }

            base.Update();
        }
    }
}
