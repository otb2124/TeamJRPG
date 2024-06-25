using Microsoft.Xna.Framework;



namespace TeamJRPG
{
    public class Room
    {


        public Tile[,] tiles;
        public Vector2 position;


        public Room(Vector2 position)
        {
            tiles = new Tile[Globals.roomSize.X, Globals.roomSize.Y];
            this.position = position;
            Init();
        }



        public void Init()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    // Check if the current tile is a border tile
                    bool isBorderTile = x == 0 || y == 0 || x == tiles.GetLength(0) - 1 || y == tiles.GetLength(1) - 1;

                    if (isBorderTile)
                    {
                        // Set border tile texture
                        tiles[x, y] = new Tile(new Vector2(x * Globals.tileSize.X + position.X, y * Globals.tileSize.Y + position.Y), Globals.assetSetter.textures[0][1][0]);
                    }
                    else
                    {
                        // Set regular tile texture (assuming index 0 is for regular tiles)
                        tiles[x, y] = new Tile(new Vector2(x * Globals.tileSize.X + position.X, y * Globals.tileSize.Y + position.Y), Globals.assetSetter.textures[0][0][0]);
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
