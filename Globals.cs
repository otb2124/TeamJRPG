using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TeamJRPG
{
    public static class Globals
    {

        public static readonly float gameScale = 1.75f;
        public static readonly Vector2 tileSize = new Vector2(32 * gameScale, 32 * gameScale);
        public static readonly Point roomSize = new Point(10, 10);
        public static readonly int mapSize = 3;

        public enum GameMode { playmode, debugmode }
        public static GameMode currentGameMode;

        public static List<Entity> entities;

        public static GameManager gameManager {  get; set; }
        public static Map map { get; set; }
        public static Player player { get; set; }


        public static AssetSetter assetSetter { get; set; }
        public static InputManager inputManager { get; set; }   
        public static CollisionManager collisionManager { get; set; }

        public static void Init()
        {
            gameManager = new GameManager();

            assetSetter = new AssetSetter();
            inputManager = new InputManager();
            collisionManager = new CollisionManager();  

            camera = new Camera(graphics.GraphicsDevice.Viewport);
        }


        public static SpriteBatch spriteBatch { get; set; }
        public static GraphicsDeviceManager graphics { get; set; }
        public static ContentManager Content { get; set; }
        public static Camera camera { get; set; }


    }
}
