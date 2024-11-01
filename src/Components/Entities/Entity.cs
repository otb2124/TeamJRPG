﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace TeamJRPG
{


    [Serializable]
    [JsonObject(IsReference = true)]
    public class Entity
    {

        public enum EntityType { groupMember, obj, mob, npc }
        public EntityType entityType;

        public Point mapPosition;

        [JsonIgnore]
        public Vector2 position;

        [JsonIgnore]
        public Vector2 drawPosition;

        [JsonIgnore]
        public Sprite[] sprites;

        [JsonIgnore]
        public AnimationManager anims;

        [JsonIgnore]
        public Color drawColor;







        [JsonIgnore]
        public System.Drawing.RectangleF collisionBox;

        [JsonIgnore]
        public Texture2D collisionTexture;

  

        public bool entityCollision;
        public bool tileCollision;


        public List<Item> inventory;



        public Entity(Point mapPosition)
        {
            this.mapPosition = mapPosition;
            this.position = mapPosition.ToVector2() * Globals.tileSize;

            this.tileCollision = false;
            this.entityCollision = true;

            float collisionBoxWidth = Globals.tileSize.X / 2;
            float collisionBoxHeight = Globals.tileSize.Y / 2;
            float collisionBoxX = this.position.X;
            float collisionBoxY = this.position.Y;
            this.collisionBox = new System.Drawing.RectangleF(collisionBoxX, collisionBoxY, collisionBoxWidth, collisionBoxHeight);
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)collisionBoxWidth, (int)collisionBoxHeight, new Color(0.5f, 0, 0, 0.01f));

            this.drawColor = Color.White;

            this.inventory = new List<Item>();
        }


        public virtual void Update()
        {
            
        }


        




        public void AddToInventory(Item item)
        {

            bool hasItem = false;

            if (item.IsStackable)
            {
                foreach (var invItem in inventory)
                {
                    if (invItem.name == item.name)
                    {
                        hasItem = true;
                        invItem.amount+=item.amount;
                        break;
                    }
                }

            }


            if (!item.IsStackable || !hasItem)
            {
                inventory.Add(item);
            }

        }






        public virtual void Draw()
        {
            mapPosition = GetMapPos();

            Vector2 scale = new Vector2(Globals.gameScale, Globals.gameScale);

            for (int i = 0; i < sprites.Length; i++)
            {

                if (Globals.currentGameMode == Globals.GameMode.debugMode)
                {
                    drawColor = Color.Red;
                }

                sprites[i].Draw(drawPosition, drawColor, 0, Vector2.Zero, scale, 0);
            }


            

        }




        public Point GetMapPos()
        {
            Vector2 mapPos = position / Globals.tileSize;
            return mapPos.ToPoint();
        }
    }
}
