﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace TeamJRPG
{
    [Serializable]
    public class Tile
    {
        public int id;
        public Vector2 position;
        public Sprite sprite;
        public bool collision;
        public System.Drawing.RectangleF collisionBox;
        public Texture2D collisionTexture;

        public Tile(Vector2 position, int id)
        {
            this.id = id;
            this.position = position;
            SetTileData();
            this.collisionBox = new System.Drawing.RectangleF(position.X, position.Y, Globals.tileSize.X, Globals.tileSize.Y);
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)Globals.tileSize.X, (int)Globals.tileSize.Y, new Color(0, 0, 0.5f, 0.01f));
        }


        public void SetTileData()
        {
            Vector2 sheetPos = Vector2.Zero;

            //grass
            if (id >= 0 && id < 5)
            {
                sheetPos = new Vector2(0 + (id*32), 0);
            }
            //water
            else if(id == 5)
            {
                sheetPos = new Vector2(0, 32);
                collision = true;
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