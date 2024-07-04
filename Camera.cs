using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TeamJRPG
{
    public class Camera
    {
        public Vector2 position;
        public float zoom;
        private float rotation;
        private Matrix transform;
        public Viewport viewport;
        public readonly float DEFAULT_ZOOM = 1.0f;
        public readonly float MIN_ZOOM = 0.85f, MAX_ZOOM = 5f;
        public readonly Vector2 MAX_DISTANCE_FROM_PLAYER = new Vector2(Globals.graphics.PreferredBackBufferWidth /2, Globals.graphics.PreferredBackBufferHeight/2);

        public bool FollowPlayer = false;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            Init();
        }

        public void Init()
        {
            zoom = DEFAULT_ZOOM;
            rotation = 0f;
        }

        public void Load()
        {
            position = Globals.player.position;
            ClampCameraPosition();
            UpdateTransform();
        }

        public void Update()
        {
            if (Globals.inputManager.IsKeyPressed(Keys.Left)) Move(new Vector2(-5, 0));
            if (Globals.inputManager.IsKeyPressed(Keys.Right)) Move(new Vector2(5, 0));
            if (Globals.inputManager.IsKeyPressed(Keys.Up)) Move(new Vector2(0, -5));
            if (Globals.inputManager.IsKeyPressed(Keys.Down)) Move(new Vector2(0, 5));

            if (Globals.inputManager.IsKeyPressed(Keys.OemPlus)) Zoom(0.05f);
            if (Globals.inputManager.IsKeyPressed(Keys.OemMinus)) Zoom(-0.05f);


            if (Globals.inputManager.IsKeyPressedAndReleased(Keys.OemTilde))
            {
                FollowPlayer = !FollowPlayer;
            }


            if (FollowPlayer)
            {
                position = Globals.player.position;
            }


            ClampCameraPosition();
            UpdateTransform();

        }


        public void Move(Vector2 delta)
        {
            position += delta;
        }

        public void Zoom(float delta)
        {
            zoom += delta;
            zoom = MathHelper.Clamp(zoom, MIN_ZOOM, MAX_ZOOM);
        }

        public Matrix Transform => transform;

        private void ClampCameraPosition()
        {
            float cameraWidth = viewport.Width / zoom;
            float cameraHeight = viewport.Height / zoom;


            if (!FollowPlayer)
            {
                Vector2 playerPosition = Globals.player.position;
                position.X = MathHelper.Clamp(position.X, playerPosition.X - MAX_DISTANCE_FROM_PLAYER.X, playerPosition.X + MAX_DISTANCE_FROM_PLAYER.X);
                position.Y = MathHelper.Clamp(position.Y, playerPosition.Y - MAX_DISTANCE_FROM_PLAYER.Y, playerPosition.Y + MAX_DISTANCE_FROM_PLAYER.Y);
            }


            position.X = MathHelper.Clamp(position.X, cameraWidth / 2, Globals.map.mapSize.X * Globals.tileSize.X - cameraWidth / 2);
            position.Y = MathHelper.Clamp(position.Y, cameraHeight / 2, Globals.map.mapSize.Y * Globals.tileSize.Y - cameraHeight / 2);

            
        }

        private void UpdateTransform()
        {
            transform =
                Matrix.CreateTranslation(new Vector3(-position, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(zoom) *
                Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
        }
    }
}
