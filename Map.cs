using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace TeamJRPG
{

    
    public class Map
    {

        public Room[] rooms;
        


        public Map()
        {
            rooms = new Room[Globals.mapSize];
        }


        public void Load()
        {
            for (int x = 0; x < rooms.Length; x++)
            {

                rooms[x] = new Room(new Vector2(x * Globals.tileSize.X * Globals.roomSize.X, 0));
                
            }
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
