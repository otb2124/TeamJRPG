using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;



namespace TeamJRPG
{
    [Serializable]
    public class Room
    {


        public Tile[,] tiles;
        public Vector2 position;
        public Vector2 size;
        public System.Drawing.RectangleF bounds;
        public Texture2D collisionTexture;
        public Point roomSize = new Point(10, 10);

        public Room(Vector2 position)
        {
            tiles = new Tile[roomSize.X, roomSize.Y];
            this.position = position;
            Init();
            bounds = new System.Drawing.RectangleF(position.X, position.Y, size.X, size.Y);
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)size.X, (int)size.Y, new Color(0.1f, 0, 0.1f, 0.01f));
        }



        public void Init()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    // Check if the current tile is a border tile
                    bool isBorderTile = x == 0 || y == 0 || x == tiles.GetLength(0) - 1 || y == tiles.GetLength(1) - 1;

                    if (isBorderTile)
                    {
                        if ((x % 7 == 0 || y % 7 == 0) && !(x == 0 && y == 0) && !(x == tiles.GetLength(0) - 1 && y == tiles.GetLength(1) - 1))
                        {
                            tiles[x, y] = new Tile(new Vector2(x * Globals.tileSize.X + position.X, y * Globals.tileSize.Y + position.Y), 0);
                        }
                        else
                        {
                            // Set border tile texture and make it collidable
                            tiles[x, y] = new Tile(new Vector2(x * Globals.tileSize.X + position.X, y * Globals.tileSize.Y + position.Y), 1);
                            tiles[x, y].collision = true;
                        }
                    }
                    else
                    {
                        // Set regular tile texture (assuming index 0 is for regular tiles)
                        tiles[x, y] = new Tile(new Vector2(x * Globals.tileSize.X + position.X, y * Globals.tileSize.Y + position.Y), 0);
                    }

                    
                }
                size += Globals.tileSize;
            }
        }


        public void Draw()
        {

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    tiles[x, y].Draw();
                }
            }



            if (Globals.currentGameMode == Globals.GameMode.debugmode)
            {
                Globals.spriteBatch.Draw(collisionTexture, new Vector2(bounds.X, bounds.Y), null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
        }

    }
}
