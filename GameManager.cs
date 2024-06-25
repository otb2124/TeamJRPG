using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class GameManager
    {


        public GameManager() 
        {
            Init();
        }



        public void Init()
        {
            Globals.map = new Map();
        }



        public void Load()
        {
            Globals.assetSetter.SetAssets();
            Globals.map.Load();
            Globals.player = new Player(new Vector2(1, 1), Globals.assetSetter.textures[1][0][0]);
            Globals.player = Globals.player;
        }

        public void Update()
        {
            Globals.inputManager.Update();
            Globals.player.Update();
            Globals.camera.Update();

        }



        public void Draw()
        {
            Globals.map.Draw();
            Globals.player.Draw();
        }
    }
}
