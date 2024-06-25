using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{

    
    public class Map
    {

        public Room[,] rooms;
        


        public Map()
        {
            rooms = new Room[10, 10];
        }


        public void Init()
        {
            for (int x = 0; x < rooms.GetLength(0); x++)
            {
                for (int y = 0; y < rooms.GetLength(1); y++)
                {
                    rooms[x, y] = new Room(new Vector2(x * Globals.tileSize.X * Globals.roomSize.X, y * Globals.roomSize.Y * Globals.tileSize.Y));
                }
            }
        }


        public void Draw()
        {
            for (int x = 0; x < rooms.GetLength(0); x++)
            {
                for (int y = 0; y < rooms.GetLength(1); y++)
                {
                    rooms[x, y].Draw();
                }
            }
        }
    }
}
