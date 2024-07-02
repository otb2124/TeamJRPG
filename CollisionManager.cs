

using Microsoft.Xna.Framework;
using System.Drawing;

namespace TeamJRPG
{
    public class CollisionManager
    {


        public bool CheckTileCollision(Entity entity)
        {
                // Loop through all tiles in the current room
                for (int x = 0; x < Globals.map.tiles.GetLength(0); x++)
                {
                    for (int y = 0; y < Globals.map.tiles.GetLength(1); y++)
                    {
                        // Check if the current tile is collidable
                        if (Globals.map.tiles[x, y].collision)
                        {
                            // Check for intersection with the entity's tileCollision box
                            if (entity.collisionBox.IntersectsWith(Globals.map.tiles[x, y].collisionBox))
                            {
                                return true;
                            }
                        }
                    }
                
            }

            return false;
        }


        public Entity CheckEntityCollision(Entity entity)
        {

            ConstrainEntityToMap(entity);

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



        public void ConstrainEntityToMap(Entity entity)
        {
            // Get map boundaries
            float mapWidth = Globals.map.mapSize.X * Globals.tileSize.X;
            float mapHeight = Globals.map.mapSize.Y * Globals.tileSize.Y;

            // Constrain entity's position within the map boundaries
            entity.position.X = MathHelper.Clamp(entity.position.X, 0, mapWidth - entity.collisionBox.Width);
            entity.position.Y = MathHelper.Clamp(entity.position.Y, 0, mapHeight - entity.collisionBox.Height);

            // Update the collision box position
            entity.collisionBox.X = (int)entity.position.X;
            entity.collisionBox.Y = (int)entity.position.Y;
        }



    }
}



