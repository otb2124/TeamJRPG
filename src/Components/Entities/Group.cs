using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TeamJRPG
{
    public class Group
    {

        public List<GroupMember> members;
        public List<Item> inventory;


        public int currentPlayerId;

        public GroupMember previousPlayer;
        public GroupMember currentPlayer;
        public bool PlayerChanged = false;

        public List<Quest> actualQuests;
        public Group() 
        {
            members = new List<GroupMember>();
        }


        public void Load()
        {
            SetGroup();
            SetInventory();
            SetQuests();
        }


        public void SetGroup()
        {
            Globals.player = new GroupMember(new Vector2(20, 50));
            Globals.player.isPlayer = true;
            Globals.player.name = "Vika";
            Globals.player.SetIcons(0, 0);
            Globals.player.currentHP = 10;

            GroupMember member1 = new GroupMember(new Vector2(20, 51));
            member1.name = "Orest";
            member1.skinColor = Color.White;
            member1.SetIcons(0, 1);
            member1.currentHP = 100;
            member1.currentMana = 50;

            GroupMember member2 = new GroupMember(new Vector2(20, 52));
            member2.name = "Kirjusha";
            member2.SetIcons(0, 2);
            member2.currentHP = 50;

            GroupMember member3 = new GroupMember(new Vector2(20, 53));
            member3.name = "Artur";
            member3.skinColor = Color.DarkOrange;
            member3.SetIcons(0, 3);
            member3.currentHP = 75;

            members.Add(Globals.player);
            members.Add(member1);
            members.Add(member2);
            members.Add(member3);
        }

        public void SetInventory()
        {
            inventory = new List<Item>();

            AddToInventory(new Consumable(0, 2));
            AddToInventory(new Consumable(0, 2));
            AddToInventory(new Weapon(1));
            AddToInventory(new Weapon(1));
            AddToInventory(new Weapon(2));
            AddToInventory(new Weapon(3));
            AddToInventory(new Weapon(4));

            AddToInventory(new Armor(1));
            AddToInventory(new Armor(2));
            AddToInventory(new Armor(3));
            AddToInventory(new Armor(4));
            AddToInventory(new Armor(5));
            AddToInventory(new Armor(6));
            AddToInventory(new Armor(7));
            AddToInventory(new Armor(8));
            AddToInventory(new Armor(9));
            AddToInventory(new Armor(1));
            AddToInventory(new Armor(2));
            AddToInventory(new Armor(3));
            AddToInventory(new Armor(4));
            AddToInventory(new Armor(5));
            AddToInventory(new Armor(6));
            AddToInventory(new Armor(7));
            AddToInventory(new Armor(8));
            AddToInventory(new Armor(9));
            AddToInventory(new Armor(1));
            AddToInventory(new Armor(2));
            AddToInventory(new Armor(3));
            AddToInventory(new Armor(4));
            AddToInventory(new Armor(5));
            AddToInventory(new Armor(6));
            AddToInventory(new Armor(7));
            AddToInventory(new Armor(8));
            AddToInventory(new Armor(9));
            AddToInventory(new Armor(1));
            AddToInventory(new Armor(2));
            AddToInventory(new Armor(3));
            AddToInventory(new Armor(4));
            AddToInventory(new Armor(5));
            AddToInventory(new Armor(6));
            AddToInventory(new Armor(7));
            AddToInventory(new Armor(8));
            AddToInventory(new Armor(9));

            AddToInventory(new QuestItem(0));
            AddToInventory(new QuestItem(0));
            AddToInventory(new QuestItem(0));
            AddToInventory(new Valuable(0, 3));
            AddToInventory(new Valuable(0, 4));
            AddToInventory(new Material(0, 1));
        }


        public void SetQuests()
        {
            actualQuests = new List<Quest>();

            actualQuests.Add(new Quest(0));
            actualQuests.Add(new Quest(1));
            actualQuests.Add(new Quest(2));
        }


        public void Update()
        {
            CheckChangeInput();
        }

        private void CheckChangeInput()
        {
            if (Globals.inputManager.IsKeyPressedAndReleased(Keys.Q))
            {
                Globals.player.SetPrevMemberToPlayer();
            }
            else if (Globals.inputManager.IsKeyPressedAndReleased(Keys.E))
            {
                Globals.player.SetNextMemberToPlayer();
            }

            CheckPlayerChange();
        }



        public void CheckPlayerChange()
        {
            currentPlayer = Globals.player;

            if(currentPlayer != previousPlayer)
            {
                PlayerChanged = true;
                previousPlayer = currentPlayer;
            }
            else
            {
                PlayerChanged = false;
            }
        }


        public void AddToInventory(Item item)
        {

            bool hasItem = false;

            if (item.IsStackable)
            {
                foreach (var invItem in inventory)
                {
                    if (invItem.name == item.name)
                    {
                        hasItem = true;
                        invItem.amount += item.amount;
                        break;
                    }
                }

            }


            if (!item.IsStackable || !hasItem)
            {
                inventory.Add(item);
            }


            
            Globals.inventoryHandler.RefreshUI();
            

        }



        public void RemoveFromInventory(Item item)
        {

            bool hasItem = false;

            if (item.IsStackable)
            {
                foreach (var invItem in inventory)
                {
                    if (invItem.name == item.name)
                    {
                        hasItem = true;
                        if(invItem.amount > 1)
                        {
                            invItem.amount--;
                        }
                        else
                        {
                            inventory.Remove(item);
                        }
                        
                        break;
                    }
                }

            }


            if (!item.IsStackable || !hasItem)
            {
                inventory.Remove(item);
            }

        }
    }
}
