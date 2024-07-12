using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;



namespace TeamJRPG
{
    public class LiveEntity : Entity
    {
        //sprites
        public SpriteSheet bodySpriteSheet;
        public int characterIconID;
        public Sprite characterIcon;
        public int characterIconBackGroundID;
        public Sprite backGroundIcon;

        //colors
        public Color skinColor;
        public Color hairColor;
        public Color eyeColor;

        //animations
        public enum AnimationState { idle, walking };
        public AnimationState animationState;


        //body movement
        public float speed;
        public enum Direction
        { up, down, left, right }


        //pathfinding
        public Direction direction;
        public Queue<Point> path;
        public Point previousPosition;


        //equipment
        public Weapon weapon1;
        public Weapon weapon2;
        public Armor[] armor;
        public static readonly int CHESTPLATE = 1, HELMET = 0, BOOTS = 2, GLOVES = 3, CAPE = 4, NECKLACE = 6, BELT = 5, RING1 = 7, RING2 = 8;




        //exp stats
        public int level = 0;
        public int currentExp = 0;
        public int skillPoints = 0;

        //battle attributes
        public int strength = 0;
        public int dexterity = 0;
        public int wisdom = 0;
        public int currentMana = 0;
        public int maxMana = 100;
        public int currentHP = 0;
        public int maxHP = 100;

        //fightingSkills
        public float onehandedSkill = 0;
        public float twohandedSkill = 0;
        public float bowSkill = 0;
        public float crossbowSkill = 0;
        public float magicSkill = 0;





        public LiveEntity(Vector2 position) : base(position) { }




        public override void Draw()
        {

            for (int i = 0; i < sprites.Length; i++)
            {
                if(i == 0)
                {
                    drawColor = skinColor;
                }
                else if(i == 1)
                {
                    drawColor = eyeColor;
                }
                else if(i == 2)
                {
                    drawColor = hairColor;
                }
            }


            base.Draw();
        }












        //AMIMATIONS
        public void AddAnimationForAllDirections(AnimationState animationState, Vector2 sheetGridSize, float eachFrameDuration, int rowIDstart)
        {
            AddAnimation(Direction.down, animationState, sheetGridSize, eachFrameDuration, rowIDstart);
            AddAnimation(Direction.left, animationState, sheetGridSize, eachFrameDuration, rowIDstart + 1);
            AddAnimation(Direction.right, animationState, sheetGridSize, eachFrameDuration, rowIDstart + 2);
            AddAnimation(Direction.up, animationState, sheetGridSize, eachFrameDuration, rowIDstart + 3);
        }

        public void AddAnimation(Direction direction, AnimationState animationState, Vector2 sheetGridSize, float eachFrameDuration, int rowID)
        {
            rowID++;
            anims.AddAnimation(new Tuple<Direction, AnimationState>(direction, animationState), new Animation(bodySpriteSheet.texture, (int)sheetGridSize.X, (int)sheetGridSize.Y, eachFrameDuration, rowID));
        }


        //AI
        public void Move(Vector2 delta, Direction direction)
        {
            position += delta;
            this.direction = direction;
            this.animationState = AnimationState.walking;
        }


        public void Follow(LiveEntity entity)
        {
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

                if (Vector2.Distance(position, targetPosition) < speed)
                {
                    // Move the mob to the next tile
                    position = targetPosition;
                    path.Dequeue();
                }
                else
                {
                    // Move the mob towards the next tile
                    var movementDirection = Vector2.Normalize(targetPosition - position);
                    position += movementDirection * speed;

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






        //CALCULATIONS

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
