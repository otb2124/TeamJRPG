using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TeamJRPG
{
    [Serializable]
    [JsonObject(IsReference = true)]
    public class LiveEntity : Entity
    {
        // sprites
        [JsonIgnore]
        public SpriteSheet bodySpriteSheet;
        [JsonIgnore]
        public readonly Vector2 DEFAULT_HUMANOID_BODY_SPRITE_SIZE = new Vector2(32, 64);
        [JsonIgnore]
        public Sprite characterIcon;
        [JsonIgnore]
        public Sprite backGroundIcon;

        public int bodySpriteSheetID;
        public int characterIconID;
        public int characterIconBackGroundID;



        // colors
        public Color skinColor;
        public Color hairColor;
        public Color eyeColor;

        // states
        public enum Status { idle, walking, sprinting };

        public Status currentStatus;

        // animations
        public enum AnimationState { idle, walking, sprinting };
        public AnimationState animationState;

        // body movement
        public float currentSpeed;
        public float defaultSpeed;

        // dodge attributes
        public float currentSprintSpeed;
        public float defaultSprintSpeed;
        public bool IsSprinting = false;
        public float defaultSprintDuration;
        public float currentSprintDuration;

        [JsonIgnore]
        public float dodgeTimer;



        public enum Direction { up, down, left, right }

        // pathfinding
        public Direction direction;

        [JsonIgnore]
        public Queue<Point> path;
        [JsonIgnore]
        public Point previousPosition;


        //aggro
        [JsonIgnore]
        public Vector2 aggroDistance;

        public bool isAggroed = false;



        // equipment
        [JsonIgnore]
        public Weapon weapon1;
        [JsonIgnore]
        public Weapon weapon2;
        [JsonIgnore]
        public Armor[] armor;


        [JsonIgnore]
        public static readonly int CHESTPLATE = 1, HELMET = 0, BOOTS = 2, GLOVES = 3, CAPE = 4, NECKLACE = 6, BELT = 5, RING1 = 7, RING2 = 8;

        // exp stats
        public int level = 0;
        public int currentExp = 0;
        public int skillPoints = 0;

        // battle attributes
        public int strength = 0;
        public int dexterity = 0;
        public int wisdom = 0;
        public float currentMana = 100;
        public float maxMana = 100;
        public float currentHP = 100;
        public float maxHP = 100;

        // fighting skills
        public float onehandedSkill = 0;
        public float twohandedSkill = 0;
        public float bowSkill = 0;
        public float crossbowSkill = 0;
        public float magicSkill = 0;



        public LiveEntity(Point mapPosition) : base(mapPosition)
        {
            
        }


        public override void Update()
        {
            if (IsSprinting)
            {
                currentStatus = Status.sprinting;
                currentSpeed = currentSprintSpeed;   
            }
            else
            {
                currentSpeed = defaultSpeed;
            }

            base.Update();
        }


        public override void Draw()
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (i == 0)
                {
                    drawColor = skinColor;
                }
                else if (i == 1)
                {
                    drawColor = eyeColor;
                }
                else if (i == 2)
                {
                    drawColor = hairColor;
                }
            }
            base.Draw();
        }

        // Animations
        public void AddAnimation(Direction direction, AnimationState animationState, int framesCount, Vector2 startPos, Vector2 frameSize, float eachFrameDuration)
        {
            anims.AddAnimation(new Tuple<Direction, AnimationState>(direction, animationState), new Animation(bodySpriteSheet.texture, framesCount, startPos, frameSize, eachFrameDuration));
        }

        public AnimationState SwitchStatusToAnimation()
        {
            switch (currentStatus)
            {
                case Status.idle: return AnimationState.idle;
                case Status.walking: return AnimationState.walking;
                case Status.sprinting: return AnimationState.sprinting;
            }
            return AnimationState.idle;
        }

        // AI
        public void Move(Vector2 delta, Direction direction)
        {
            position += delta;
            this.direction = direction;
            this.currentStatus = Status.walking;
            
        }

        public void Sprint()
        {
            IsSprinting = true;
        }

        public void UnSprint()
        {
            IsSprinting = false;
        }


        public void FollowInRange(LiveEntity entity, float minRange, float maxRange)
        {
            float distanceToEntity = Vector2.Distance(position, entity.position);

            if (distanceToEntity >= minRange * Globals.tileSize.X && distanceToEntity <= maxRange * Globals.tileSize.X)
            {
                Follow(entity);
            }

        }


        public void FollowInAggroRange(LiveEntity entity, float minRange, float maxRange)
        {
            float distanceToEntity = Vector2.Distance(position, entity.position);

            if (!isAggroed && distanceToEntity < minRange * Globals.tileSize.X)
            {
                isAggroed = true;
            }

            if (isAggroed)
            {
                Follow(entity);

                if (distanceToEntity > maxRange * Globals.tileSize.X)
                {
                    isAggroed = false;
                }
            }
        }


        public void Follow(LiveEntity entity)
        {
            this.currentStatus = Status.walking;
            Point mobTile = new Point((int)(position.X / Globals.tileSize.X), (int)(position.Y / Globals.tileSize.Y));
            Point playerTile = new Point((int)(entity.position.X / Globals.tileSize.X), (int)(entity.position.Y / Globals.tileSize.Y));
            Point playerPreviousTile = entity.previousPosition;

            if (path == null || path.Count == 0 || (path.Count == 1 && path.Peek() == playerTile))
            {
                path = Globals.aStarPathfinding.FindPath(mobTile, playerPreviousTile);
            }

            if (path != null && path.Count > 0)
            {
                var nextTile = path.Peek();
                var targetPosition = new Vector2(nextTile.X * Globals.tileSize.X, nextTile.Y * Globals.tileSize.Y);

                if (Vector2.Distance(position, targetPosition) < currentSpeed)
                {
                    // Move the mob to the next tile
                    position = targetPosition;
                    path.Dequeue();
                }
                else
                {
                    // Move the mob towards the next tile
                    var movementDirection = Vector2.Normalize(targetPosition - position);

                    IsSprinting = entity.IsSprinting;

                    position += movementDirection * currentSpeed;

                    // Set the direction attribute based on the movement direction
                    if (Math.Abs(movementDirection.X) > Math.Abs(movementDirection.Y))
                    {
                        if (movementDirection.X > 0)
                        {
                            direction = Direction.right;
                        }
                        else
                        {
                            direction = Direction.left;
                        }
                    }
                    else
                    {
                        if (movementDirection.Y > 0)
                        {
                            direction = Direction.down;
                        }
                        else
                        {
                            direction = Direction.up;
                        }
                    }
                }

                // Recalculate path if next tile is the player's current tile
                if (path.Count == 0 || (path.Count == 1 && path.Peek() == playerTile))
                {
                    path = Globals.aStarPathfinding.FindPath(mobTile, playerPreviousTile);
                }
            }
        }




        // UI
        public void SetIcons(int charIconId, int charIconBGId)
        {
            this.characterIconID = charIconId;
            this.characterIconBackGroundID = charIconBGId;
            characterIcon = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.entity_icons, 0, new Vector2(32 * this.characterIconID, 0), new Vector2(64, 64));
            backGroundIcon = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.entity_icon_backgrounds, 0, new Vector2(32 * this.characterIconBackGroundID, 0), new Vector2(32, 32));
        }













        // Calculations
        public int GetExpToNextLevel()
        {
            return (level + 1) * 1000;
        }

        public float GetTotalPhysicalDamage()
        {
            float totalArmorDMG = 0;
            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDMG += armor[i].PhysicalDMG;
            }
            return weapon1.PhysicalDMG + weapon2.PhysicalDMG + totalArmorDMG;
        }

        public float GetTotalMagicalDamage()
        {
            float totalArmorDMG = 0;
            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDMG += armor[i].MagicalDMG;
            }
            return weapon1.MagicalDMG + weapon2.MagicalDMG + totalArmorDMG;
        }

        public float GetTotalFireDamage()
        {
            float totalArmorDMG = 0;
            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDMG += armor[i].FireDMG;
            }
            return weapon1.FireDMG + weapon2.FireDMG + totalArmorDMG;
        }

        public float GetTotalColdDamage()
        {
            float totalArmorDMG = 0;
            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDMG += armor[i].ColdDMG;
            }
            return weapon1.ColdDMG + weapon2.ColdDMG + totalArmorDMG;
        }

        public float GetTotalLightningDamage()
        {
            float totalArmorDMG = 0;
            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDMG += armor[i].LightningDMG;
            }
            return weapon1.LightningDMG + weapon2.LightningDMG + totalArmorDMG;
        }

        public float GetTotalPhysicalDefense()
        {
            float totalArmorDEF = 0;
            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDEF += armor[i].PhysicalDEF;
            }
            return weapon1.PhysicalDEF + weapon2.PhysicalDEF + totalArmorDEF;
        }

        public float GetTotalMagicalDefense()
        {
            float totalArmorDEF = 0;
            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDEF += armor[i].MagicalDEF;
            }
            return weapon1.MagicalDEF + weapon2.MagicalDEF + totalArmorDEF;
        }

        public float GetTotalFireDefense()
        {
            float totalArmorDEF = 0;
            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDEF += armor[i].FireDEF;
            }
            return weapon1.FireDEF + weapon2.FireDEF + totalArmorDEF;
        }

        public float GetTotalColdDefense()
        {
            float totalArmorDEF = 0;
            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDEF += armor[i].ColdDEF;
            }
            return weapon1.ColdDEF + weapon2.ColdDEF + totalArmorDEF;
        }

        public float GetTotalLightningDefense()
        {
            float totalArmorDEF = 0;
            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorDEF += armor[i].LightningDEF;
            }
            return weapon1.LightningDEF + weapon2.LightningDEF + totalArmorDEF;
        }

        public float GetAvgCritChance()
        {
            float totalArmorCrit = 0;
            for (int i = 0; i < armor.Length; i++)
            {
                totalArmorCrit += armor[i].critChance;
            }
            return (weapon1.critChance + weapon2.critChance + totalArmorCrit) / (armor.Length + 2);
        }

        
    }
}
