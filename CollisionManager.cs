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


        public bool CheckTileCollision(Entity entity)
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


        public Entity CheckEntityCollision(Entity entity)
        {

            for (int i = 0; i < Globals.entities.Count; i++)
            {
                if (entity.collisionBox.IntersectsWith(Globals.entities[i].collisionBox) && entity != Globals.entities[i])
                {
                    return Globals.entities[i];
                }
            }

            return null;
        }


        public Entity CheckEntityInterraction(Entity entity)
        {
            for (int i = 0; i < Globals.entities.Count; i++)
            {
                if (Globals.entities[i] != entity)
                {
                    if (Globals.entities[i] is Object obj)
                    {
                        if (entity.collisionBox.IntersectsWith(obj.interractionBox))
                        {
                            return obj;
                        }
                    }

                    /*if (Globals.entities[i] is NPC npc)
                    {
                        if (entity.collisionBox.IntersectsWith(npc.interractionBox))
                        {
                            return npc;
                        }
                    }*/
                }
            }

            return null;
        }


    }
}



