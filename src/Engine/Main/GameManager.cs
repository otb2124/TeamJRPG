using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
            Globals.maps = new Map[5];
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
            Globals.mapReader.ReadMaps("maps");
            Globals.currentMap = Globals.maps[0];

            Globals.collisionManager.Load();

            Globals.mapReader.ReadEntities("entities.json");

            Globals.aStarPathfinding.Init();
            Globals.uiManager.Init();

            

            Globals.group.SetInventory();
            Globals.group.SetQuests();

            Globals.camera.Load();

            Globals.eventManager.SetEvents();


            commandThread = new Thread(CommandHandler);
            commandThread.Start();


            //Globals.mapReader.WriteMap("save.json");
            //Globals.mapReader.WriteEntities("entities.json");
        }

        public void Update()
        {
            Globals.inputManager.Update();
            Globals.group.Update();
            Globals.eventManager.CheckEvents();

            //Game States
            if (Globals.currentGameState == Globals.GameState.playstate)
            {
                if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Escape) || Globals.inputManager.IsKeyPressedAndReleased(Keys.Tab))
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


                //DEBUG MODE
                if(Globals.currentGameMode == Globals.GameMode.debugmode)
                {
                    Console.SetCursorPosition(0, 0); // Set cursor position to top-left corner of console
                    Console.WriteLine($"Player Position: {Globals.player.GetMapPos()}");
                }
            }




            //UI STATES
            else if(Globals.currentGameState == Globals.GameState.ingamemenustate)
            {

                //IF ESC or TAB
                if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Escape) || Globals.inputManager.IsKeyPressedAndReleased(Keys.Tab))
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
            Globals.currentMap.Draw();

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



        public void OnExit()
        {
            Console.WriteLine("Program Exited");
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
