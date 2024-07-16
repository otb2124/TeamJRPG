using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace TeamJRPG
{
    [Serializable]
    public class Tile
    {
        public int id;

        [JsonIgnore]
        public Vector2 position;


        public Point mapPosition;


        [JsonIgnore]
        public Sprite sprite;

        [JsonIgnore]
        public bool collision;

        [JsonIgnore]
        public System.Drawing.RectangleF collisionBox;






        [JsonConstructor]
        public Tile(Point mapPosition, int id)
        {
            this.id = id;
            this.mapPosition = mapPosition;
            this.position = mapPosition.ToVector2()*Globals.tileSize;
            SetTileData();
            this.collisionBox = new System.Drawing.RectangleF(position.X, position.Y, Globals.tileSize.X, Globals.tileSize.Y);
        }




        public void SetTileData()
        {
            Vector2 sheetPos = Vector2.Zero;

            // grass
            if (id >= 0 && id < 5)
            {
                sheetPos = new Vector2(id * 32, 0);
            }
            // water
            else if (id == 5)
            {
                sheetPos = new Vector2(0, 32);
                collision = true;
            }
            //examples
            if(id == 6 || id == 7)
            {
                sheetPos = new Vector2((id-6)*32, 64);
            }


            this.sprite = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.tiles, 0, sheetPos, new Vector2(32, 32));
        }

        public void Draw()
        {
            Color color = Color.White;

            if (collision && Globals.currentGameMode == Globals.GameMode.debugmode)
            {
                color = Color.Red;
            }

            sprite.Draw(position, color, 0, Vector2.Zero, new Vector2(Globals.gameScale, Globals.gameScale), SpriteEffects.None, 0f);
        }
    }
}
