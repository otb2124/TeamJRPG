using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TeamJRPG
{
    public class Object : Entity
    {

        public enum ObjectType { pickable, unPickable, autoPickable}
        public ObjectType type;

        public System.Drawing.RectangleF interractionBox;
        public Texture2D interractionBoxTexture;

        public Object(Vector2 position) : base(position)
        {

            this.tileCollision = true;

            this.collisionBox.Height = Globals.tileSize.Y / 4;
            this.collisionBox.Width = Globals.tileSize.X / 3;
            this.collisionBox.Y = position.Y * Globals.tileSize.Y + this.collisionBox.Height;
            this.collisionBox.X = position.X * Globals.tileSize.X + this.collisionBox.Width;
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)this.collisionBox.Width, (int)this.collisionBox.Height, new Color(0.5f, 0, 0, 0.01f));


            float interractionBoxWidth = this.collisionBox.Width + 2 * Globals.tileSize.X;
            float interractionBoxHeight = this.collisionBox.Height + 2 * Globals.tileSize.Y;
            float interractionBoxX = this.collisionBox.X - Globals.tileSize.X;
            float interractionBoxY = this.collisionBox.Y - Globals.tileSize.Y;
            this.interractionBox = new System.Drawing.RectangleF(interractionBoxX, interractionBoxY, interractionBoxWidth, interractionBoxHeight);
            this.interractionBoxTexture = Globals.assetSetter.CreateSolidColorTexture((int)this.interractionBox.Width, (int)this.interractionBox.Height, new Color(0, 0, 0.1f, 0.1f));

            type = ObjectType.pickable;

            SetTextures();
            SetInventory();
        }



        public void SetInventory()
        {
            AddToInventory(new Consumable(0, 1));
        }



        public void SetTextures()
        {
            texture = new Texture2D[1];

            texture[0] = Globals.assetSetter.textures[Globals.assetSetter.OBJECTS][0][0];

            skinColor = Color.White;
        }



        public override void Draw()
        {
            drawPosition = new Vector2(position.X, position.Y);

            if(Globals.currentGameMode == Globals.GameMode.debugmode)
            {
                Globals.spriteBatch.Draw(interractionBoxTexture, new Vector2(interractionBox.X, interractionBox.Y), Color.White);
            }

            base.Draw();
        }
    }
}
