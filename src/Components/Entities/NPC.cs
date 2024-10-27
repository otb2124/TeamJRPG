using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;


namespace TeamJRPG
{

    [Serializable]
    [JsonObject(IsReference = true)]
    public class NPC : LiveEntity
    {


        public int npcID;

        
        [JsonIgnore]
        public System.Drawing.RectangleF interractionBox;
        [JsonIgnore]
        public Texture2D interractionBoxTexture;



        [JsonConstructor]
        public NPC(Point mapPosition, int npcID) : base(mapPosition)
        {
            this.entityType = EntityType.npc;
            this.npcID = npcID;

            tileCollision = false;

            float collisionBoxWidth = Globals.tileSize.X / 2;
            float collisionBoxHeight = Globals.tileSize.Y / 2;
            float collisionBoxX = this.position.X + (Globals.tileSize.X - collisionBoxWidth) / 2;
            float collisionBoxY = this.position.Y;
            this.collisionBox = new System.Drawing.RectangleF(collisionBoxX, collisionBoxY, collisionBoxWidth, collisionBoxHeight);
            this.collisionTexture = Globals.assetSetter.CreateSolidColorTexture((int)collisionBoxWidth, (int)collisionBoxHeight, new Color(0, 0.5f, 0, 0.01f));

            float interractionBoxWidth = this.collisionBox.Width + 2 * Globals.tileSize.X;
            float interractionBoxHeight = this.collisionBox.Height + 2 * Globals.tileSize.Y;
            float interractionBoxX = this.collisionBox.X - Globals.tileSize.X;
            float interractionBoxY = this.collisionBox.Y - Globals.tileSize.Y;
            this.interractionBox = new System.Drawing.RectangleF(interractionBoxX, interractionBoxY, interractionBoxWidth, interractionBoxHeight);
            this.interractionBoxTexture = Globals.assetSetter.CreateSolidColorTexture((int)this.interractionBox.Width, (int)this.interractionBox.Height, new Color(0, 0, 0.1f, 0.1f));

            this.path = null;

            SetTextures();
            SetAnimations();
        }



        public void SetTextures()
        {

            sprites = new Sprite[1];


            switch (npcID)
            {
                case 0:

                    name = "Carl";


                    skinColor = Color.Gray;
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
                    characterIconID = 0;
                    characterIconBackGroundID = 0;

                    currentDialogueId = 0;

                    break;
            }






            SetIcons();


        }


        public void SetAnimations()
        {

            anims = new AnimationManager();

            switch (npcID)
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
                    frameSpeed = 0.2f;
                    this.attackAnimationDuration = (int)(frameSpeed * 4 * 60);
                    AddAnimation(Direction.left, AnimationState.attacking, 4, new Vector2(0, 64 * 6), new Vector2(32 + 16, 64), frameSpeed, SpriteEffects.FlipHorizontally);
                    AddAnimation(Direction.right, AnimationState.attacking, 4, new Vector2(0, 64 * 6), new Vector2(32 + 16, 64), frameSpeed, SpriteEffects.None);

                    this.currentStatus = Status.idle;
                    this.direction = Direction.down;
                    break;
            }



            this.currentStatus = Status.idle;
            this.direction = Direction.down;
        }



        public void Interract()
        {
            if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Enter))
            {

                

                if (Globals.currentGameState == Globals.GameState.playState)
                {

                    Entity talkingEntity = this;

                    switch (npcID)
                    {
                        case 0:

                            currentDialogueId = 0;

                            talkingEntity = this;

                            Globals.camera.focusEntity = this;

                            break;
                    }

                    Globals.player.interractedEntity = talkingEntity;
                    Globals.currentGameState = Globals.GameState.dialogueState;
                    Globals.uiManager.currentMenuState = UIManager.MenuState.dialogue;
                    Globals.uiManager.MenuStateNeedsChange = true;
                    Globals.camera.FocusOnEntity();

                }

                


            }
        }
        

        



        public override void Update()
        {

            anims.Update(new Tuple<LiveEntity.Direction, LiveEntity.AnimationState>(direction, animationState));
            this.currentStatus = Status.idle;


            HandleCollisions();

            this.collisionBox.Location = new System.Drawing.PointF(this.position.X + (Globals.tileSize.X - collisionBox.Width) / 2, this.position.Y + Globals.tileSize.Y / 2);

            base.Update();  

            animationState = SwitchStatusToAnimation();
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
            sprites[0].srcRect = anims.GetCurrent().GetCurrentFrame();
            sprites[0].effect = anims.GetCurrent().effect;

            base.Draw();
        }

    }
}
