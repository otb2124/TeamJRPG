using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using SharpDX.DirectWrite;

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

        public int[] targetIDList;
        public BattleUsable currentUsable;

        public bool CheckForAnimationToFinish = false;
        public bool AnimationsSet = false;
        public int currentTargetIdAnimated = 0;
        public List<Vector2> oldPositions;

        public LiveEntity ReconstructQueueWithout = null;

        public int End = -1;

        public BattleManager()
        {
            leftSide = new List<LiveEntity>();
            rightSide = new List<LiveEntity>();
            all = new List<LiveEntity>();

            oldPositions = new List<Vector2>();

            turnQueue = new Queue<int>();
        }

        public void StartBattle(params LiveEntity[] enemies)
        {
            background = new BattleBackground(backGroundSetId);
            leftSide.AddRange(Globals.group.members);
            rightSide.AddRange(enemies);
            all.AddRange(leftSide);
            all.AddRange(rightSide);

            float leftSideWidth = (leftSide.Count) * XDistanceBetweenSprites;
            float rightSideWidth = (rightSide.Count) * XDistanceBetweenSprites;

            TotalEntitiyX = leftSideWidth + rightSideWidth + XDistanceBetweenSides;

            float halfBackgroundWidth = (background.foreGroundWidth - (background.foreGroundWidth - Globals.camera.viewport.Width)) / 2 - TotalEntitiyX / 2;

            for (int i = 0; i < leftSide.Count; i++)
            {
                leftSide[i].direction = LiveEntity.Direction.right;
                leftSide[i].battleScreenPosition = new Vector2(i * XDistanceBetweenSprites + halfBackgroundWidth, Globals.camera.viewport.Height - LiveEntity.DEFAULT_HUMANOID_BODY_SPRITE_SIZE.Y * 2);
            }
            for (int i = 0; i < rightSide.Count; i++)
            {
                rightSide[i].direction = LiveEntity.Direction.left;
                rightSide[i].battleScreenPosition = new Vector2(i * XDistanceBetweenSprites + leftSide.Count * XDistanceBetweenSprites + XDistanceBetweenSides + halfBackgroundWidth + TotalEntitiyX, Globals.camera.viewport.Height - LiveEntity.DEFAULT_HUMANOID_BODY_SPRITE_SIZE.Y * 2);
            }
            for (int i = 0; i < all.Count; i++)
            {
                oldPositions.Add(all[i].battleScreenPosition);
                all[i].currentStatus = LiveEntity.Status.idle;
            }

            float DefaultChance = 0.9f;
            EscapeChance = DefaultChance - ((all.Count - 1) * 0.05f);

            InitializeTurnQueue();

            Globals.currentGameState = Globals.GameState.battleState;

            ProceedBattle();
        }

        public void ProceedBattle()
        {
            CheckForAnimationToFinish = false;

            if (rightSide.Contains(all[Globals.battleManager.turnQueue.ElementAt(0)]))
            {
                Globals.uiManager.currentMenuState = UIManager.MenuState.battle_clear;
                Console.WriteLine("Enemy turn");
                InitEnemyTurn();
            }
            else
            {
                Globals.uiManager.currentMenuState = UIManager.MenuState.battle_menu;
                Console.WriteLine("Ally turn");
            }

            Globals.uiManager.MenuStateNeedsChange = true;

            Globals.camera.Reload();
        }

        public void InitEnemyTurn()
        {
            GenerateEnemyTurn();
            InitializeTargetList();
            for (int i = 0; i < targetIDList.Length; i++)
            {
                targetIDList[i] = RandomHelper.RandomInteger(0, leftSide.Count);
            }
            InitCast();
        }

        public void GenerateEnemyTurn()
        {
            int usableTypeChoice = RandomHelper.RandomInteger(0, 1);

            //skill
            if (usableTypeChoice == 0)
            {
                int skillChoice = RandomHelper.RandomInteger(0, all[turnQueue.Peek()].skills.Count);

                currentUsable = new BattleUsable(all[turnQueue.Peek()].skills[skillChoice]);

                Console.WriteLine("Using skill " + all[turnQueue.Peek()].skills[skillChoice].name);
            }
            //consumable
            else if (usableTypeChoice == 1)
            {
                List<Consumable> consumables = new List<Consumable>();

                for (int i = 0; i < all[turnQueue.Peek()].inventory.Count; i++)
                {
                    if (all[turnQueue.Peek()].inventory[i].type == Item.ItemType.CONSUMABLE)
                    {
                        consumables.Add((Consumable)all[turnQueue.Peek()].inventory[i]);
                    }
                }

                if (consumables.Count > 0)
                {
                    int consChoice = RandomHelper.RandomInteger(0, consumables.Count);

                    currentUsable = new BattleUsable(consumables[consChoice]);

                    Console.WriteLine("Using consumable " + consumables[consChoice].name);
                }
                else
                {
                    InitEnemyTurn();
                }
            }
        }

        public void InitCast()
        {
            Globals.uiManager.currentMenuState = UIManager.MenuState.battle_clear;
            Globals.uiManager.MenuStateNeedsChange = true;

            for (int i = 0; i < targetIDList.Length; i++)
            {
                if (currentUsable.consumable != null)
                {
                    ProceedCast(all[targetIDList[i]], currentUsable.consumable.casts);
                }
                else if (currentUsable.skill != null)
                {
                    ProceedCast(all[targetIDList[i]], currentUsable.skill.casts);
                }
                else if (currentUsable.action != null)
                {
                    ProceedCast(all[targetIDList[i]], new Cast[] { currentUsable.action.cast });
                }
            }

            CheckForAnimationToFinish = true;
        }

        public void InitProceedTurn()
        {
            turnQueue.Dequeue();
            EnqueRandomTurn();
            ProceedBattle();
        }

        public void ProceedCast(LiveEntity target, Cast[] casts)
        {
            LiveEntity caster = all[turnQueue.ElementAt(0)];

            Console.WriteLine("Target " + target.name);

            for (int i = 0; i < casts.Length; i++)
            {
                switch (casts[i].castType)
                {
                    case Cast.CastType.hp:
                        target.currentHP += casts[i].amount;
                        break;
                    case Cast.CastType.mana:
                        target.currentMana += casts[i].amount;
                        break;
                    case Cast.CastType.talk:
                        Console.WriteLine("talking to " + target.name);
                        break;
                }
            }


            HandleSingleDeath(target);
            
        }

        public void InitializeTargetList()
        {
            int count = 1;

            if (currentUsable.consumable != null)
            {
                count = currentUsable.consumable.casts[0].targetAmount;
            }
            else if (currentUsable.skill != null)
            {
                count = currentUsable.skill.casts[0].targetAmount;
            }
            else if (currentUsable.action != null)
            {
                count = currentUsable.action.cast.targetAmount;
            }

            targetIDList = new int[count];
        }

        private void InitializeTurnQueue()
        {
            turnQueue.Clear();

            for (int i = 0; i < 10; i++)
            {
                EnqueRandomTurn();
            }
        }

        public void EnqueRandomTurn()
        {
            int randomIndex = RandomHelper.RandomInteger(0, all.Count);
            if (all[randomIndex].currentBattleStatus != LiveEntity.BattleStatus.dead)
            {
                turnQueue.Enqueue(randomIndex);
            }
            else
            {
                EnqueRandomTurn();
            }
        }

        public void HandleSingleDeath(LiveEntity target)
        {
            if (target.currentHP <= 0)
            {
                target.currentBattleStatus = LiveEntity.BattleStatus.dead;
                target.currentHP = 0;
                ReconstructQueueWithout = target;
            }

            CheckGlobalDeath();
        }

        public void CheckGlobalDeath()
        {
            HandleGlobalAllyDeath();
            HandleGlobalEnemyDeath();
        }

        public void HandleGlobalAllyDeath()
        {
            bool AllDead = true;

            for (int i = 0; i < leftSide.Count; i++)
            {
                if (leftSide[i].currentBattleStatus == LiveEntity.BattleStatus.live)
                {
                    AllDead = false;
                }
            }

            if (AllDead)
            {
                End = 0;
            }
        }

        public void HandleGlobalEnemyDeath()
        {
            bool AllDead = true;

            for (int i = 0; i < rightSide.Count; i++)
            {
                if (rightSide[i].currentBattleStatus == LiveEntity.BattleStatus.live)
                {
                    AllDead = false;
                }
            }

            if (AllDead)
            {
                End = 1;
            }
        }

        public void ReconstructTurnQueueWithoutDead()
        {
            int deadEntityIndex = all.IndexOf(ReconstructQueueWithout);
            Queue<int> newTurnQueue = new Queue<int>();

            int deadEntityTurnsCount = 0;

            while (turnQueue.Count > 0)
            {
                int current = turnQueue.Dequeue();
                if (current != deadEntityIndex)
                {
                    newTurnQueue.Enqueue(current);
                }
                else
                {
                    deadEntityTurnsCount++;
                }
            }


            foreach (int entityIndex in newTurnQueue)
            {
                turnQueue.Enqueue(entityIndex);
            }

            for (int i = 0; i < deadEntityTurnsCount; i++)
            {
                EnqueRandomTurn();
            }
        }


        public void UpdateAnimations()
        {
            if (CheckForAnimationToFinish)
            {
                bool areAllInIdle = true;

                if (!AnimationsSet)
                {
                    all[turnQueue.Peek()].currentStatus = LiveEntity.Status.attacking;

                    if (turnQueue.Peek() != targetIDList[currentTargetIdAnimated])
                    {
                        all[targetIDList[currentTargetIdAnimated]].currentStatus = LiveEntity.Status.takingDamage;
                    }

                    AnimationsSet = true;
                    currentTargetIdAnimated++;

                    if (ReconstructQueueWithout != null)
                    {
                        ReconstructTurnQueueWithoutDead();
                        ReconstructQueueWithout = null;
                    }
                }

                if (AnimationsSet)
                {
                    if (!targetIDList.Contains(turnQueue.Peek()))
                    {
                        if (all[turnQueue.Peek()].currentStatus == LiveEntity.Status.attacking)
                        {
                            Vector2 targetPos = all[targetIDList[currentTargetIdAnimated - 1]].battleScreenPosition;
                            if (leftSide.Contains(all[turnQueue.Peek()]))
                            {
                                targetPos.X -= LiveEntity.DEFAULT_HUMANOID_BODY_SPRITE_SIZE.X * 1.5f;
                            }
                            else
                            {
                                targetPos.X += LiveEntity.DEFAULT_HUMANOID_BODY_SPRITE_SIZE.X * 1.5f;
                            }

                            all[turnQueue.Peek()].battleScreenPosition = targetPos;
                        }
                        else
                        {
                            all[turnQueue.Peek()].battleScreenPosition = oldPositions[turnQueue.Peek()];
                        }
                    }
                }

                for (int i = 0; i < all.Count; i++)
                {
                    if (!(all[i].currentStatus == LiveEntity.Status.idle || all[i].currentStatus == LiveEntity.Status.dead))
                    {
                        areAllInIdle = false;
                        break;
                    }
                }

                if (areAllInIdle)
                {
                    
                    if (currentTargetIdAnimated >= targetIDList.Length)
                    {
                        if(End == 0)
                        {
                            Globals.currentGameState = Globals.GameState.gameOverState;
                            Globals.uiManager.currentMenuState = UIManager.MenuState.gameOverMenu;
                            Globals.uiManager.MenuStateNeedsChange = true;
                            Globals.camera.Reload();
                            Globals.camera.FollowPlayer = true;
                            End = -1;
                        }
                        else if(End == 1)
                        {
                            Globals.currentGameState = Globals.GameState.playState;
                            Globals.uiManager.currentMenuState = UIManager.MenuState.clean;
                            Globals.uiManager.MenuStateNeedsChange = true;
                            Globals.camera.Reload();
                            End = -1;
                        }
                        else if(End == -1)
                        {
                            InitProceedTurn();
                            currentTargetIdAnimated = 0;
                        }
                        
                    }
                    else
                    {
                        AnimationsSet = false;
                        areAllInIdle = false;
                    }

                    
                }
            }
        }

        

        public void Update()
        {
            background.Update();

            for (int i = 0; i < all.Count; i++)
            {
                all[i].Update();
            }

            UpdateAnimations();
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
