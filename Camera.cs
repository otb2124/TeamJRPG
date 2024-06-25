using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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


        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            Init();
        }

        public void Init()
        {
            position = Vector2.Zero;
            zoom = DEFAULT_ZOOM;
            rotation = 0f;
            UpdateTransform();
        }

        public void Move(Vector2 delta)
        {
            position += delta;
            UpdateTransform();
        }

        public void Zoom(float delta)
        {
            zoom += delta;
            if (zoom < 0.5f) zoom = 0.5f;
            if (zoom > 10f) zoom = 10f;
            UpdateTransform();
        }

        public Matrix Transform => transform;

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
