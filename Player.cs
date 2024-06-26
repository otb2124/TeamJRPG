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
            speed = 3.5f;

            collision = true;

            float collisionBoxWidth = Globals.tileSize.X / 2;
            float collisionBoxHeight = Globals.tileSize.Y / 2;
            float collisionBoxX = this.position.X + (Globals.tileSize.X - collisionBoxWidth) / 2;
            float collisionBoxY = this.position.Y;
            this.collisionBox = new System.Drawing.RectangleF(collisionBoxX, collisionBoxY, collisionBoxWidth, collisionBoxHeight);
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)collisionBoxWidth, (int)collisionBoxHeight, new Color(0, 0.5f, 0, 0.01f));
        }





        public void Update()
        {
            this.collisionBox.Location = new System.Drawing.PointF(this.position.X + (Globals.tileSize.X - collisionBox.Width) / 2, this.position.Y + Globals.tileSize.Y / 2);

            base.Update();

            if (Globals.inputManager.IsKeyPressed(Keys.W)) { position.Y -= speed; direction = Direction.down; texture = Globals.assetSetter.textures[1][1][0]; }
            else if (Globals.inputManager.IsKeyPressed(Keys.S)) { position.Y += speed; direction = Direction.up; texture = Globals.assetSetter.textures[1][0][0]; }
            else if (Globals.inputManager.IsKeyPressed(Keys.A)) { position.X -= speed; direction = Direction.left; texture = Globals.assetSetter.textures[1][3][0]; }
            else if (Globals.inputManager.IsKeyPressed(Keys.D)) { position.X += speed; direction = Direction.right; texture = Globals.assetSetter.textures[1][2][0]; }

            HandleCollisions();
            HandleInterractions();
        }


        public void HandleCollisions()
        {
            Entity collidedEntity = Globals.collisionManager.CheckEntityCollision(this);

            if (collidedEntity != null)
            {

                //object picking for autopicking
                if (collidedEntity is Object obj)
                {
                    if (obj.type == Object.ObjectType.autoPickable)
                    {
                        Globals.entities.Remove(collidedEntity);
                    }
                }

            }
        }

        public void HandleInterractions()
        {
            Entity interractedEntity = Globals.collisionManager.CheckEntityInterraction(this);

            if (interractedEntity != null)
            {


                //object picking
                if (interractedEntity is Object obj)
                {
                    if (obj.type == Object.ObjectType.pickable)
                    {
                        if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Enter))
                        {
                            Globals.entities.Remove(interractedEntity);
                        }
                    }
                }

            }
        }


        public override void Draw()
        {
            drawPosition = new Vector2(position.X, position.Y - Globals.tileSize.Y);

            base.Draw();
        }
    }
}
