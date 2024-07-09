using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;



namespace TeamJRPG
{
    public class GroupMember : LiveEntity
    {
        public bool isPlayer;
        public string name;
        public int[] TextureIDs;

        public GroupMember(Vector2 position) : base(position)
        {
            speed = 3.5f;
            tileCollision = true;

            float collisionBoxWidth = Globals.tileSize.X / 2;
            float collisionBoxHeight = Globals.tileSize.Y / 2;
            float collisionBoxX = this.position.X + (Globals.tileSize.X - collisionBoxWidth) / 2;
            float collisionBoxY = this.position.Y;
            this.collisionBox = new System.Drawing.RectangleF(collisionBoxX, collisionBoxY, collisionBoxWidth, collisionBoxHeight);
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)collisionBoxWidth, (int)collisionBoxHeight, new Color(0, 0.5f, 0, 0.01f));

            isPlayer = false;
            name = "BLANK_NAME";

            SetTextures();
            SetEquipment();
        }

        public void SetTextures()
        {
            texture = new Texture2D[3];
            TextureIDs = new int[5];

            //APPEARANCE SETTINGS
            TextureIDs[0] = 0;
            TextureIDs[1] = 1;
            TextureIDs[2] = 0;
            TextureIDs[3] = 0;
            TextureIDs[4] = 0;
            skinColor = RandomHelper.RandomColor();


            texture[0] = Globals.assetSetter.textures[Globals.assetSetter.CHARACHTER_BODIES][TextureIDs[0]][0];
            texture[1] = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_EYES][TextureIDs[1]][0];
            texture[2] = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_MOUTHS][TextureIDs[2]][0];
            characterIcon = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_ICONS][TextureIDs[3]][0];
            battleTexture = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_BATTLESPRITES][TextureIDs[4]][0];
        }

        public void SetEquipment()
        {
            weapon1 = new Weapon(0);
            weapon2 = new Weapon(0);

            armor = new Armor[9];
            armor[HELMET] = new Armor(0);
            armor[HELMET].slotType = Armor.SlotType.helmet;
            armor[CHESTPLATE] = new Armor(0);
            armor[CHESTPLATE].slotType = Armor.SlotType.chestplate;
            armor[BOOTS] = new Armor(0);
            armor[BOOTS].slotType = Armor.SlotType.boots;
            armor[GLOVES] = new Armor(0);
            armor[GLOVES].slotType = Armor.SlotType.gloves;
            armor[CAPE] = new Armor(0);
            armor[CAPE].slotType = Armor.SlotType.cape;
            armor[BELT] = new Armor(0);
            armor[BELT].slotType = Armor.SlotType.belt;
            armor[NECKLACE] = new Armor(0);
            armor[NECKLACE].slotType = Armor.SlotType.necklace;
            armor[RING1] = new Armor(0);
            armor[RING1].slotType = Armor.SlotType.ring;
            armor[RING2] = new Armor(0);
            armor[RING2].slotType = Armor.SlotType.ring;

            for (int i = 0; i < armor.Length; i++)
            {
                armor[i].SetTexture();
            }



        }


        


        public override void Update()
        {
            HandleCollisions();

            previousPosition = new Point((int)(position.X / Globals.tileSize.X), (int)(position.Y / Globals.tileSize.X));

            if (isPlayer)
            {
                if (Globals.inputManager.IsKeyPressed(Keys.W)) { Move(new Vector2(0, -speed), Direction.up); }
                else if (Globals.inputManager.IsKeyPressed(Keys.S)) { Move(new Vector2(0, speed), Direction.down); }
                else if (Globals.inputManager.IsKeyPressed(Keys.A)) { Move(new Vector2(-speed, 0), Direction.left); }
                else if (Globals.inputManager.IsKeyPressed(Keys.D)) { Move(new Vector2(speed, 0), Direction.right); }

                HandleInterractions();
                CheckPlayerChange();
            }
            else
            {
                FollowPreviousMember();
            }

            this.collisionBox.Location = new System.Drawing.PointF(this.position.X + (Globals.tileSize.X - collisionBox.Width) / 2, this.position.Y + Globals.tileSize.Y / 2);

            HandleDirectionTextures();


            
            base.Update();
        }

        private void Move(Vector2 delta, Direction direction)
        {
            position += delta;
            this.direction = direction;
        }

        public void HandleCollisions()
        {
            Entity collidedEntity = Globals.collisionManager.CheckEntityCollision(this);

            if (collidedEntity != null)
            {
                // Object picking for autopicking
                if (collidedEntity is Object obj)
                {
                    if (obj.type == Object.ObjectType.autoPickable)
                    {
                        for (global::System.Int32 i = 0; i < obj.inventory.Count; i++)
                        {
                            Globals.group.AddToInventory(obj.inventory[i]);
                        }
                        Globals.entities.Remove(collidedEntity);
                    }
                }
            }

            if ((collidedEntity != null && collidedEntity.entityCollision) && entityCollision)
            {
                if (collidedEntity is Mob || collidedEntity is GroupMember)
                {
                    //skip collision
                }
                else
                {
                    StopMovement();
                }
            }

            if (Globals.collisionManager.CheckTileCollision(this) && tileCollision)
            {
                StopMovement();
            }
        }

        public void HandleInterractions()
        {
            Entity interractedEntity = Globals.collisionManager.CheckEntityInterraction(this);

            if (interractedEntity != null)
            {
                // Object picking
                if (interractedEntity is Object obj)
                {
                    if (obj.type == Object.ObjectType.pickable)
                    {
                        if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Enter))
                        {
                            for (global::System.Int32 i = 0; i < obj.inventory.Count; i++)
                            {
                                Globals.group.AddToInventory(obj.inventory[i]);
                            }
                            Globals.entities.Remove(interractedEntity);
                        }
                    }
                }
            }
        }

        private void FollowPreviousMember()
        {
            int index = Globals.group.members.IndexOf(this);
            int newIndex = (index - 1 + Globals.group.members.Count) % Globals.group.members.Count;
            Entity previousMember = Globals.group.members[newIndex];
            MaintainDistance(previousMember);
        }

        private void MaintainDistance(Entity previousMember)
        {
            float distance = Vector2.Distance(position, previousMember.position);
            if (distance > Globals.tileSize.X * 10)
            {
                // Teleport to the previous member if the distance is greater than 10 tiles
                position = previousMember.position;
                path = new Queue<Point>();
            }
            else if (distance > Globals.tileSize.X)
            {
                // Follow the previous member
                Follow(previousMember);
            }
        }

        private void StopMovement()
        {
            switch (direction)
            {
                case Direction.up:
                    position.Y += speed; break;
                case Direction.down:
                    position.Y -= speed; break;
                case Direction.right:
                    position.X -= speed; break;
                case Direction.left:
                    position.X += speed; break;
            }
        }

        private void HandleDirectionTextures()
        {
            switch (direction)
            {
                case Direction.up:
                    texture[0] = Globals.assetSetter.textures[Globals.assetSetter.CHARACHTER_BODIES][TextureIDs[0]][1];
                    texture[1] = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_EYES][TextureIDs[1]][1];
                    texture[2] = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_MOUTHS][TextureIDs[2]][1]; 
                    break;
                case Direction.down:
                    texture[0] = Globals.assetSetter.textures[Globals.assetSetter.CHARACHTER_BODIES][TextureIDs[0]][0]; 
                    texture[1] = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_EYES][TextureIDs[1]][0];
                    texture[2] = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_MOUTHS][TextureIDs[2]][0]; 
                    break;
                case Direction.right:
                    texture[0] = Globals.assetSetter.textures[Globals.assetSetter.CHARACHTER_BODIES][TextureIDs[0]][2];
                    texture[1] = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_EYES][TextureIDs[1]][2];
                    texture[2] = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_MOUTHS][TextureIDs[2]][2]; 
                    break;
                case Direction.left:
                    texture[0] = Globals.assetSetter.textures[Globals.assetSetter.CHARACHTER_BODIES][TextureIDs[0]][3];
                    texture[1] = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_EYES][TextureIDs[1]][3];
                    texture[2] = Globals.assetSetter.textures[Globals.assetSetter.CHARACTER_MOUTHS][TextureIDs[2]][3]; 
                    break;
            }
        }


        private void CheckPlayerChange()
        {
            if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Q))
            {
                SetPrevMemberToPlayer();
            }
            else if (Globals.inputManager.IsKeyPressedAndReleased(Keys.E))
            {
                SetNextMemberToPlayer();
            }
        }

        public void SetPrevMemberToPlayer()
        {
            ChangePlayer(-1);
        }

        public void SetNextMemberToPlayer()
        {
            ChangePlayer(1);
        }

        private void ChangePlayer(int direction)
        {
            int currentIndex = Globals.group.members.IndexOf(this);
            int newIndex = (currentIndex + direction + Globals.group.members.Count) % Globals.group.members.Count;
            SetPlayer(Globals.group.members[newIndex]);
        }

        public void SetPlayer(GroupMember newPlayer)
        {
            if (newPlayer != this)
            {
                this.isPlayer = false;
                newPlayer.isPlayer = true;
                Globals.player = newPlayer;

                // Uncomment the following line if needed
                // Globals.uimanager.drawPointer();
            }

            for (int i = 0; i < Globals.group.members.Count; i++)
            {
                Globals.group.members[i].path = new Queue<Point>();
            }

        }





        public override void Draw()
        {
            drawPosition = new Vector2(position.X, position.Y - Globals.tileSize.Y);
            base.Draw();
        }
    }
}
