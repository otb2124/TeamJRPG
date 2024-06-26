using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace TeamJRPG
{
    public class GameManager
    {

        public List<Entity> overDraw;
        public List<Entity> underDraw;


        public GameManager()
        {
            Init();
        }



        public void Init()
        {
            Globals.map = new Map();
            Globals.entities = new List<Entity>();
            overDraw = new List<Entity>();
            underDraw = new List<Entity>();

            Globals.currentGameMode = Globals.GameMode.playmode;
        }



        public void Load()
        {
            Globals.assetSetter.SetAssets();
            Globals.map.Load();
            //Globals.mapReader.WriteMap("Content/maps/map1.json");

            Globals.player = new Player(new Vector2(1, 1), Globals.assetSetter.textures[1][0][0]);
            Globals.entities.Add(Globals.player);
            Globals.entities.Add(new Object(new Vector2(15, 5), Globals.assetSetter.textures[2][0][0]));

            
        }

        public void Update()
        {

            Globals.inputManager.Update();

            //gamemodes
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


            
            



            //over\underdraw
            foreach (Entity entity in Globals.entities)
            {
                if (entity.drawPosition.Y - (Globals.tileSize.Y*1.25f) <= Globals.player.drawPosition.Y && !(entity is Player))
                {
                    underDraw.Add(entity);
                }
                else
                {
                    overDraw.Add(entity);
                }
            }



            Globals.player.Update();
            Globals.camera.Update();

        }



        public void Draw()
        {
            Globals.map.Draw();
            for (int i = 0; i < underDraw.Count; i++)
            {
                underDraw[i].Draw();
            }
            for (int i = 0; i < overDraw.Count; i++)
            {
                overDraw[i].Draw();
            }

            underDraw.Clear();
            overDraw.Clear();
        }
    }
}
