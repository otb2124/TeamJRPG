using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
        public SpriteEffects effect;

        public Animation(Texture2D texture, int framesCountX, Vector2 startPos, Vector2 frameSize, float frameTime, SpriteEffects effect)
        {
            this.texture = texture;
            this.frames = framesCountX;
            this.frameTime = frameTime;
            this.frameTimeLeft = frameTime;
            currentFrame = 0;
            active = true;
            this.effect = effect;

            for (int i = 0; i < frames; i++)
            {
                sourceRectangles.Add(new Rectangle(i * (int)frameSize.X + (int)startPos.X, (int)startPos.Y, (int)frameSize.X, (int)frameSize.Y));
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
            Console.WriteLine(effect.ToString());
            Globals.sprites.Draw(texture, position, GetCurrentFrame(), Color.White, 0f, Vector2.Zero, Globals.gameScale, effect, 0f);
        }

        public Rectangle GetCurrentFrame()
        {
            return sourceRectangles[currentFrame];
        }
    }
}
