using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TeamJRPG
{
    public class Entity
    {


        public Vector2 position;
        public Vector2 drawPosition;
        public Texture2D texture;
        public int currentRoom;



        public float speed;
        public enum Direction
        {
            up, down, left, right
        }

        public Direction direction;


        public System.Drawing.RectangleF collisionBox;
        public Texture2D collisionTexture;
        public bool collision;


        public Entity(Vector2 position, Texture2D texture) 
        {
            this.position = position * Globals.tileSize;
            this.texture = texture;
            this.direction = Direction.up;

            this.collision = false;

            float collisionBoxWidth = Globals.tileSize.X / 2;
            float collisionBoxHeight = Globals.tileSize.Y / 2;
            float collisionBoxX = this.position.X;
            float collisionBoxY = this.position.Y;
            this.collisionBox = new System.Drawing.RectangleF(collisionBoxX, collisionBoxY, collisionBoxWidth, collisionBoxHeight);
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)collisionBoxWidth, (int)collisionBoxHeight, new Color(0.5f, 0, 0, 0.01f));
        }


        public void Update()
        {


            if ((Globals.collisionManager.CheckTileCollision(this) || (Globals.collisionManager.CheckEntityCollision(this) != null && Globals.collisionManager.CheckEntityCollision(this).collision)) && collision)
            {
                switch (direction)
                {
                    case Direction.up:
                        position.Y -= speed; break;
                    case Direction.down:
                        position.Y += speed; break;
                    case Direction.right:
                        position.X -= speed; break;
                    case Direction.left:
                        position.X += speed; break;
                }
            }

            for (int i = 0; i < Globals.map.rooms.Length; i++)
            {
                if (Globals.map.rooms[i].bounds.Contains(collisionBox))
                {
                    currentRoom = i; break;
                }
            }
        }



        public virtual void Draw()
        {
            Globals.spriteBatch.Draw(texture, drawPosition, null, Color.White, 0f, Vector2.Zero, Globals.gameScale, SpriteEffects.None, 0f);

            if(Globals.currentGameMode == Globals.GameMode.debugmode)
            {
                Globals.spriteBatch.Draw(collisionTexture, new Vector2(collisionBox.X, collisionBox.Y), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
            
        }
    }
}
