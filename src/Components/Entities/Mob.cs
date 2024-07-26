using Microsoft.Xna.Framework;
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

                    this.bodySpriteSheet = Globals.TextureManager.GetSheet(TextureManager.SheetCategory.character_bodies, 0);


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
                    float frameSpeed = 0.175f;
                    AddAnimation(Direction.down, AnimationState.idle, 4, new Vector2(0, 0), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed);
                    AddAnimation(Direction.left, AnimationState.idle, 4, new Vector2(0, 64), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed);
                    AddAnimation(Direction.right, AnimationState.idle, 4, new Vector2(0, 64 * 2), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed);
                    AddAnimation(Direction.up, AnimationState.idle, 4, new Vector2(0, 64 * 3), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed);

                    //walking
                    frameSpeed = 0.1f;
                    AddAnimation(Direction.down, AnimationState.walking, 4, new Vector2(0, 64 * 4), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed);
                    AddAnimation(Direction.left, AnimationState.walking, 4, new Vector2(0, 64 * 5), new Vector2(32 + 16, 64), frameSpeed);
                    AddAnimation(Direction.right, AnimationState.walking, 4, new Vector2(0, 64 * 6), new Vector2(32 + 16, 64), frameSpeed);
                    AddAnimation(Direction.up, AnimationState.walking, 4, new Vector2(0, 64 * 7), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed);

                    //sprint
                    frameSpeed = 0.075f;
                    AddAnimation(Direction.down, AnimationState.sprinting, 4, new Vector2(0, 64 * 4), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed);
                    AddAnimation(Direction.left, AnimationState.sprinting, 4, new Vector2(0, 64 * 5), new Vector2(32 + 16, 64), frameSpeed);
                    AddAnimation(Direction.right, AnimationState.sprinting, 4, new Vector2(0, 64 * 6), new Vector2(32 + 16, 64), frameSpeed);
                    AddAnimation(Direction.up, AnimationState.sprinting, 4, new Vector2(0, 64 * 7), DEFAULT_HUMANOID_BODY_SPRITE_SIZE, frameSpeed);
                    break;
            }

            

            this.currentStatus = Status.idle;
            this.direction = Direction.down;
        }


        


        public override void Update()
        {
            if (Globals.currentGameState == Globals.GameState.battle)
            {
                anims.Update(new Tuple<LiveEntity.Direction, LiveEntity.AnimationState>(direction, animationState));


                this.currentStatus = Status.idle;

                animationState = SwitchStatusToAnimation();
            }
            else
            {

                anims.Update(new Tuple<LiveEntity.Direction, LiveEntity.AnimationState>(direction, animationState));
                this.currentStatus = Status.idle;


                FollowInAggroRange(Globals.player, aggroDistance.X, aggroDistance.Y);

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
                Console.WriteLine("TOUCH!");
            }
        }


        public override void Draw()
        {
            drawPosition = new Vector2(position.X, position.Y - Globals.tileSize.Y);
            sprites[0].srcRect = anims.GetCurrentFrame();

            base.Draw();
        }

    }
}
