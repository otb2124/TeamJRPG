using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TeamJRPG
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.ApplyChanges();

            Globals.graphics = _graphics;
            Globals.Content = Content;
            Globals.game = this;
        }

        protected override void Initialize()
        {
            PythonTranslator.InitializePythonEngine();
            Globals.Init();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.spriteBatch = _spriteBatch;
            Globals.gameManager.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.gameManager.Update();   
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Globals.spriteBatch.Begin(transformMatrix: Globals.camera.Transform);
            Globals.gameManager.Draw();
            Globals.spriteBatch.End();
            base.Draw(gameTime);
        }




        protected override void OnExiting(object sender, EventArgs args)
        {
            PythonTranslator.ShutdownPythonEngine();
            base.OnExiting(sender, args);
        }
    }
}
