using System.Collections.Generic;

namespace TeamJRPG
{
    public class Group
    {

        public List<GroupMember> members;
        public List<Item> inventory;


        public int currentPlayerId;

        public Group() 
        {
            members = new List<GroupMember>();
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

            AddToInventory(new QuestItem(0));
            AddToInventory(new QuestItem(0));
            AddToInventory(new QuestItem(0));
            AddToInventory(new Valuable(0, 3));
            AddToInventory(new Valuable(0, 4));
            AddToInventory(new Material(0, 1));
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
