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

        public BattleManager() 
        {
            leftSide = new List<LiveEntity>();
            rightSide = new List<LiveEntity>();
            all = new List<LiveEntity>();

            turnQueue = new Queue<int>();
        }


        public void StartBattle(params LiveEntity[] enemies)
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

            ProceedBattle();
            
        }


        public void ProceedBattle()
        {
            
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

                for (global::System.Int32 i = 0; i < all[turnQueue.Peek()].inventory.Count; i++)
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


            InitializeTargetList();
            for (int i = 0; i < targetIDList.Length; i++)
            {
                targetIDList[i] = RandomHelper.RandomInteger(0, leftSide.Count);
            }
            InitCast();





        }


        public void InitCast()
        {


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



            

            turnQueue.Dequeue();
            EnqueueRandomTurn();

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
        }


        public void InitializeTargetList()
        {
            int count = 1;

            if(currentUsable.consumable != null)
            {
                count = currentUsable.consumable.casts[0].targetAmount;
            }
            else if(currentUsable.skill != null)
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

            for(int i = 0; i < 10; i++)
            {
                int randomIndex = RandomHelper.RandomInteger(0, all.Count);
                turnQueue.Enqueue(randomIndex);
            }
        }

        public void EnqueueRandomTurn()
        {
            int randomIndex = RandomHelper.RandomInteger(0, all.Count);
            turnQueue.Enqueue(randomIndex);
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
