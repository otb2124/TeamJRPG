﻿using Microsoft.Xna.Framework;
using System;

namespace TeamJRPG
{
    [Serializable]
    public class Map
    {
        public Tile[,] tiles;
        public Point mapSize = new Point(100, 100);

        public Map()
        {
            tiles = new Tile[mapSize.X, mapSize.Y];
        }

        public void Load()
        {
            Console.WriteLine("Generating Map...");

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    int tileKinds = Globals.TextureManager.GetSheet(TextureManager.SheetCategory.tiles, 0).GetTotalNumberOfSpritesInCol(0, new Vector2(32, 32));
                    int tileKindIndex = RandomHelper.RandomInteger(0, tileKinds);
                    int tileSubKinds = Globals.TextureManager.GetSheet(TextureManager.SheetCategory.tiles, 0).GetTotalNumberOfSpritesInRow(tileKindIndex, new Vector2(32, 32));
                    int tileSubKindIndex = RandomHelper.RandomInteger(0, tileSubKinds);

                    tiles[x, y] = new Tile(new Vector2(x * Globals.tileSize.X, y * Globals.tileSize.Y), tileKindIndex + tileSubKindIndex);
                }
            }

            Console.WriteLine("Map Has Been Generated");
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
