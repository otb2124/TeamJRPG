using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace TeamJRPG
{

    [Serializable]
    [JsonObject(IsReference = true)]
    public class Mob : LiveEntity
    {


        public int mobID;


       


        [JsonConstructor]
        public Mob(Point mapPosition, int mobID) : base(mapPosition)
        {
            this.entityType = EntityType.mob;
            this.mobID = mobID;

            tileCollision = false;

            float collisionBoxWidth = Globals.tileSize.X / 2;
            float collisionBoxHeight = Globals.tileSize.Y / 2;
            float collisionBoxX = this.position.X + (Globals.tileSize.X - collisionBoxWidth) / 2;
            float collisionBoxY = this.position.Y;
            this.collisionBox = new System.Drawing.RectangleF(collisionBoxX, collisionBoxY, collisionBoxWidth, collisionBoxHeight);
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)collisionBoxWidth, (int)collisionBoxHeight, new Color(0, 0.5f, 0, 0.01f));



            this.path = null;

            SetTextures();
            SetAnimations();
        }






        public void SetTextures()
        {

            sprites = new Sprite[1];


            switch (mobID)
            {
                case 0:


                    name = "Bandit";

                    skinColor = Color.Red;
                    hairColor = RandomHelper.RandomColor();
                    eyeColor = RandomHelper.RandomColor();

                    this.bodySpriteSheet = Globals.textureManager.GetSheet(TextureManager.SheetCategory.character_bodies, 0);


                    sprites[0] = bodySpriteSheet.GetSprite(Vector2.Zero, new Vector2(32, 64));

                    defaultSpeed = 1.5f;
                    defaultSprintSpeed = 5f;
                    defaultSprintDuration = 5 * 60;

                    currentSpeed = defaultSpeed;
                    currentSprintSpeed = defaultSprintSpeed;
                    currentSprintDuration = defaultSprintDuration;


                    aggroDistance = new Vector2(3, 10);


                    characterIconBackGroundID = 0;
                    characterIconID = 0;
                    break;
            }



            
            

            SetIcons();


        }


        public void SetAnimations()
        {

            anims = new AnimationManager();

            switch (mobID)
            {
                case 0:
                    //idle
                    float frameSpeed = 0.2f;
                    AddAnimation(Direction.down, AnimationState.idle, 6, new Vector2(0, 0), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed, SpriteEffects.None);
                    AddAnimation(Direction.left, AnimationState.idle, 6, new Vector2(0, 64), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed, SpriteEffects.FlipHorizontally);
                    AddAnimation(Direction.right, AnimationState.idle, 6, new Vector2(0, 64), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed, SpriteEffects.None);
                    AddAnimation(Direction.up, AnimationState.idle, 6, new Vector2(0, 64 * 2), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed, SpriteEffects.None);

                    //walking
                    frameSpeed = 0.1f;
                    AddAnimation(Direction.down, AnimationState.walking, 4, new Vector2(0, 64 * 3), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed, SpriteEffects.None);
                    AddAnimation(Direction.left, AnimationState.walking, 4, new Vector2(0, 64 * 4), new Vector2(32 + 16, 64), frameSpeed, SpriteEffects.FlipHorizontally);
                    AddAnimation(Direction.right, AnimationState.walking, 4, new Vector2(0, 64 * 4), new Vector2(32 + 16, 64), frameSpeed, SpriteEffects.None);
                    AddAnimation(Direction.up, AnimationState.walking, 4, new Vector2(0, 64 * 5), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed, SpriteEffects.None);

                    //sprint
                    frameSpeed = 0.075f;
                    AddAnimation(Direction.down, AnimationState.sprinting, 4, new Vector2(0, 64 * 3), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed, SpriteEffects.None);
                    AddAnimation(Direction.left, AnimationState.sprinting, 4, new Vector2(0, 64 * 4), new Vector2(32 + 16, 64), frameSpeed, SpriteEffects.FlipHorizontally);
                    AddAnimation(Direction.right, AnimationState.sprinting, 4, new Vector2(0, 64 * 4), new Vector2(32 + 16, 64), frameSpeed, SpriteEffects.None);
                    AddAnimation(Direction.up, AnimationState.sprinting, 4, new Vector2(0, 64 * 5), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed, SpriteEffects.None);

                    //attack
                    frameSpeed = 0.1f;
                    this.attackAnimationDuration = (int)(frameSpeed * 4 * 60);
                    AddAnimation(Direction.left, AnimationState.attacking, 4, new Vector2(0, 64 * 6), new Vector2(32 + 16, 64), frameSpeed, SpriteEffects.FlipHorizontally);
                    AddAnimation(Direction.right, AnimationState.attacking, 4, new Vector2(0, 64 * 6), new Vector2(32 + 16, 64), frameSpeed, SpriteEffects.None);

                    //taking damage
                    frameSpeed = 0.15f;
                    this.tdamageAnimationDuration = (int)(frameSpeed * 4 * 60);
                    AddAnimation(Direction.left, AnimationState.takingDamage, 6, new Vector2(0, 64 * 7), new Vector2(32, 64), frameSpeed, SpriteEffects.FlipHorizontally);
                    AddAnimation(Direction.right, AnimationState.takingDamage, 6, new Vector2(0, 64 * 7), new Vector2(32, 64), frameSpeed, SpriteEffects.None);

                    //dies
                    frameSpeed = 0.15f;
                    AddAnimation(Direction.left, AnimationState.dead, 1, new Vector2(0, 64 * 8), new Vector2(32 + 16, 64), frameSpeed, SpriteEffects.FlipHorizontally);
                    AddAnimation(Direction.right, AnimationState.dead, 1, new Vector2(0, 64 * 8), new Vector2(32 + 16, 64), frameSpeed, SpriteEffects.None);
                    break;
            }


            this.currentBattleStatus = BattleStatus.live;
            this.currentStatus = Status.idle;
            this.direction = Direction.down;
        }


        


        public override void Update()
        {
            if (Globals.currentGameState == Globals.GameState.battleState)
            {
                anims.Update(new Tuple<LiveEntity.Direction, LiveEntity.AnimationState>(direction, animationState));

                if (currentStatus == Status.attacking)
                {
                    PerformAttackAnimation();
                }
                if (currentStatus == Status.takingDamage)
                {
                    PerformTakingDamageAnimation();
                }


                animationState = SwitchStatusToAnimation();
            }
            else
            {

                anims.Update(new Tuple<LiveEntity.Direction, LiveEntity.AnimationState>(direction, animationState));
                this.currentStatus = Status.idle;

                if(Globals.currentGameState != Globals.GameState.gameOverState)
                {
                    if (currentBattleStatus != BattleStatus.dead)
                    {
                        FollowInAggroRange(Globals.player, aggroDistance.X, aggroDistance.Y);
                    }
                    else
                    {
                        this.currentStatus = Status.dead;
                    }
                }
                
                

                HandleCollisions();

                this.collisionBox.Location = new System.Drawing.PointF(this.position.X + (Globals.tileSize.X - collisionBox.Width) / 2, this.position.Y + Globals.tileSize.Y / 2);

                base.Update();

                animationState = SwitchStatusToAnimation();
            }

        }


        public void HandleCollisions()
        {
            Entity collidedEntity = Globals.collisionManager.CheckEntityCollision(this);

            if (collidedEntity == Globals.player)
            {
                //touch
            }
        }


        public override void Draw()
        {
            drawPosition = new Vector2(position.X, position.Y - Globals.tileSize.Y);
            sprites[0].srcRect = anims.GetCurrent().GetCurrentFrame();
            sprites[0].effect = anims.GetCurrent().effect;

            base.Draw();
        }

    }
}
