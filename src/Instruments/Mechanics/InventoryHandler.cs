using System;
using System.Linq;
using static TeamJRPG.UIManager;


namespace TeamJRPG
{
    public class InventoryHandler
    {

        public bool IsDraggingItemInInventory = false;
        public InventoryInGameMenu inventory;






        public void DescribeItem(Item item)
        {
            Globals.uiManager.AddElement(new DescriptionWindow(item));
        }

        public void DropItem(Item item)
        {
            if (Globals.player.armor.Contains(item))
            {
                for (int i = 0; i < Globals.player.armor.Length; i++)
                {
                    if (Globals.player.armor[i] == item)
                    {
                        Armor newArmor = new Armor(0);

                        switch (i)
                        {
                            case 0: newArmor.slotType = Armor.SlotType.helmet; break;
                            case 1: newArmor.slotType = Armor.SlotType.chestplate; break;
                            case 2: newArmor.slotType = Armor.SlotType.boots; break;
                            case 3: newArmor.slotType = Armor.SlotType.gloves; break;
                            case 4: newArmor.slotType = Armor.SlotType.cape; break;
                            case 5: newArmor.slotType = Armor.SlotType.belt; break;
                            case 6: newArmor.slotType = Armor.SlotType.necklace; break;
                            case 7: newArmor.slotType = Armor.SlotType.ring; break;
                        }


                        Globals.player.armor[i] = newArmor;
                        break;
                    }
                }
            }



            else if (Globals.player.weapon1 == item)
            {
                Globals.player.weapon1 = new Weapon(0);
            }
            else if (Globals.player.weapon2 == item)
            {
                Globals.player.weapon2 = new Weapon(0);
            }
            else if (Globals.group.inventory.Contains(item))
            {
                Globals.group.inventory.Remove(item);
            }


            Object drop = new Object(Globals.player.position, 0);
            drop.AddToInventory(item);
            Globals.currentEntities.Add(drop);
            Globals.group.inventory.Remove(item);
            if (Globals.uiManager.currentMenuState == MenuState.inventory)
            {
                InventoryInGameMenu inventory = (InventoryInGameMenu)Globals.uiManager.GetAllChildrenOfType(UIComposite.UICompositeType.INGAME_MENU_INVENTORY)[0];
                RefreshUI();
            }
        }


        public void DestroyItem(Item item)
        {

            if (Globals.player.armor.Contains(item))
            {
                for (int i = 0; i < Globals.player.armor.Length; i++)
                {
                    if (Globals.player.armor[i] == item)
                    {
                        Armor newArmor = new Armor(0);

                        switch (i)
                        {
                            case 0: newArmor.slotType = Armor.SlotType.helmet; break;
                            case 1: newArmor.slotType = Armor.SlotType.chestplate; break;
                            case 2: newArmor.slotType = Armor.SlotType.boots; break;
                            case 3: newArmor.slotType = Armor.SlotType.gloves; break;
                            case 4: newArmor.slotType = Armor.SlotType.cape; break;
                            case 5: newArmor.slotType = Armor.SlotType.belt; break;
                            case 6: newArmor.slotType = Armor.SlotType.necklace; break;
                            case 7: newArmor.slotType = Armor.SlotType.ring; break;
                        }


                        Globals.player.armor[i] = newArmor;
                        break;
                    }
                }
            }



            else if (Globals.player.weapon1 == item)
            {
                Globals.player.weapon1 = new Weapon(0);
            }
            else if (Globals.player.weapon2 == item)
            {
                Globals.player.weapon2 = new Weapon(0);
            }
            else if (Globals.group.inventory.Contains(item))
            {
                Globals.group.inventory.Remove(item);
            }




            if (Globals.uiManager.currentMenuState == MenuState.inventory)
            {
                InventoryInGameMenu inventory = (InventoryInGameMenu)Globals.uiManager.GetAllChildrenOfType(UIComposite.UICompositeType.INGAME_MENU_INVENTORY)[0];
                RefreshUI();
            }
        }


        public void EquipItem(Item item)
        {
            if (Globals.uiManager.currentMenuState == MenuState.inventory)
            {
                InventoryInGameMenu inventory = (InventoryInGameMenu)Globals.uiManager.GetAllChildrenOfType(UIComposite.UICompositeType.INGAME_MENU_INVENTORY)[0];
                EquipItemByButtonMenu(item);
            }
        }

        public void UneqipItem(Item item)
        {
            if (Globals.uiManager.currentMenuState == MenuState.inventory)
            {
                InventoryInGameMenu inventory = (InventoryInGameMenu)Globals.uiManager.GetAllChildrenOfType(UIComposite.UICompositeType.INGAME_MENU_INVENTORY)[0];
                UnEquipByButtonmenu(item);
            }
        }




        public void ConsumeItem(Item item)
        {
            Globals.group.RemoveFromInventory(item);
            if (Globals.uiManager.currentMenuState == MenuState.inventory)
            {
                InventoryInGameMenu inventory = (InventoryInGameMenu)Globals.uiManager.GetAllChildrenOfType(UIComposite.UICompositeType.INGAME_MENU_INVENTORY)[0];
                RefreshUI();
            }
        }
































        public void EquipItemByButtonMenu(Item item)
        {
            if (item.type == Item.ItemType.WEAPON)
            {
                Weapon.SlotType weapontype = ((Weapon)item).slotType;


                if (weapontype == Weapon.SlotType.oneHanded)
                {
                    if (inventory.itemHolders[0].item.IsSlot)
                    {
                        Globals.player.weapon1 = (Weapon)item;
                    }
                    else if (inventory.itemHolders[1].item.IsSlot)
                    {
                        Globals.player.weapon2 = (Weapon)item;
                    }
                    else
                    {
                        Globals.group.inventory.Add(Globals.player.weapon1);
                        Globals.player.weapon1 = (Weapon)item;
                        if (inventory.itemHolders[1].IsClone)
                        {
                            Globals.player.weapon2 = new Weapon(0);
                        }
                    }
                }
                else
                {


                    if (!inventory.itemHolders[0].item.IsSlot)
                    {
                        Globals.group.inventory.Add(Globals.player.weapon1);
                    }
                    if (!inventory.itemHolders[1].item.IsSlot && !inventory.itemHolders[1].IsClone)
                    {
                        Globals.group.inventory.Add(Globals.player.weapon2);
                    }

                    Globals.player.weapon1 = (Weapon)item;
                }
            }
            else
            {
                Armor.SlotType armortype = ((Armor)item).slotType;

                switch (armortype)
                {
                    case Armor.SlotType.helmet:
                        Globals.player.armor[LiveEntity.HELMET] = (Armor)item;
                        break;
                    case Armor.SlotType.chestplate:
                        Globals.player.armor[LiveEntity.CHESTPLATE] = (Armor)item;
                        break;
                    case Armor.SlotType.gloves:
                        Globals.player.armor[LiveEntity.GLOVES] = (Armor)item;
                        break;
                    case Armor.SlotType.boots:
                        Globals.player.armor[LiveEntity.BOOTS] = (Armor)item;
                        break;
                    case Armor.SlotType.belt:
                        Globals.player.armor[LiveEntity.BELT] = (Armor)item;
                        break;
                    case Armor.SlotType.necklace:
                        Globals.player.armor[LiveEntity.NECKLACE] = (Armor)item;
                        break;
                    case Armor.SlotType.cape:
                        Globals.player.armor[LiveEntity.CAPE] = (Armor)item;
                        break;
                    case Armor.SlotType.ring:
                        if (Globals.player.armor[LiveEntity.RING1].IsSlot)
                        {
                            Globals.player.armor[LiveEntity.RING1] = (Armor)item;
                        }
                        else if (Globals.player.armor[LiveEntity.RING2].IsSlot)
                        {
                            Globals.player.armor[LiveEntity.RING2] = (Armor)item;
                        }
                        else
                        {
                            Globals.group.AddToInventory(Globals.player.armor[LiveEntity.RING1]);
                            Globals.player.armor[LiveEntity.RING1] = (Armor)item;
                        }

                        break;
                }
            }


            Globals.group.inventory.Remove(item);
            RefreshUI();
        }


        public void UnEquipByButtonmenu(Item item)
        {
            ItemHolder holder = null;

            for (int i = 0; i < inventory.itemHolders.Count; i++)
            {
                if (inventory.itemHolders[i].item == item)
                {
                    holder = inventory.itemHolders[i];
                    break;
                }

            }

            if (item.type == Item.ItemType.ARMOR)
            {

                Armor armor = new Armor(0);

                armor.slotType = ((Armor)item).slotType;
                armor.SetTexture();

                Globals.player.armor[holder.equipmentSlotId - 2] = armor;

            }
            else
            {
                if (holder.equipmentSlotId == 0)
                {
                    if (Globals.player.weapon1.slotType == Weapon.SlotType.twoHanded)
                    {
                        Globals.player.weapon2 = new Weapon(0);
                    }
                    Globals.player.weapon1 = new Weapon(0);

                }
                else
                {
                    if (Globals.player.weapon2.slotType == Weapon.SlotType.twoHanded)
                    {
                        Globals.player.weapon1 = new Weapon(0);
                    }
                    Globals.player.weapon2 = new Weapon(0);
                }

                if (holder.IsClone)
                {
                    Globals.player.weapon1 = new Weapon(0);
                    Globals.player.weapon2 = new Weapon(0);
                }
            }


            Globals.group.AddToInventory(item);
            RefreshUI();
        }







        public void EquipItemFomToItemHolder(ItemHolder itemHolderFrom, Item itemToReplace, int equipmentSlotId, bool isFromInventory)
        {
            bool proceed = false;

            if (itemHolderFrom.item.type == itemToReplace.type)
            {
                if (itemHolderFrom.item.type == Item.ItemType.WEAPON)
                {
                    Weapon weaponItem = (Weapon)itemHolderFrom.item;

                    if (weaponItem.slotType == Weapon.SlotType.oneHanded)
                    {
                        if (equipmentSlotId == 0)
                        {
                            Globals.player.weapon1 = weaponItem;
                        }
                        else if (equipmentSlotId == 1)
                        {
                            Globals.player.weapon2 = weaponItem;
                            if (Globals.player.weapon1.slotType == Weapon.SlotType.twoHanded)
                            {
                                Globals.player.weapon1 = new Weapon(0);
                            }
                        }
                    }
                    else if (weaponItem.slotType == Weapon.SlotType.twoHanded)
                    {
                        Globals.player.weapon1 = weaponItem;

                        if (equipmentSlotId == 0)
                        {
                            if (!inventory.itemHolders[equipmentSlotId + 1].item.IsSlot)
                            {
                                if (!inventory.itemHolders[equipmentSlotId + 1].IsClone)
                                {
                                    Globals.group.inventory.Add(inventory.itemHolders[equipmentSlotId + 1].item);
                                }

                                Globals.player.weapon2 = new Weapon(0);
                            }
                        }
                        else if (equipmentSlotId == 1)
                        {
                            if (!inventory.itemHolders[equipmentSlotId - 1].item.IsSlot)
                            {
                                if (!inventory.itemHolders[equipmentSlotId].IsClone)
                                {
                                    Globals.group.inventory.Add(inventory.itemHolders[equipmentSlotId - 1].item);
                                }
                                Globals.player.weapon2 = new Weapon(0);
                            }
                        }
                    }
                    proceed = true;
                }
                else if (itemHolderFrom.item.type == Item.ItemType.ARMOR)
                {
                    Armor armorItem = (Armor)itemHolderFrom.item;
                    Armor.SlotType slotType1 = armorItem.slotType;

                    if (slotType1 == ((Armor)itemToReplace).slotType)
                    {
                        switch (slotType1)
                        {
                            case Armor.SlotType.helmet:
                                Globals.player.armor[LiveEntity.HELMET] = armorItem;
                                break;
                            case Armor.SlotType.chestplate:
                                Globals.player.armor[LiveEntity.CHESTPLATE] = armorItem;
                                break;
                            case Armor.SlotType.gloves:
                                Globals.player.armor[LiveEntity.GLOVES] = armorItem;
                                break;
                            case Armor.SlotType.boots:
                                Globals.player.armor[LiveEntity.BOOTS] = armorItem;
                                break;
                            case Armor.SlotType.belt:
                                Globals.player.armor[LiveEntity.BELT] = armorItem;
                                break;
                            case Armor.SlotType.necklace:
                                Globals.player.armor[LiveEntity.NECKLACE] = armorItem;
                                break;
                            case Armor.SlotType.cape:
                                Globals.player.armor[LiveEntity.CAPE] = armorItem;
                                break;
                            case Armor.SlotType.ring:
                                if (equipmentSlotId == 9)
                                {
                                    Globals.player.armor[LiveEntity.RING1] = armorItem;
                                }
                                else if (equipmentSlotId == 10)
                                {
                                    Globals.player.armor[LiveEntity.RING2] = armorItem;
                                }

                                break;
                        }
                        proceed = true;
                    }
                }
            }

            if (proceed)
            {
                if (!itemToReplace.IsSlot)
                {
                    Globals.group.inventory.Add(itemToReplace);
                }

                if (!isFromInventory)
                {
                    if (inventory.itemHolders[equipmentSlotId].item.type == Item.ItemType.ARMOR)
                    {
                        if (equipmentSlotId == 9)
                        {
                            Globals.player.armor[LiveEntity.RING2] = new Armor(0) { slotType = Armor.SlotType.ring };
                            Globals.player.armor[LiveEntity.RING2].SetTexture();
                        }
                        else if (equipmentSlotId == 10)
                        {
                            Globals.player.armor[LiveEntity.RING1] = new Armor(0) { slotType = Armor.SlotType.ring };
                            Globals.player.armor[LiveEntity.RING1].SetTexture();
                        }


                    }
                    else
                    {
                        if (equipmentSlotId == 0)
                        {
                            Globals.player.weapon2 = new Weapon(0);
                        }
                        else if (equipmentSlotId == 1)
                        {
                            Globals.player.weapon1 = new Weapon(0);
                        }
                    }
                }



                Globals.group.inventory.Remove(itemHolderFrom.item);
                RefreshUI();
            }
        }


        public void UnEquipItem(ItemHolder itemHolder)
        {
            Item item = itemHolder.item;

            if (item.type == Item.ItemType.ARMOR)
            {

                Armor armor = new Armor(0);

                armor.slotType = ((Armor)item).slotType;
                armor.SetTexture();

                Globals.player.armor[itemHolder.equipmentSlotId - 2] = armor;

            }
            else
            {
                if (itemHolder.equipmentSlotId == 0)
                {
                    Globals.player.weapon1 = new Weapon(0);
                }
                else
                {
                    Globals.player.weapon2 = new Weapon(0);
                }

                if (itemHolder.IsClone)
                {
                    Globals.player.weapon1 = new Weapon(0);
                    Globals.player.weapon2 = new Weapon(0);
                }
            }


            Globals.group.AddToInventory(item);
            RefreshUI();
        }






        public void RefreshUI()
        {
            if (Globals.uiManager.currentMenuState == UIManager.MenuState.inventory)
            {
                inventory.RefreshEquipment();
                inventory.RefreshInventory();

            }
        }
    }
}
