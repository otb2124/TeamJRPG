using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;




namespace TeamJRPG
{
    public class Sprite
    {

        public Texture2D texture;
        public Rectangle srcRect;

        public Vector2 sheetPosition;
        public Vector2 sheetSize;

        public Vector2 size;
        public int Height, Width;

        public bool IsEmpty;


        public Sprite(SpriteSheet sheet, Vector2 sheetPosition, Vector2 size) 
        {
            this.texture = sheet.texture;
            this.sheetPosition = sheetPosition;
            this.sheetSize = size;
            this.srcRect = new Rectangle(sheetPosition.ToPoint(), sheetSize.ToPoint());
            this.size = sheetSize;
            this.Width = sheetSize.ToPoint().X;
            this.Height = sheetSize.ToPoint().Y;
        }


        public Sprite(Texture2D tex, Vector2 sheetPosition, Vector2 size)
        {
            this.texture = tex;
            this.sheetPosition = sheetPosition;
            this.sheetSize = size;
            this.srcRect = new Rectangle(sheetPosition.ToPoint(), sheetSize.ToPoint());
            this.size = sheetSize;
            this.Width = sheetSize.ToPoint().X;
            this.Height = sheetSize.ToPoint().Y;
        }


        public void ResetSrcRect(Vector2 pos, Vector2 size)
        {
            this.srcRect = new Rectangle(pos.ToPoint(), size.ToPoint());
        }


        public void Draw(Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effect, float layerDepth)
        {
            Globals.sprites.Draw(texture, position, srcRect, color, rotation, origin, scale, effect, layerDepth);
        }
    }


    
}
