using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;


namespace TeamJRPG
{
    public class BattleTargetMenu : UIComposite
    {


        public List<Button> allButtons;
        public List<Color> defaultColors;

        public List<int> chosenTargets;
        public List<int> restrictedTargets;

        public bool SelfSkip = false;
        public bool Done = false;

        public BattleTargetMenu()
        {
            type = UICompositeType.BATTLE_TARGET_MENU;

            allButtons = new List<Button>();
            defaultColors = new List<Color>();

            for (int i = 0; i < Globals.battleManager.all.Count; i++)
            {
                Vector2 pos = new Vector2(Globals.battleManager.all[i].battleScreenPosition.X - Globals.battleManager.TotalEntitiyX / 2 - Globals.battleManager.all[i].sprites[0].Width - 16, Globals.battleManager.all[i].battleScreenPosition.Y + Globals.tileSize.Y / 2);

                Button btn = new Button(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.placeholders, 0, new Vector2(0, 0), new Vector2(32, 32)), pos, new Vector2(1, 2), -1, new List<string> { Globals.battleManager.all[i].name });
                allButtons.Add(btn);

                defaultColors.Add(Globals.battleManager.all[i].skinColor);
            }
            children.AddRange(allButtons);



            

            restrictedTargets = new List<int>();



            BattleUsable usable = Globals.battleManager.currentUsable;
            if (usable.consumable != null)
            {
                SwitchCast(usable.consumable.casts[0]);
            }
            else if (usable.skill != null)
            {
                SwitchCast(usable.skill.casts[0]);
            }
            else if(usable.action != null)
            {
                SwitchCast(usable.action.cast);
            }

            




            chosenTargets = new List<int>();
        }


        public void SwitchCast(Cast cast)
        {

            List<int> allies = new List<int>();
            List<int> enemies = new List<int>();
            for (int i = 0; i < Globals.battleManager.leftSide.Count; i++)
            {
                allies.Add(i);
            }
            for (int i = Globals.battleManager.leftSide.Count; i < Globals.battleManager.all.Count; i++)
            {
                enemies.Add(i);
            }


            switch (cast.castTargetType)
            {
                case Cast.CastTargetType.enemy:
                    restrictedTargets.Add(Globals.battleManager.turnQueue.ElementAt(0));
                    restrictedTargets.AddRange(allies);
                    break;
                case Cast.CastTargetType.ally:
                    restrictedTargets.Add(Globals.battleManager.turnQueue.ElementAt(0));
                    restrictedTargets.AddRange(enemies);
                    break;
                case Cast.CastTargetType.allySelf:
                    restrictedTargets.AddRange(enemies);
                    break;
                case Cast.CastTargetType.any:
                    restrictedTargets.Add(Globals.battleManager.turnQueue.ElementAt(0));
                    break;
                case Cast.CastTargetType.anySelf:
                    break;
                case Cast.CastTargetType.self:
                    SelfSkip = true;
                    break;
            }
        }


        public override void Update()
        {
            for (int i = 0; i < allButtons.Count; i++)
            {
                if (allButtons[i].OnHover && !restrictedTargets.Contains(i) && !SelfSkip && !Done)
                {
                    Globals.battleManager.all[i].skinColor = Color.Green;
                    Globals.battleManager.all[i].hairColor = Color.Green;
                    Globals.battleManager.all[i].eyeColor = Color.Green;



                    if (allButtons[i].Activated)
                    {

                        if (chosenTargets.Contains(i))
                        {
                            chosenTargets.Remove(i);
                        }
                        else
                        {
                            if (chosenTargets.Count < Globals.battleManager.targetIDList.Length)
                            {
                                chosenTargets.Add(i);
                            }

                            if (chosenTargets.Count == Globals.battleManager.targetIDList.Length)
                            {
                                Globals.battleManager.targetIDList = new int[chosenTargets.Count];

                                for (global::System.Int32 j = 0; j < chosenTargets.Count; j++)
                                {
                                    Globals.battleManager.targetIDList[j] = chosenTargets[j];
                                }

                                RestoreColors();


                                Globals.battleManager.InitCast();
                                Done = true;

                                return;
                            }
                        }



                    }
                }
                else
                {
                    Globals.battleManager.all[i].skinColor = defaultColors[i];
                    Globals.battleManager.all[i].hairColor = defaultColors[i];
                    Globals.battleManager.all[i].eyeColor = defaultColors[i];
                }



                if (chosenTargets.Contains(i) && !Done)
                {
                    Globals.battleManager.all[i].skinColor = Color.DarkGreen;
                    Globals.battleManager.all[i].hairColor = Color.DarkGreen;
                    Globals.battleManager.all[i].eyeColor = Color.DarkGreen;
                }
            }


            if (SelfSkip && !Done)
            {
                Globals.battleManager.targetIDList = new int[1];
                Globals.battleManager.targetIDList[0] = Globals.battleManager.turnQueue.ElementAt(0);

                
                RestoreColors();


                Globals.battleManager.InitCast();
                Done = true;
            }


            base.Update();
        }


        public void RestoreColors()
        {
            for (global::System.Int32 i = 0; i < Globals.battleManager.all.Count; i++)
            {
                Globals.battleManager.all[i].skinColor = defaultColors[i];
                Globals.battleManager.all[i].hairColor = defaultColors[i];
                Globals.battleManager.all[i].eyeColor = defaultColors[i];
            }
        }
    }
}
