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


        public void Load()
        {
            Globals.mapReader.ReadConfig();
            Globals.config.ApplyAll();

            Globals.mapReader.ReadDialogues();
            Globals.assetSetter.SetAllAssets();

            Globals.currentGameMode = Globals.GameMode.playmode;

            Globals.currentGameState = Globals.GameState.mainmenustate;
            Globals.uiManager.currentMenuState = UIManager.MenuState.titlemenu;
            Globals.uiManager.MenuStateNeedsChange = true;

            Globals.camera.Reload();

            Console.WriteLine("Program Loaded Successfully");

            commandThread = new Thread(CommandHandler);
            commandThread.Start();
        }


        public void LoadGame()
        {

            Globals.maps = new Map[5];
            Globals.group = new Group();

            overDraw = new List<Entity>();
            underDraw = new List<Entity>();
            entitiesToUpdate = new List<Entity>();


            Globals.currentGameMode = Globals.GameMode.playmode;

            Globals.currentGameState = Globals.GameState.playstate;
            Globals.uiManager.currentMenuState = UIManager.MenuState.clean;
            Globals.uiManager.MenuStateNeedsChange = true;



            Globals.currentSaveName = "save7.json";

            Globals.mapReader.ReadMaps(Globals.currentSaveName);

            Globals.currentEntities = Globals.currentMap.entities;


            Globals.camera.Reload();

            Globals.collisionManager.Load();



            Globals.aStarPathfinding.Init();

            Globals.eventManager.SetEvents();


            
        }

        public void Update()
        {



            Globals.inputManager.Update();




            if (Globals.currentGameState == Globals.GameState.playstate || Globals.currentGameState == Globals.GameState.ingamemenustate || Globals.currentGameState == Globals.GameState.dialoguestate)
            {


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
                            Console.WriteLine(Globals.currentEntities.Count);
                        }
                        else
                        {
                            Globals.currentGameMode = Globals.GameMode.playmode;
                        }
                    }


                    //DEBUG MODE
                    if (Globals.currentGameMode == Globals.GameMode.debugmode)
                    {
                        Console.SetCursorPosition(50, 0); // Set cursor position to top-left corner of console
                        Console.WriteLine($"Player Position: {Globals.player.GetMapPos()}");
                    }
                }
                



                //IN_GAME_MENU STATES
                else if (Globals.currentGameState == Globals.GameState.ingamemenustate)
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
                else if(Globals.currentGameState == Globals.GameState.dialoguestate)
                {
                    if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Escape))
                    {
                        Globals.dialogueData.CloseDialogue();
                    }
                }










                // Copy the currentEntities that need to be updated to a separate list
                entitiesToUpdate.Clear();
                foreach (var entity in Globals.currentEntities)
                {
                    if (entity.entityType != Entity.EntityType.obj)
                    {
                        entitiesToUpdate.Add(entity);
                    }
                }

                // Update currentEntities from the separate list
                foreach (var entity in entitiesToUpdate)
                {
                    entity.Update();
                }

                // Sort currentEntities for drawing
                Globals.currentEntities.Sort((e1, e2) => (e1.drawPosition.Y + e1.sprites[0].texture.Height * Globals.gameScale).CompareTo(e2.drawPosition.Y + e2.sprites[0].texture.Height * Globals.gameScale));

                // Split currentEntities into overDraw and underDraw lists
                foreach (Entity entity in Globals.currentEntities)
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


            }



            if (Globals.currentGameState == Globals.GameState.battle)
            {
                Globals.battleManager.Update();
                Globals.camera.Update();
            }


            Globals.uiManager.Update();


            

        }

        public void Draw()
        {


            if (Globals.currentGameState == Globals.GameState.playstate || Globals.currentGameState == Globals.GameState.ingamemenustate || Globals.currentGameState == Globals.GameState.dialoguestate)
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
            }


            if(Globals.currentGameState == Globals.GameState.battle)
            {
                Globals.battleManager.Draw();
            }
            

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
