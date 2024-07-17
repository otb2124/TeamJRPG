using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class BattleManager
    {

        
        public List<LiveEntity> leftSide;
        public List<LiveEntity> rightSide;
        public List<LiveEntity> all;

        public int backGroundSetId = 0;
        public BattleBackground background;

        public float XDistanceBetweenSprites = 64;
        public float XDistanceBetweenSides = 200;
        public float TotalEntitiyX;

        public Queue<int> turnQueue;

        public float EscapeChance;

        public BattleManager() 
        {
            leftSide = new List<LiveEntity>();
            rightSide = new List<LiveEntity>();
            all = new List<LiveEntity>();

            turnQueue = new Queue<int>();
        }


        public void StartBatlle(params LiveEntity[] enemies)
        {
            
            

            background = new BattleBackground(backGroundSetId);
            leftSide.AddRange(Globals.group.members);
            rightSide.AddRange(enemies);
            all.AddRange(leftSide); all.AddRange(rightSide);

            float leftSideWidth = (leftSide.Count) * XDistanceBetweenSprites;
            float rightSideWidth = (rightSide.Count) * XDistanceBetweenSprites;

            TotalEntitiyX = leftSideWidth + rightSideWidth + XDistanceBetweenSides;

            

            float halfBackgroundWidth = (background.foreGroundWidth - (background.foreGroundWidth - Globals.camera.viewport.Width))/2 - TotalEntitiyX/2;


            for (int i = 0; i < leftSide.Count; i++)
            {
                leftSide[i].direction = LiveEntity.Direction.right;
                leftSide[i].battleScreenPosition = new Vector2(i * XDistanceBetweenSprites + halfBackgroundWidth, Globals.camera.viewport.Height - LiveEntity.DEFAULT_HUMANOID_BODY_SPRITE_SIZE.Y*2);
            }
            for (int i = 0; i < rightSide.Count; i++)
            {
                rightSide[i].direction = LiveEntity.Direction.left;
                rightSide[i].battleScreenPosition = new Vector2(i * XDistanceBetweenSprites + leftSide.Count * XDistanceBetweenSprites + XDistanceBetweenSides + halfBackgroundWidth + TotalEntitiyX, Globals.camera.viewport.Height - LiveEntity.DEFAULT_HUMANOID_BODY_SPRITE_SIZE.Y * 2);
            }


            float DefaultChance = 0.9f;
            EscapeChance = DefaultChance - ((all.Count - 1) * 0.05f);

            InitializeTurnQueue();



            Globals.currentGameState = Globals.GameState.battle;
            Globals.uiManager.currentMenuState = UIManager.MenuState.battle;
            Globals.uiManager.MenuStateNeedsChange = true;

            Globals.camera.Reload();
        }



        public void CastSkill(LiveEntity lent, Skill skill)
        {
            switch (skill.castType)
            {
                case Skill.CastType.self:

                    switch (skill.type)
                    {
                        case Skill.SkillType.attacking:
                            Console.WriteLine("Hurting self");
                            break;
                        case Skill.SkillType.defending:
                            Console.WriteLine("Defending self");
                            break;
                        case Skill.SkillType.boosting:
                            Console.WriteLine("Healing self");
                            break;
                    }


                    break;
                case Skill.CastType.singleTarget:

                    switch (skill.type)
                    {
                        case Skill.SkillType.attacking:
                            Console.WriteLine("Attacking target");
                            break;
                        case Skill.SkillType.defending:
                            Console.WriteLine("Defending target");
                            break;
                        case Skill.SkillType.boosting:
                            Console.WriteLine("Healing target");
                            break;
                    }


                    break;
                case Skill.CastType.groupTarget:


                    switch (skill.type)
                    {
                        case Skill.SkillType.attacking:
                            Console.WriteLine("Attacking group of targets");
                            break;
                        case Skill.SkillType.defending:
                            Console.WriteLine("Defending group of targets");
                            break;
                        case Skill.SkillType.boosting:
                            Console.WriteLine("Healing group of targets");
                            break;
                    }


                    break;
                case Skill.CastType.allEnemy:

                    switch (skill.type)
                    {
                        case Skill.SkillType.attacking:
                            Console.WriteLine("Attacking all enemies");
                            break;
                        case Skill.SkillType.defending:
                            Console.WriteLine("Defending all enemies");
                            break;
                        case Skill.SkillType.boosting:
                            Console.WriteLine("Healing all enemies");
                            break;
                    }




                    break;
                case Skill.CastType.allAlly:


                    switch (skill.type)
                    {
                        case Skill.SkillType.attacking:
                            Console.WriteLine("Attacking all allies");
                            break;
                        case Skill.SkillType.defending:
                            Console.WriteLine("Defending all allies");
                            break;
                        case Skill.SkillType.boosting:
                            Console.WriteLine("Healing all allies");
                            break;
                    }



                    break;
                case Skill.CastType.all:



                    switch (skill.type)
                    {
                        case Skill.SkillType.attacking:
                            Console.WriteLine("Attacking all");
                            break;
                        case Skill.SkillType.defending:
                            Console.WriteLine("Defending all");
                            break;
                        case Skill.SkillType.boosting:
                            Console.WriteLine("Healing all");
                            break;
                    }


                    break;
            }
        }


        private void InitializeTurnQueue()
        {
            turnQueue.Clear();

            for(int i = 0; i < 10; i++)
            {
                int randomIndex = RandomHelper.RandomInteger(0, all.Count);
                turnQueue.Enqueue(randomIndex);
            }
        }






        public void Update()
        {
            background.Update();

            for (int i = 0; i < all.Count; i++)
            {
                all[i].Update();
            }

        }




        public void Draw()
        {
            background.Draw();

            for (int i = 0; i < all.Count; i++)
            {
                all[i].Draw();
            }

        }
    }
}
