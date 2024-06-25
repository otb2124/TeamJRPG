using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{

    
    public class Map
    {

        public Room[] rooms;
        


        public Map()
        {
            rooms = new Room[10];
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
