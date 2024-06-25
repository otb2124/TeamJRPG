using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;



namespace TeamJRPG
{
    public class Player : Entity
    {
        public Player(Vector2 position, Texture2D texture) : base(position, texture)
        {
            speed = 2.5f;
            this.collisionBox = new System.Drawing.RectangleF(position.X, position.Y, texture.Width/2 * Globals.gameScale + position.X, texture.Height/4 * Globals.gameScale + position.Y);
        }





        public void Update()
        {

            if (Globals.inputManager.IsKeyPressed(Keys.W)) { position.Y -= speed; direction = Direction.down; texture = Globals.assetSetter.textures[1][1][0]; }
            else if (Globals.inputManager.IsKeyPressed(Keys.S)) { position.Y += speed; direction = Direction.up; texture = Globals.assetSetter.textures[1][0][0]; }
            else if (Globals.inputManager.IsKeyPressed(Keys.A)) { position.X -= speed; direction = Direction.left; texture = Globals.assetSetter.textures[1][3][0];  }
            else if (Globals.inputManager.IsKeyPressed(Keys.D)) { position.X += speed; direction = Direction.right; texture = Globals.assetSetter.textures[1][2][0]; }


            base.Update();
        }
    }
}
