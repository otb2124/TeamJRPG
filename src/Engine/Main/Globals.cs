﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TeamJRPG
{
    public static class Globals
    {

        public static readonly float gameScale = 1.75f;
        public static readonly Vector2 tileSize = new Vector2(32 * gameScale, 32 * gameScale);


        public static float TotalSeconds { get; set; }
        public enum GameMode { playmode, debugmode }
        public static GameMode currentGameMode;

        public enum GameState { playstate, ingamemenustate }
        public static GameState currentGameState;

        public static List<Entity> entities;

        public static Game1 game;
        public static GameManager gameManager {  get; set; }
        public static Map map { get; set; }
        public static GroupMember player { get; set; }
        public static Group group { get; set; }

        public static AssetSetter assetSetter { get; set; }
        public static InputManager inputManager { get; set; }   
        public static CollisionManager collisionManager { get; set; }
        public static MapReader mapReader { get; set; }
        public static AStarPathfinding aStarPathfinding { get; set; }
        public static UIManager uiManager { get; set; }
        public static InventoryHandler inventoryHandler { get; set; }

        public static TextureManager TextureManager { get; set; }


        public static void Init()
        {
            gameManager = new GameManager();

            assetSetter = new AssetSetter();
            inputManager = new InputManager();
            collisionManager = new CollisionManager();  
            mapReader = new MapReader();
            aStarPathfinding = new AStarPathfinding();
            uiManager = new UIManager();
            inventoryHandler = new InventoryHandler();

            camera = new Camera(graphics.GraphicsDevice.Viewport);
        }
        public static void Exit()
        {
            game.Exit();
        }

        public static SpriteBatch sprites { get; set; }
        public static GraphicsDeviceManager graphics { get; set; }
        public static ContentManager Content { get; set; }
        public static Camera camera { get; set; }


    }
}