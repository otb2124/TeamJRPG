using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class Tile
    {

        public Vector2 position;
        public Texture2D texture;


        public Tile(Vector2 position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, Globals.gameScale, SpriteEffects.None, 0f);
        }
    }
}
