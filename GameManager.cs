using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Drawing;

namespace TeamJRPG
{
    public class GameManager
    {


        public List<Entity> overDraw;
        public List<Entity> underDraw;
        private List<Entity> entitiesToUpdate;



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

            Globals.player = new GroupMember(new Vector2(1, 1));
            Globals.player.isPlayer = true;
            Globals.player.name = "Vika";

            GroupMember member1 = new GroupMember(new Vector2(1, 2));
            member1.name = "Orest";
            GroupMember member2 = new GroupMember(new Vector2(1, 3));

            member2.name = "Slavic";
            GroupMember member3 = new GroupMember(new Vector2(1, 4));

            member3.name = "Artur";

            Globals.group.members.Add(Globals.player);
            Globals.group.members.Add(member1);
            Globals.group.members.Add(member2);
            Globals.group.members.Add(member3);
            Globals.group.SetInventory();

            Globals.entities.AddRange(Globals.group.members);
            Globals.entities.Add(new Object(new Vector2(15, 5), 1));

            Globals.camera.Load();
        }

        public void Update()
        {
            Globals.inputManager.Update();

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
            else if(Globals.currentGameState == Globals.GameState.ingamemenustate)
            {
                if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Escape))
                {
                    Globals.currentGameState = Globals.GameState.playstate;
                    Globals.uiManager.currentMenuState = UIManager.MenuState.clean;
                    Globals.uiManager.MenuStateNeedsChange = true;
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
            Globals.entities.Sort((e1, e2) => (e1.drawPosition.Y + e1.texture[0].Height * Globals.gameScale).CompareTo(e2.drawPosition.Y + e2.texture[0].Height * Globals.gameScale));

            // Split entities into overDraw and underDraw lists
            foreach (Entity entity in Globals.entities)
            {
                if (entity.drawPosition.Y + (entity.texture[0].Height * Globals.gameScale) <= Globals.player.drawPosition.Y + (Globals.player.texture[0].Height * Globals.gameScale) && !(entity is GroupMember))
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



            if (Globals.playerChanged)
            {
                Globals.playerChanged = false;
            }
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


        
    }
}
