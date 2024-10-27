using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace TeamJRPG
{

    [Serializable]
    public class Group
    {

        [JsonIgnore]
        public List<GroupMember> members;
        public List<Item> inventory;



        [JsonIgnore]
        public GroupMember previousPlayer;
        [JsonIgnore]
        public GroupMember currentPlayer;
        [JsonIgnore]
        public bool PlayerChanged = false;

        public List<Quest> actualQuests;


        public Group() 
        {
            members = new List<GroupMember>();
            inventory = new List<Item>();
            actualQuests = new List<Quest>();
        }



        public void Update()
        {
            if(Globals.currentGameState != Globals.GameState.gameOverState)
            {
                CheckChangeInput();
            }
            
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
