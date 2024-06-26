using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TeamJRPG
{
    [Serializable]
    public class Tile
    {

        public Vector2 position;
        public Texture2D texture;
        public bool collision;
        public System.Drawing.RectangleF collisionBox;
        public Texture2D collisionTexture;

        public Tile(Vector2 position, int id)
        {
            this.position = position;
            this.texture = Globals.assetSetter.textures[0][id][0];
            this.collision = false;
            this.collisionBox = new System.Drawing.RectangleF(position.X, position.Y, Globals.tileSize.X, Globals.tileSize.Y);
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)Globals.tileSize.X, (int)Globals.tileSize.Y, new Color(0, 0, 0.5f, 0.01f));
        }

        public void Draw()
        {


            Globals.spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, Globals.gameScale, SpriteEffects.None, 0f);

            if (collision && Globals.currentGameMode == Globals.GameMode.debugmode)
            {
                Globals.spriteBatch.Draw(collisionTexture, new Vector2(collisionBox.X, collisionBox.Y), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
            

        }



        
    }
}
