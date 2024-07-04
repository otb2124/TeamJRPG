using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TeamJRPG
{
    public class Button : UIComposite
    {
        public bool Active = false;
        public int id;
        public System.Drawing.RectangleF buttonBox;

        public Button(Texture2D texture, Vector2 startPosition, float scale, int id)
        {

            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);
            this.id = id;

            ImageHolder img = new ImageHolder(texture, startPosition, new Vector2(scale, scale));


            buttonBox = new System.Drawing.RectangleF(position.X, position.Y, texture.Width * scale, texture.Height * scale);

            components.AddRange(img.components);

            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                components[i].IsStickToZoom = true;
            }
        }



        public override void Update()
        {
            Active = false;

            if (buttonBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
            {

                if (Globals.inputManager.IsMouseButtonClick(InputManager.MouseButton.Left))
                {

                    Active = true;

                    switch (id)
                    {
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
                            Debug.WriteLine("left");
                            break;
                        case 10:
                            Debug.WriteLine("right");
                            break;
                        case 11:
                            Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                            Globals.uiManager.MenuStateNeedsChange = true;
                            break;
                        case 12:
                            Globals.Exit();
                            break;
                        case 13:
                            Debug.WriteLine("prev");
                            break;
                        case 14:
                            Debug.WriteLine("next");
                            break;
                        case 15:
                            Debug.WriteLine("completed quests");
                            break;
                        case 16:
                            Debug.WriteLine("secondary quests");
                            break;





                        case 100:
                            Debug.WriteLine("100");
                            break;
                        case 101:
                            Debug.WriteLine("101");
                            break;
                        case 102:
                            Debug.WriteLine("102");
                            break;
                        case 103:
                            Debug.WriteLine("103");
                            break;
                        case 104:
                            Debug.WriteLine("104");
                            break;
                    }
                }
            }

            base.Update();

        }
    }
}
