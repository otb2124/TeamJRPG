using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;



namespace TeamJRPG
{
    [Serializable]
    [JsonObject(IsReference = true)]
    public class GroupMember : LiveEntity
    {
        public bool isPlayer;
        public string name;



        public GroupMember(Vector2 position) : base(position)
        {
            this.entityType = EntityType.groupMember;
            defaultSpeed = 3.5f;
            defaultSprintSpeed = 5f;
            defaultSprintDuration = 5 * 60;

            currentSpeed = defaultSpeed;
            currentSprintSpeed = defaultSprintSpeed;
            currentSprintDuration = defaultSprintDuration;

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
            SetAnimations();
            SetEquipment();
        }


        public void SetAnimations()
        {
            anims = new AnimationManager();

            AddAnimation(Direction.down, AnimationState.idle, 4, new Vector2(0, 0), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, 0.1f);
            AddAnimation(Direction.left, AnimationState.idle, 4, new Vector2(0, 64), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, 0.1f);
            AddAnimation(Direction.right, AnimationState.idle, 4, new Vector2(0, 64*2), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, 0.1f);
            AddAnimation(Direction.up, AnimationState.idle, 4, new Vector2(0, 64*3), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, 0.1f);

            AddAnimation(Direction.down, AnimationState.walking, 4, new Vector2(0, 64 * 4), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, 0.1f);
            AddAnimation(Direction.left, AnimationState.walking, 4, new Vector2(0, 64 * 5), new Vector2(32+16, 64), 0.1f);
            AddAnimation(Direction.right, AnimationState.walking, 4, new Vector2(0, 64 * 6), new Vector2(32+16, 64), 0.1f);
            AddAnimation(Direction.up, AnimationState.walking, 4, new Vector2(0, 64 * 7), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, 0.1f);

            AddAnimation(Direction.down, AnimationState.sprinting, 4, new Vector2(0, 64 * 4), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, 0.1f);
            AddAnimation(Direction.left, AnimationState.sprinting, 4, new Vector2(0, 64 * 5), new Vector2(32 + 16, 64), 0.1f);
            AddAnimation(Direction.right, AnimationState.sprinting, 4, new Vector2(0, 64 * 6), new Vector2(32 + 16, 64), 0.1f);
            AddAnimation(Direction.up, AnimationState.sprinting, 4, new Vector2(0, 64 * 7), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, 0.1f);

            this.currentStatus = Status.idle;
            this.direction = Direction.down;
        }



        


        public void SetTextures()
        {
            skinColor = RandomHelper.RandomColor();
            hairColor = RandomHelper.RandomColor();
            eyeColor = RandomHelper.RandomColor();

            this.bodySpriteSheet = Globals.TextureManager.GetSheet(TextureManager.SheetCategory.character_bodies, 0);


            sprites = new Sprite[1];
            sprites[0] = bodySpriteSheet.GetSprite(Vector2.Zero, new Vector2(32, 64));

            SetIcons(0, 0);
        }


        public void SetIcons(int charIconId, int charIconBGId)
        {
            this.characterIconID = charIconId;
            this.characterIconBackGroundID = charIconBGId;
            characterIcon = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.entity_icons, 0, new Vector2(32*this.characterIconID, 0), new Vector2(64, 64));
            backGroundIcon = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.entity_icon_backgrounds, 0, new Vector2(32 * this.characterIconBackGroundID, 0), new Vector2(32, 32));
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
           
            anims.Update(new Tuple<LiveEntity.Direction, LiveEntity.AnimationState>(direction, animationState));

            HandleCollisions();

            previousPosition = new Point((int)(position.X / Globals.tileSize.X), (int)(position.Y / Globals.tileSize.X));

            this.currentStatus = Status.idle;

            if (isPlayer)
            {
                if (Globals.inputManager.IsKeyPressed(Keys.W)) { Move(new Vector2(0, -currentSpeed), Direction.up); }
                else if (Globals.inputManager.IsKeyPressed(Keys.S)) { Move(new Vector2(0, currentSpeed), Direction.down); }
                else if (Globals.inputManager.IsKeyPressed(Keys.A)) { Move(new Vector2(-currentSpeed, 0), Direction.left); }
                else if (Globals.inputManager.IsKeyPressed(Keys.D)) { Move(new Vector2(currentSpeed, 0), Direction.right); }

                if (currentStatus == Status.walking)
                {
                    if (Globals.inputManager.IsKeyPressed(Keys.LeftShift)) { Sprint(); } else { UnSprint(); }
                }
                else
                {
                    UnSprint();
                }

                if (!Globals.inputManager.IsKeyPressed(Keys.LeftShift)) { UnSprint(); }

                HandleInterractions();
            }
            else
            {
                FollowPreviousMember();
            }

            this.collisionBox.Location = new System.Drawing.PointF(this.position.X + (Globals.tileSize.X - collisionBox.Width) / 2, this.position.Y + Globals.tileSize.Y / 2);


            base.Update();

            animationState = SwitchStatusToAnimation();
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
            LiveEntity previousMember = Globals.group.members[newIndex];
            MaintainDistance(previousMember);
        }

        private void MaintainDistance(LiveEntity previousMember)
        {
            float distance = Vector2.Distance(position, previousMember.position);
            if (distance > Globals.tileSize.X * 20)
            {
                // Teleport to the previous member if the distance is greater than 10 tiles
                position = previousMember.position;
                path = new Queue<Point>();
            }
            else if (distance > Globals.tileSize.X)
            {
                // Follow the previous member
                Follow(previousMember);
                this.currentStatus = Status.walking;
            }
        }

        private void StopMovement()
        {
            switch (direction)
            {
                case Direction.up:
                    position.Y += currentSpeed; break;
                case Direction.down:
                    position.Y -= currentSpeed; break;
                case Direction.right:
                    position.X -= currentSpeed; break;
                case Direction.left:
                    position.X += currentSpeed; break;
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
            drawPosition = new Vector2(position.X, position.Y-Globals.tileSize.Y);
            sprites[0].srcRect = anims.GetCurrentFrame();

            base.Draw();
            
        }
    }
}
