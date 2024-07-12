using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TeamJRPG
{
    public class Animation
    {
        public Texture2D texture;
        public List<Rectangle> sourceRectangles = new List<Rectangle>();
        public int frames;
        public int currentFrame;
        public float frameTime;
        public float frameTimeLeft;
        public bool active;

        public Animation(Texture2D texture, int framesCountX, int framesCountY, float frameTime, int row = 1)
        {
            this.texture = texture;
            this.frames = framesCountX;
            this.frameTime = frameTime;
            this.frameTimeLeft = frameTime;
            currentFrame = 0;
            active = true;

            int frameWidth = texture.Width / framesCountX;
            int frameHeight = texture.Height / framesCountY;

            for (int i = 0; i < frames; i++)
            {
                sourceRectangles.Add(new Rectangle(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
            }
        }

        public void Start()
        {
            active = true;
        }

        public void Stop()
        {
            active = false;
        }

        public void Reset()
        {
            currentFrame = 0;
            frameTimeLeft = frameTime;
        }

        public void Update()
        {
            if (!active) return;

            frameTimeLeft -= (float)Globals.TotalSeconds;

            if (frameTimeLeft <= 0)
            {
                frameTimeLeft += frameTime;
                currentFrame = (currentFrame + 1) % frames;
            }
        }


        public void Draw(Vector2 position)
        {
            Globals.sprites.Draw(texture, position, GetCurrentFrame(), Color.White, 0f, Vector2.Zero, Globals.gameScale, SpriteEffects.None, 0f);
        }

        public Rectangle GetCurrentFrame()
        {
            return sourceRectangles[currentFrame];
        }
    }
}
