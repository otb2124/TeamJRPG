using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace TeamJRPG
{
    public class GameManager
    {


        public List<Entity> overDraw;
        public List<Entity> underDraw;
        private List<Entity> entitiesToUpdate;

        private Thread commandThread;

        public GameManager()
        {
            Init();
        }

        public void Init()
        {
            Globals.map = new Map();
            Globals.entities = new List<Entity>();
            Globals.group = new Group();
            overDraw = new List<Entity>();
            underDraw = new List<Entity>();
            entitiesToUpdate = new List<Entity>();

            Globals.currentGameMode = Globals.GameMode.playmode;
            Globals.currentGameState = Globals.GameState.playstate;
        }

        public void Load()
        {
            Globals.assetSetter.SetAssets();
            Globals.map.Load();
            Globals.aStarPathfinding.Init();
            Globals.uiManager.Init();


            Globals.group.Load();

            Globals.entities.AddRange(Globals.group.members);
            Globals.entities.Add(new Object(new Vector2(15, 5), 1));

            Globals.camera.Load();

            commandThread = new Thread(CommandHandler);
            commandThread.Start();
        }

        public void Update()
        {
            Globals.inputManager.Update();
            Globals.group.Update();


            //Game States
            if (Globals.currentGameState == Globals.GameState.playstate)
            {
                if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Escape))
                {
                    Globals.currentGameState = Globals.GameState.ingamemenustate;
                    Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                    Globals.uiManager.MenuStateNeedsChange = true;
                }
                // Game modes
                if (Globals.inputManager.IsKeyPressedAndReleased(Keys.H))
                {
                    if (Globals.currentGameMode == Globals.GameMode.playmode)
                    {
                        Globals.currentGameMode = Globals.GameMode.debugmode;
                    }
                    else
                    {
                        Globals.currentGameMode = Globals.GameMode.playmode;
                    }
                }
            }

            //UI STATES
            else if(Globals.currentGameState == Globals.GameState.ingamemenustate)
            {

                //IF ESC
                if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Escape))
                {

                    if (Globals.uiManager.HasCompositesOfType(UIComposite.UICompositeType.DESCRIPTION_WINDOW))
                    {
                        Globals.uiManager.RemoveAllCompositesOfTypes(UIComposite.UICompositeType.DESCRIPTION_WINDOW);
                    }
                    else
                    {
                        if (Globals.uiManager.currentMenuState == UIManager.MenuState.inGameMenu)
                        {
                            Globals.currentGameState = Globals.GameState.playstate;
                            Globals.uiManager.currentMenuState = UIManager.MenuState.clean;
                            Globals.uiManager.MenuStateNeedsChange = true;
                        }
                        else if (Globals.uiManager.currentMenuState != UIManager.MenuState.inGameMenu && Globals.uiManager.currentMenuState != UIManager.MenuState.clean)
                        {
                            Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                            Globals.uiManager.MenuStateNeedsChange = true;
                        }
                    }

                    
                }
                    


            }
            

            


            

            


            // Copy the entities that need to be updated to a separate list
            entitiesToUpdate.Clear();
            foreach (var entity in Globals.entities)
            {
                if (entity is Mob || entity is GroupMember)
                {
                    entitiesToUpdate.Add(entity);
                }
            }

            // Update entities from the separate list
            foreach (var entity in entitiesToUpdate)
            {
                entity.Update();
            }

            // Sort entities for drawing
            Globals.entities.Sort((e1, e2) => (e1.drawPosition.Y + e1.sprites[0].texture.Height * Globals.gameScale).CompareTo(e2.drawPosition.Y + e2.sprites[0].texture.Height * Globals.gameScale));

            // Split entities into overDraw and underDraw lists
            foreach (Entity entity in Globals.entities)
            {
                if (entity.drawPosition.Y + (entity.sprites[0].texture.Height * Globals.gameScale) <= Globals.player.drawPosition.Y + (Globals.player.sprites[0].texture.Height * Globals.gameScale) && !(entity is GroupMember))
                {
                    underDraw.Add(entity);
                }
                else
                {
                    overDraw.Add(entity);
                }
            }


            Globals.camera.Update();
            Globals.uiManager.Update();


            

        }

        public void Draw()
        {
            Globals.map.Draw();

            foreach (var entity in underDraw)
            {
                entity.Draw();
            }

            foreach (var entity in overDraw)
            {
                entity.Draw();
            }

            underDraw.Clear();
            overDraw.Clear();

            Globals.uiManager.Draw();
        }



        private void CommandHandler()
        {
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                Globals.commandManager.ExecuteCommand(input);
            }
        }

    }
}
