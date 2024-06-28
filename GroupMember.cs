using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace TeamJRPG
{
    public class GroupMember : Entity
    {
        public bool isPlayer;
        public string name;
        public GroupMember(Vector2 position, Texture2D texture) : base(position, texture)
        {
            speed = 3.5f;
            tileCollision = true;

            float collisionBoxWidth = Globals.tileSize.X / 2;
            float collisionBoxHeight = Globals.tileSize.Y / 2;
            float collisionBoxX = this.position.X + (Globals.tileSize.X - collisionBoxWidth) / 2;
            float collisionBoxY = this.position.Y;
            this.collisionBox = new System.Drawing.RectangleF(collisionBoxX, collisionBoxY, collisionBoxWidth, collisionBoxHeight);
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)collisionBoxWidth, (int)collisionBoxHeight, new Color(0, 0.5f, 0, 0.01f));

            isPlayer = false;
            name = "BLANK_NAME";
        }

        public override void Update()
        {
            HandleCollisions();

            previousPosition = new Point((int)(position.X / Globals.tileSize.X), (int)(position.Y / Globals.tileSize.X));

            if (isPlayer)
            {
                if (Globals.inputManager.IsKeyPressed(Keys.W)) { Move(new Vector2(0, -speed), Direction.up); }
                else if (Globals.inputManager.IsKeyPressed(Keys.S)) { Move(new Vector2(0, speed), Direction.down); }
                else if (Globals.inputManager.IsKeyPressed(Keys.A)) { Move(new Vector2(-speed, 0), Direction.left); }
                else if (Globals.inputManager.IsKeyPressed(Keys.D)) { Move(new Vector2(speed, 0), Direction.right); }

                HandleInterractions();
                CheckPlayerChange();
            }
            else
            {
                FollowPreviousMember();
            }

            this.collisionBox.Location = new System.Drawing.PointF(this.position.X + (Globals.tileSize.X - collisionBox.Width) / 2, this.position.Y + Globals.tileSize.Y / 2);

            HandleDirectionTextures();
            

            base.Update();
        }

        private void Move(Vector2 delta, Direction direction)
        {
            position += delta;
            this.direction = direction;
        }

        public void HandleCollisions()
        {
            Entity collidedEntity = Globals.collisionManager.CheckEntityCollision(this);

            if (collidedEntity != null)
            {
                // Object picking for autopicking
                if (collidedEntity is Object obj)
                {
                    if (obj.type == Object.ObjectType.autoPickable)
                    {
                        Globals.entities.Remove(collidedEntity);
                    }
                }
            }

            if ((collidedEntity != null && collidedEntity.entityCollision) && entityCollision)
            {
                if (collidedEntity is Mob || collidedEntity is GroupMember)
                {
                    //return;
                }
                else
                {
                    StopMovement();
                }
            }

            if (Globals.collisionManager.CheckTileCollision(this) && tileCollision)
            {
                StopMovement();
            }
        }

        public void HandleInterractions()
        {
            Entity interractedEntity = Globals.collisionManager.CheckEntityInterraction(this);

            if (interractedEntity != null)
            {
                // Object picking
                if (interractedEntity is Object obj)
                {
                    if (obj.type == Object.ObjectType.pickable)
                    {
                        if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Enter))
                        {
                            this.inventory.AddRange(obj.inventory);
                            Globals.entities.Remove(interractedEntity);
                        }
                    }
                }
            }
        }

        private void FollowPreviousMember()
        {
            int index = Globals.group.IndexOf(this);
            if (index > 0)
            {
                Entity previousMember = Globals.group[index - 1];
                MaintainDistance(previousMember);
            }
        }

        private void MaintainDistance(Entity previousMember)
        {
            float distance = Vector2.Distance(position, previousMember.position);
            if (distance > Globals.tileSize.X * 10)
            {
                // Teleport to the previous member if the distance is greater than 10 tiles
                position = previousMember.position;
                path = new Queue<Point>();
            }
            else if (distance > Globals.tileSize.X)
            {
                // Follow the previous member
                Follow(previousMember);
            }
        }

        private void StopMovement()
        {
            switch (direction)
            {
                case Direction.up:
                    position.Y += speed; break;
                case Direction.down:
                    position.Y -= speed; break;
                case Direction.right:
                    position.X -= speed; break;
                case Direction.left:
                    position.X += speed; break;
            }
        }

        private void HandleDirectionTextures()
        {
            switch (direction)
            {
                case Direction.up:
                    texture = Globals.assetSetter.textures[1][1][0]; break;
                case Direction.down:
                    texture = Globals.assetSetter.textures[1][0][0]; break;
                case Direction.right:
                    texture = Globals.assetSetter.textures[1][2][0]; break;
                case Direction.left:
                    texture = Globals.assetSetter.textures[1][3][0]; break;
            }
        }


        private void CheckPlayerChange()
        {
            // Check for player change input (previous)
            if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Q))
            {
                int currentIndex = Globals.group.IndexOf(this);
                int newIndex = (currentIndex - 1 + Globals.group.Count) % Globals.group.Count;
                SetPlayer(Globals.group[newIndex]);
            }
            // Check for player change input (next)
            else if (Globals.inputManager.IsKeyPressedAndReleased(Keys.E))
            {
                int currentIndex = Globals.group.IndexOf(this);
                int newIndex = (currentIndex + 1) % Globals.group.Count;
                SetPlayer(Globals.group[newIndex]);
            }
        }

        private void SetPlayer(GroupMember newPlayer)
        {
            if (newPlayer != this)
            {
                this.isPlayer = false;
                newPlayer.isPlayer = true;

                //Globals.uimanager.drawPointer();

                if (Globals.group.Contains(newPlayer))
                {
                    Globals.group.Remove(newPlayer);
                }

                Globals.group.Insert(0, newPlayer);
            }
        }


        public override void Draw()
        {
            drawPosition = new Vector2(position.X, position.Y - Globals.tileSize.Y);
            base.Draw();
        }
    }
}
