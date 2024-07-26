using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class ConsumableMenu : UIComposite
    {


        public LiveEntity lent;
        public List<Button> consumableButtons;
        public List<Label> consumableLabels;
        public List<Consumable> consumables;

        public ConsumableMenu()
        {
            type = UICompositeType.BATTLE_CONSUMABLE_MENU;


            Vector2 margin = new Vector2(20, 20);


            Vector2 framePos = new Vector2(margin.X + 160, margin.Y * 2 + 64);
            Vector2 frameSize = new Vector2(64 - 32, Globals.camera.viewport.Height - (margin.Y * 2 + 64) - margin.Y * 2 - LiveEntity.DEFAULT_HUMANOID_BODY_SPRITE_SIZE.Y - 40);
            Frame skillFrame = new Frame(framePos, frameSize);
            children.Add(skillFrame);

            consumableButtons = new List<Button>();
            consumableLabels = new List<Label>();

            lent = Globals.battleManager.all[Globals.battleManager.turnQueue.ElementAt(0)];



            consumables = new List<Consumable>();
            for (int i = 0; i < Globals.group.inventory.Count; i++)
            {
                if (Globals.group.inventory[i].type == Item.ItemType.CONSUMABLE)
                {
                    consumables.Add((Consumable)Globals.group.inventory[i]);
                }
            }


            for (int i = 0; i < consumables.Count; i++)
            {
                Button skillButton = new Button(consumables[i].sprite, new Vector2(framePos.X, framePos.Y + 64 * i), new Vector2(2, 2), -1, new List<string> { consumables[i].name, consumables[i].description });
                consumableButtons.Add(skillButton);
                if (consumables[i].amount > 1)
                {
                    Label amount = new Label(consumables[i].amount.ToString(), new Vector2(framePos.X + 8, framePos.Y + 64 * i + 28), 1, Color.White, new Stroke(1, Color.Black, MonoGame.StrokeType.OutlineAndTexture));
                    consumableLabels.Add(amount);
                }
                

            }

            children.AddRange(consumableButtons);
            children.AddRange(consumableLabels);



        }



        public override void Update()
        {
            for (int i = 0; i < consumableButtons.Count; i++)
            {
                if (consumableButtons[i].Activated)
                {
                    Globals.battleManager.currentUsable = new BattleUsable(consumables[i]);
                    Globals.battleManager.InitializeTargetList();
                    Globals.uiManager.currentMenuState = UIManager.MenuState.battle_target_menu;
                    Globals.uiManager.MenuStateNeedsChange = true;
                }
            }

            base.Update();
        }
    }
}
