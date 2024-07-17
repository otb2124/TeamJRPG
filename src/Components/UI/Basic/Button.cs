using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace TeamJRPG
{
    public class Button : UIComposite
    {
        public bool Activated = false;
        public int id;
        public System.Drawing.RectangleF buttonBox;
        public string floatingHint;

        public Vector2 frameSize;

        public Button(Sprite sprite, Vector2 startPosition, float scale, int id, List<string> floatingHint)
        {
            this.type = UICompositeType.BUTTON;
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);
            this.id = id;


            ImageHolder img = new ImageHolder(sprite, startPosition, Color.White, new Vector2(scale, scale), null);
            if (floatingHint != null)
            {
                img.floatingText.AddRange(floatingHint);
            }

            frameSize = new Vector2(sprite.srcRect.Width *scale, sprite.srcRect.Height *scale);
            buttonBox = new System.Drawing.RectangleF(position.X, position.Y, sprite.srcRect.Width * scale, sprite.srcRect.Height * scale);

            children.Add(img);

            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                components[i].IsStickToZoom = true;
            }
        }



        public override void Update()
        {
            Activated = false;

            if (buttonBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
            {

                if (Globals.inputManager.IsMouseButtonClick(InputManager.MouseButton.Left))
                {

                    Activated = true;

                    switch (id)
                    {
                        case -1:
                            break;
                        case 0:
                            Globals.currentGameState = Globals.GameState.playstate;
                            Globals.uiManager.currentMenuState = UIManager.MenuState.clean;
                            Globals.uiManager.MenuStateNeedsChange = true;
                            break;
                        case 1:
                            if (!Globals.uiManager.HasCompositesOfType(UICompositeType.INGAME_MENU_CHARACTERS)){
                                Globals.uiManager.currentMenuState = UIManager.MenuState.charachters;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            else
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            
                            break;
                        case 2:
                            if (!Globals.uiManager.HasCompositesOfType(UICompositeType.INGAME_MENU_INVENTORY))
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.inventory;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            else
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            break;
                        case 3:
                            if (!Globals.uiManager.HasCompositesOfType(UICompositeType.INGAME_MENU_SKILLS))
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.skills;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            else
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            break;
                        case 4:
                            if (!Globals.uiManager.HasCompositesOfType(UICompositeType.INGAME_MENU_QUESTBOOK))
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.questBook;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            else
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            break;
                        case 5:
                            if (!Globals.uiManager.HasCompositesOfType(UICompositeType.INGAME_MENU_STATS))
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.statistics;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            else
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            break;
                        case 6:
                            if (!Globals.uiManager.HasCompositesOfType(UICompositeType.INGAME_MENU_MAP))
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.map;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            else
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            break;
                        case 7:
                            if (!Globals.uiManager.HasCompositesOfType(UICompositeType.INGAME_MENU_SETTINGS))
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.settings;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            else
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            break;
                        case 8:
                            if (!Globals.uiManager.HasCompositesOfType(UICompositeType.INGAME_MENU_EXIT))
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.exit;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            else
                            {
                                Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                                Globals.uiManager.MenuStateNeedsChange = true;
                            }
                            break;
                        case 9:
                            Globals.player.SetPrevMemberToPlayer();
                            break;
                        case 10:
                            Globals.player.SetNextMemberToPlayer();
                            break;
                        case 11:
                            Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                            Globals.uiManager.MenuStateNeedsChange = true;
                            break;
                        case 12:
                            Globals.currentGameState = Globals.GameState.mainmenustate;
                            Globals.uiManager.currentMenuState = UIManager.MenuState.titlemenu;
                            Globals.uiManager.MenuStateNeedsChange = true;
                            break;



                        //skills character switch
                        case 13:
                            Globals.player.SetPrevMemberToPlayer();
                            break;
                        case 14:
                            Globals.player.SetNextMemberToPlayer();
                            break;


                        //questbook switch
                        case 15:
                            Debug.WriteLine("completed quests");
                            break;
                        case 16:
                            Debug.WriteLine("secondary quests");
                            break;


                        //20-49 ingamemenus



                        //mainmenu
                        case 50:
                            Debug.WriteLine("Continue");
                            Globals.gameManager.LoadGame();
                            break;
                        case 51:
                            Debug.WriteLine("Reload");
                            break;
                        case 52:
                            Debug.WriteLine("New");
                            break;
                        case 53:
                            Debug.WriteLine("Options");
                            break;
                        case 54:
                            Debug.WriteLine("Extras");
                            break;
                        case 55:
                            Globals.Exit();
                            break;


                        //battle
                        case 60:
                            Debug.WriteLine("Escape");
                            break;




                        //inventory character swap
                        case 100:
                            Globals.player.SetPlayer(Globals.group.members[id - 100]);
                            break;
                        case 101:
                            Globals.player.SetPlayer(Globals.group.members[id - 100]);
                            break;
                        case 102:
                            Globals.player.SetPlayer(Globals.group.members[id - 100]);
                            break;
                        case 103:
                            Globals.player.SetPlayer(Globals.group.members[id - 100]);
                            break;
                        case 104:
                            Globals.player.SetPlayer(Globals.group.members[id - 100]);
                            break;
                    }
                }
            }

            base.Update();

        }
    }
}
