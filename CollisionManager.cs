using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class CollisionManager
    {


        public bool CheckCollision(Entity entity)
        {
            // Loop through all rooms
            for (int i = 0; i < Globals.map.rooms.Length; i++)
            {
                // Loop through all tiles in the current room
                for (int x = 0; x < Globals.map.rooms[i].tiles.GetLength(0); x++)
                {
                    for (int y = 0; y < Globals.map.rooms[i].tiles.GetLength(1); y++)
                    {
                        // Check if the current tile is collidable
                        if (Globals.map.rooms[i].tiles[x, y].collision)
                        {
                            // Check for intersection with the entity's collision box
                            if (entity.collisionBox.IntersectsWith(Globals.map.rooms[i].tiles[x, y].collisionBox))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}



