using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace TeamJRPG
{

    [Serializable]
    public class Map
    {

        public Room[] rooms;
        public int mapSize = 3;


        public Map()
        {
            rooms = new Room[mapSize];
        }


        public void Load()
        {
            for (int x = 0; x < rooms.Length; x++)
            {

                rooms[x] = new Room(new Vector2(x * Globals.tileSize.X * 10, 0));
                
            }
        }



        public void CalculateMapBounds(out Vector2 minBounds, out Vector2 maxBounds)
        {
            if (rooms.Length == 0)
            {
                minBounds = Vector2.Zero;
                maxBounds = Vector2.Zero;
                return;
            }

            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            foreach (var room in rooms)
            {
                foreach (var tile in room.tiles)
                {
                    var position = tile.position;
                    if (position.X < minX) minX = position.X;
                    if (position.Y < minY) minY = position.Y;
                    if (position.X > maxX) maxX = position.X + Globals.tileSize.X;
                    if (position.Y > maxY) maxY = position.Y + Globals.tileSize.Y/2;
                }
            }

            minBounds = new Vector2(minX, minY);
            maxBounds = new Vector2(maxX, maxY);
        }


        public void Draw()
        {
            for (int x = 0; x < rooms.Length; x++)
            {
                rooms[x].Draw();
            }
        }
    }
}
