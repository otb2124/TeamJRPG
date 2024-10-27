using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TeamJRPG
{
    public class Button : UIComposite
    {
        public bool Activated = false;
        public bool OnHover = false;
        public int id;
        public System.Drawing.RectangleF buttonBox;
        public string floatingHint;

        public Vector2 frameSize;

        public Button(Sprite sprite, Vector2 startPosition, Vector2 scale, int id, List<string> floatingHint)
        {
            this.type = UICompositeType.BUTTON;
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);
            this.id = id;


            ImageHolder img = new ImageHolder(sprite, startPosition, Color.White, scale, null);
            if (floatingHint != null)
            {
                img.floatingText.AddRange(floatingHint);
            }

            frameSize = new Vector2(sprite.srcRect.Width *scale.X, sprite.srcRect.Height *scale.Y);
            buttonBox = new System.Drawing.RectangleF(position.X, position.Y, sprite.srcRect.Width * scale.X, sprite.srcRect.Height * scale.Y);

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
                OnHover = true;

                if (Globals.inputManager.IsMouseButtonClick(InputManager.MouseButton.Left))
                {

                    Activated = true;

                    switch (id)
                    {
                        case -1:
                            break;
                        case 0:
                            Globals.currentGameState = Globals.GameState.playState;
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
                            Globals.currentGameState = Globals.GameState.mainMenuState;
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


                        //battle_menu
                        case 60:
                            Debug.WriteLine("Escape");
                            break;
                        case 61:
                            Globals.uiManager.currentMenuState = UIManager.MenuState.battle_skills_menu;
                            Globals.uiManager.MenuStateNeedsChange = true;
                            break;
                        case 62:
                            Globals.uiManager.currentMenuState = UIManager.MenuState.battle_consumable_menu;
                            Globals.uiManager.MenuStateNeedsChange = true;
                            break;
                        case 63:
                            Globals.uiManager.currentMenuState = UIManager.MenuState.battle_interraction_menu;
                            Globals.uiManager.MenuStateNeedsChange = true;
                            break;


                        case 65:
                            Globals.uiManager.currentMenuState = UIManager.MenuState.battle_menu;
                            Globals.uiManager.MenuStateNeedsChange = true;
                            break;

                        //game over
                        case 70:
                            Console.WriteLine("Load last save");
                            break;
                        case 71:
                            Console.WriteLine("Load Save");
                            break;
                        case 72:
                            Console.WriteLine("Exit to Menu");
                            break;
                        case 73:
                            Console.WriteLine("Exit Game");
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
            else
            {
                OnHover = false;
            }

            

            base.Update();

        }
    }
}
