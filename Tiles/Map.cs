using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace TeamJRPG
{

    [Serializable]
    public class Map
    {

        public Tile[,] tiles;
        public Point mapSize = new Point(100, 50);


        public Map()
        {
            tiles = new Tile[mapSize.X, mapSize.Y];
        }


        public void Load()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    int textureIndex = RandomHelper.RandomInteger(0, Globals.assetSetter.textures[0].Length);


                    if (Globals.assetSetter.textures[0][textureIndex] == null)
                    {
                        textureIndex = 0;
                    }


                    tiles[x, y] = new Tile(new Vector2(x * Globals.tileSize.X, y * Globals.tileSize.Y), textureIndex);

                    if (textureIndex == 5)
                    {
                        tiles[x, y].collision = true;
                    }





                }
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
        }
    }
}
