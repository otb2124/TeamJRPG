using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
            Globals.group = new List<GroupMember>();
            overDraw = new List<Entity>();
            underDraw = new List<Entity>();
            entitiesToUpdate = new List<Entity>();

            Globals.currentGameMode = Globals.GameMode.playmode;
        }

        public void Load()
        {
            Globals.assetSetter.SetAssets();
            Globals.map.Load();
            Globals.aStarPathfinding.Init();
            Globals.uiManager.Init();

            Globals.player = new GroupMember(new Vector2(1, 1), Globals.assetSetter.textures[1][0][0]);
            Globals.player.isPlayer = true;
            Globals.player.name = "Vika";

            GroupMember member1 = new GroupMember(new Vector2(1, 2), Globals.assetSetter.textures[1][0][0]);
            member1.speed = 3f;
            member1.name = "Orest";
            GroupMember member2 = new GroupMember(new Vector2(1, 3), Globals.assetSetter.textures[1][0][0]);
            member2.speed = 3f;
            member1.name = "Slavic";
            GroupMember member3 = new GroupMember(new Vector2(1, 4), Globals.assetSetter.textures[1][0][0]);
            member3.speed = 3f;
            member1.name = "Artur";

            Globals.group.Add(Globals.player);
            Globals.group.Add(member1);
            Globals.group.Add(member2);
            Globals.group.Add(member3);

            Globals.entities.AddRange(Globals.group);
            Globals.entities.Add(new Object(new Vector2(15, 5), Globals.assetSetter.textures[2][0][0]));
        }

        public void Update()
        {
            Globals.inputManager.Update();

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
            Globals.entities.Sort((e1, e2) => (e1.drawPosition.Y + e1.texture.Height * Globals.gameScale).CompareTo(e2.drawPosition.Y + e2.texture.Height * Globals.gameScale));

            // Split entities into overDraw and underDraw lists
            foreach (Entity entity in Globals.entities)
            {
                if (entity.drawPosition.Y + (entity.texture.Height * Globals.gameScale) <= Globals.player.drawPosition.Y + (Globals.player.texture.Height * Globals.gameScale) && !(entity is GroupMember))
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
    }
}
