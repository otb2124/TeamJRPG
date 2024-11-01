﻿using Microsoft.Xna.Framework.Input;
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

            Globals.currentGameMode = Globals.GameMode.playMode;

            Globals.currentGameState = Globals.GameState.mainMenuState;
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


            Globals.currentGameMode = Globals.GameMode.playMode;

            Globals.currentGameState = Globals.GameState.playState;
            Globals.uiManager.currentMenuState = UIManager.MenuState.clean;
            Globals.uiManager.MenuStateNeedsChange = true;



            Globals.currentSaveName = "save10.json";

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




            if (Globals.currentGameState == Globals.GameState.playState || Globals.currentGameState == Globals.GameState.inGameMenuState || Globals.currentGameState == Globals.GameState.dialogueState || Globals.currentGameState == Globals.GameState.gameOverState)
            {


                Globals.group.Update();
                Globals.eventManager.CheckEvents();

                //Game States
                if (Globals.currentGameState == Globals.GameState.playState)
                {
                    if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Escape) || Globals.inputManager.IsKeyPressedAndReleased(Keys.Tab))
                    {
                        Globals.currentGameState = Globals.GameState.inGameMenuState;
                        Globals.uiManager.currentMenuState = UIManager.MenuState.inGameMenu;
                        Globals.uiManager.MenuStateNeedsChange = true;
                    }
                    // Game modes
                    if (Globals.inputManager.IsKeyPressedAndReleased(Keys.H))
                    {
                        if (Globals.currentGameMode == Globals.GameMode.playMode)
                        {
                            Globals.currentGameMode = Globals.GameMode.debugMode;
                            Console.WriteLine(Globals.currentEntities.Count);
                        }
                        else
                        {
                            Globals.currentGameMode = Globals.GameMode.playMode;
                        }
                    }


                    //DEBUG MODE
                    if (Globals.currentGameMode == Globals.GameMode.debugMode)
                    {
                        Console.SetCursorPosition(50, 0); // Set cursor position to top-left corner of console
                        Console.WriteLine($"Player Position: {Globals.player.GetMapPos()}");
                    }
                }
                



                //IN_GAME_MENU STATES
                else if (Globals.currentGameState == Globals.GameState.inGameMenuState)
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
                                Globals.currentGameState = Globals.GameState.playState;
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
                else if(Globals.currentGameState == Globals.GameState.dialogueState)
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



            if (Globals.currentGameState == Globals.GameState.battleState)
            {
                Globals.battleManager.Update();
                Globals.camera.Update();
            }


            Globals.uiManager.Update();


            

        }

        public void Draw()
        {

            if (Globals.currentGameState == Globals.GameState.playState || Globals.currentGameState == Globals.GameState.inGameMenuState || Globals.currentGameState == Globals.GameState.dialogueState || Globals.currentGameState == Globals.GameState.gameOverState)
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


            if(Globals.currentGameState == Globals.GameState.battleState)
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
