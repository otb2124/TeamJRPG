using Microsoft.Xna.Framework;
using System.Drawing;



namespace TeamJRPG
{
    public class CollisionManager
    {


        public RectangleF worldBounds;




        public void Load()
        {
            worldBounds = new RectangleF(0, 0, Globals.currentMap.mapSize.X * Globals.tileSize.X, Globals.currentMap.mapSize.Y * Globals.tileSize.Y);
        }





        public bool CheckTileCollision(Entity entity)
        {


            if (!worldBounds.Contains(entity.collisionBox))
            {
                return true;
            }

            // Loop through all tiles in the current room
            for (int x = 0; x < Globals.currentMap.tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Globals.currentMap.tiles.GetLength(1); y++)
                {
                    // Check if the current tile is collidable
                    if (Globals.currentMap.tiles[x, y].collision)
                    {
                        // Check for intersection with the entity's tileCollision box
                        if (entity.collisionBox.IntersectsWith(Globals.currentMap.tiles[x, y].collisionBox))
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



            for (int i = 0; i < Globals.currentEntities.Count; i++)
            {
                if (entity.collisionBox.IntersectsWith(Globals.currentEntities[i].collisionBox) && entity != Globals.currentEntities[i])
                {
                    return Globals.currentEntities[i];
                }
            }

            return null;
        }



        public Entity CheckEntityInterraction(Entity entity)
        {
            for (int i = 0; i < Globals.currentEntities.Count; i++)
            {
                if (Globals.currentEntities[i] != entity)
                {
                    if (Globals.currentEntities[i] is Object obj)
                    {
                        if (entity.collisionBox.IntersectsWith(obj.interractionBox))
                        {
                            return obj;
                        }
                    }

                    if (Globals.currentEntities[i] is NPC npc)
                    {
                        if (entity.collisionBox.IntersectsWith(npc.interractionBox))
                        {
                            return npc;
                        }
                    }
                }
            }

            return null;
        }



        public void ConstrainEntityToMap(Entity entity)
        {
            // Get currentMap boundaries
            float mapWidth = Globals.currentMap.mapSize.X * Globals.tileSize.X;
            float mapHeight = Globals.currentMap.mapSize.Y * Globals.tileSize.Y;

            // Constrain entity's position within the currentMap boundaries
            entity.position.X = MathHelper.Clamp(entity.position.X, 0, mapWidth - entity.collisionBox.Width);
            entity.position.Y = MathHelper.Clamp(entity.position.Y, 0, mapHeight - entity.collisionBox.Height);

            // Update the collision box position
            entity.collisionBox.X = (int)entity.position.X;
            entity.collisionBox.Y = (int)entity.position.Y;
        }



    }
}



