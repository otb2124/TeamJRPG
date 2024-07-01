using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TeamJRPG
{
    public class Entity
    {


        public Vector2 position;
        public Vector2 drawPosition;
        public Texture2D texture;
        public Texture2D characterTexture;
        public Texture2D battleTexture;
        public int currentRoom;



        public float speed;
        public enum Direction
        {
            up, down, left, right
        }

        public Direction direction;


        public System.Drawing.RectangleF collisionBox;
        public Texture2D collisionTexture;
        public bool entityCollision;
        public bool tileCollision;


        public Queue<Point> path;
        public Point previousPosition;


        public List<Item> inventory;

        public Entity(Vector2 position, Texture2D texture) 
        {
            this.position = position * Globals.tileSize;
            this.texture = texture;
            this.direction = Direction.up;

            this.tileCollision = false;
            this.entityCollision = true;

            float collisionBoxWidth = Globals.tileSize.X / 2;
            float collisionBoxHeight = Globals.tileSize.Y / 2;
            float collisionBoxX = this.position.X;
            float collisionBoxY = this.position.Y;
            this.collisionBox = new System.Drawing.RectangleF(collisionBoxX, collisionBoxY, collisionBoxWidth, collisionBoxHeight);
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)collisionBoxWidth, (int)collisionBoxHeight, new Color(0.5f, 0, 0, 0.01f));

            this.inventory = new List<Item>();
        }


        public virtual void Update()
        {
            
        }


        public void Follow(Entity entity)
        {
            Point mobTile = new Point((int)(position.X / Globals.tileSize.X), (int)(position.Y / Globals.tileSize.Y));
            Point playerTile = new Point((int)(entity.position.X / Globals.tileSize.X), (int)(entity.position.Y / Globals.tileSize.Y));
            Point playerPreviousTile = entity.previousPosition;

            if (path == null || path.Count == 0 || (path.Count == 1 && path.Peek() == playerTile))
            {
                path = Globals.aStarPathfinding.FindPath(mobTile, playerPreviousTile);
            }

            if (path != null && path.Count > 0)
            {
                var nextTile = path.Peek();
                var targetPosition = new Vector2(nextTile.X * Globals.tileSize.X, nextTile.Y * Globals.tileSize.Y);

                if (Vector2.Distance(position, targetPosition) < speed)
                {
                    // Move the mob to the next tile
                    position = targetPosition;
                    path.Dequeue();
                }
                else
                {
                    // Move the mob towards the next tile
                    var movementDirection = Vector2.Normalize(targetPosition - position);
                    position += movementDirection * speed;

                    // Set the direction attribute based on the movement direction
                    if (Math.Abs(movementDirection.X) > Math.Abs(movementDirection.Y))
                    {
                        if (movementDirection.X > 0)
                        {
                            direction = Direction.right;
                        }
                        else
                        {
                            direction = Direction.left;
                        }
                    }
                    else
                    {
                        if (movementDirection.Y > 0)
                        {
                            direction = Direction.down;
                        }
                        else
                        {
                            direction = Direction.up;
                        }
                    }
                }

                // Recalculate path if next tile is the player's current tile
                if (path.Count == 0 || (path.Count == 1 && path.Peek() == playerTile))
                {
                    path = Globals.aStarPathfinding.FindPath(mobTile, playerPreviousTile);
                }
            }
        }






        public virtual void Draw()
        {
            Globals.spriteBatch.Draw(texture, drawPosition, null, new Color(1f, 1f, 1f), 0f, Vector2.Zero, Globals.gameScale, SpriteEffects.None, 0f);

            if(Globals.currentGameMode == Globals.GameMode.debugmode)
            {
                Globals.spriteBatch.Draw(collisionTexture, new Vector2(collisionBox.X, collisionBox.Y), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
            
        }
    }
}
