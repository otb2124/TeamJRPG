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

        public enum GameMode { playmode, debugmode }
        public static GameMode currentGameMode;

        public static List<Entity> entities;

        public static GameManager gameManager {  get; set; }
        public static Map map { get; set; }
        public static GroupMember player { get; set; }
        public static List<GroupMember> group { get; set; }


        public static AssetSetter assetSetter { get; set; }
        public static InputManager inputManager { get; set; }   
        public static CollisionManager collisionManager { get; set; }
        public static MapReader mapReader { get; set; }
        public static AStarPathfinding aStarPathfinding { get; set; }

        public static void Init()
        {
            gameManager = new GameManager();

            assetSetter = new AssetSetter();
            inputManager = new InputManager();
            collisionManager = new CollisionManager();  
            mapReader = new MapReader();
            aStarPathfinding = new AStarPathfinding();

            camera = new Camera(graphics.GraphicsDevice.Viewport);
        }


        public static SpriteBatch spriteBatch { get; set; }
        public static GraphicsDeviceManager graphics { get; set; }
        public static ContentManager Content { get; set; }
        public static Camera camera { get; set; }


    }
}
