using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class Tile
    {

        public Vector2 position;
        public Texture2D texture;
        public int id;
        public bool collision;
        public System.Drawing.RectangleF collisionBox;



        public Texture2D collisionTexture;

        public Tile(Vector2 position, int id)
        {
            this.position = position;
            this.texture = Globals.assetSetter.textures[0][id][0];
            this.id = id;
            this.collision = false;
            this.collisionBox = new System.Drawing.RectangleF(position.X, position.Y, Globals.tileSize.X + position.X, Globals.tileSize.Y + position.Y);


            collisionTexture = Globals.assetSetter.createRect((int)collisionBox.Width, (int)collisionBox.Height);
        }

        public void Draw()
        {


            Globals.spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, Globals.gameScale, SpriteEffects.None, 0f);




            

            Vector2 coor = new Vector2(collisionBox.X, collisionBox.Y);

            if (collision)
            {
                Globals.spriteBatch.Draw(collisionTexture, coor, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
            

        }



        
    }
}
