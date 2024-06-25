using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TeamJRPG
{
    public class Entity
    {


        public Vector2 position;
        public Texture2D texture;
        public int currentRoom;



        public float speed;
        public enum Direction
        {
            up, down, left, right
        }

        public Direction direction;


        public System.Drawing.RectangleF collisionBox;



        public Entity(Vector2 position, Texture2D texture) 
        {
            this.position = position * Globals.tileSize;
            this.texture = texture;
            this.direction = Direction.up;
            this.collisionBox = new System.Drawing.RectangleF(position.X, position.Y, Globals.tileSize.X + position.X, Globals.tileSize.Y + position.Y);
        }


        public void Update()
        {
            this.collisionBox.Location = new System.Drawing.PointF(position.X, position.Y);


            if (Globals.collisionManager.CheckCollision(this))
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



        public void Draw()
        {
            Vector2 drawPosition = new Vector2(position.X, position.Y - Globals.tileSize.Y);

            Globals.spriteBatch.Draw(texture, drawPosition, null, Color.White, 0f, Vector2.Zero, Globals.gameScale, SpriteEffects.None, 0f);



            Texture2D rect = new Texture2D(Globals.graphics.GraphicsDevice, (int)collisionBox.Width, (int)collisionBox.Height);

            Color[] data = new Color[(int)collisionBox.Width * (int)collisionBox.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = new Color(255, 0, 0, 1);
            rect.SetData(data);

            Vector2 coor = new Vector2(collisionBox.X, collisionBox.Y);
            Globals.spriteBatch.Draw(rect, coor, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }
    }
}
