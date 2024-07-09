using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;



namespace TeamJRPG
{
    public class InventoryInGameMenu : UIComposite
    {

        public Vector2 frameSize;
        public Vector2 framePos;

        public ButtonChoicePanel catBCP;
        public ButtonChoicePanel characterBCP;
        public ScrollableFrame inventoryFrame;

        public Vector2 itemSize;
        public float InventoryFrameOffset = 40;

        public Frame frame;
        public Vector2 margin;
        public Vector2 iconSize;


        public List<ItemHolder> itemHolders;
        public List<UIComposite> characterComposites;


        public System.Drawing.RectangleF frameBox;
        public System.Drawing.RectangleF scrollPaneBox;


        public InventoryInGameMenu()
        {
            type = UICompositeType.INGAME_MENU_INVENTORY;


            //GENERAL
            //frame
            frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50 + 150, Globals.camera.viewport.Height - 40);
            framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f - 150, 10);

            frameBox = new System.Drawing.RectangleF(framePos.X - Globals.camera.viewport.Width / 2, framePos.Y - Globals.camera.viewport.Height / 2, frameSize.X, frameSize.Y);

            frame = new Frame(framePos, frameSize);

            children.Add(frame);


            margin = new Vector2(40, 30);

            itemHolders = new List<ItemHolder>();

            RefreshEquipment();




            characterComposites = new List<UIComposite>();
            // Center - Character sprite
            RefreshCharFrame();








            //INVENTORY
            itemSize = iconSize;
            Vector2 frameSize3 = new Vector2(frameSize.X, itemSize.Y);
            Vector2 framePos3 = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 - InventoryFrameOffset);


            //inventoryFrame
            InventoryFrameOffset = 40;
            Vector2 frameSize2 = new Vector2(frameSize.X, frameSize.Y / 2 - InventoryFrameOffset);
            Vector2 framePos2 = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + InventoryFrameOffset);


            inventoryFrame = new ScrollableFrame(framePos2, frameSize2, 7, itemSize);
            scrollPaneBox = frameBox;
            scrollPaneBox.Y += frameSize.Y / 2;



            children.Add(inventoryFrame);


            //CATEGORIES FRAME
            Frame frame3 = new Frame(framePos3, frameSize3);

            children.Add(frame3);

            //Categories
            Vector2 catMargin = new Vector2(10, 5);
            Vector2 catItemSize = new Vector2(64, 64);
            Vector2 catPadding = new Vector2(catItemSize.X * 0.8f, 10);

            Button weaponCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][0], framePos3 + catMargin, 2, 30, "Weapons");
            Button armorCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][1], new Vector2(framePos3.X + catMargin.X + catItemSize.X + catPadding.X, framePos3.Y + catMargin.Y), 2, 31, "Armor");
            Button potionCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][2], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 2, framePos3.Y + catMargin.Y), 2, 32, "Consumables");
            Button materialCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][3], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 3, framePos3.Y + catMargin.Y), 2, 33, "Materials");
            Button valueableCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][4], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 4, framePos3.Y + catMargin.Y), 2, 34, "Valuables");
            Button questItemCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][5], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 5, framePos3.Y + catMargin.Y), 2, 35, "Quest Items");

            catBCP = new ButtonChoicePanel(new Button[] { weaponCategory, armorCategory, potionCategory, materialCategory, valueableCategory, questItemCategory });
            children.Add(catBCP);


            RefreshInventory();
        }


        public override void Update()
        {

            if (catBCP.Changed || Globals.playerChanged)
            {
                RefreshInventory();
            }

            CheckEquipmentChange();

            if (Globals.inputManager.CheckPlayerInput() || Globals.playerChanged)
            {
                Globals.uiManager.RemoveAllCompositesOfTypes(UICompositeType.FLOATING_INFO_BOX);
                RefreshCharFrame();
            }


            if (Globals.playerChanged)
            {
                RefreshEquipment();
            }


            base.Update();
        }




        private ItemHolder previousHoveredItemHolder = null;


        public void CheckEquipmentChange()
        {



            DragFromInventory();

            DragFromEquipment();

        }


        public void DragFromInventory()
        {
            bool IsEquiped = false;
            //drag from inventory
            foreach (ItemHolder inventoryItemHolder in inventoryFrame.children)
            {
                if (inventoryItemHolder.dragOn)
                {
                    ItemHolder currentHoveredItemHolder = null;


                    //from inventory to equipmentbox
                    foreach (ItemHolder equipmentHolder in itemHolders)
                    {
                        equipmentHolder.dragOn = false;
                        equipmentHolder.IsDragging = false;

                        if (equipmentHolder.itemBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
                        {
                            currentHoveredItemHolder = equipmentHolder;
                            currentHoveredItemHolder.IsGlowing = true;
                            if (inventoryItemHolder.IsDraggedAndDropped)
                            {
                                EquipItemFomToItemHolder(inventoryItemHolder, currentHoveredItemHolder.item, currentHoveredItemHolder.equipmentSlotId, true);
                                IsEquiped = true;
                                currentHoveredItemHolder.IsGlowing = false;
                                break;
                            }


                        }
                        else
                        {
                            equipmentHolder.IsGlowing = false;
                        }


                    }


                    if (IsEquiped)
                    {
                        break;
                    }

                    if (currentHoveredItemHolder != null && currentHoveredItemHolder != previousHoveredItemHolder)
                    {
                        previousHoveredItemHolder = currentHoveredItemHolder;
                    }
                    else if (currentHoveredItemHolder == null)
                    {
                        previousHoveredItemHolder = null;
                    }


                    //from inventory to throw out
                    if (!frameBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
                    {
                        if (inventoryItemHolder.IsDraggedAndDropped && (inventoryItemHolder.item.value == -3 || inventoryItemHolder.item.value >= -1))
                        {
                            Globals.uiManager.DropItem(inventoryItemHolder.item);
                            break;
                        }
                    }

                }
                else
                {
                    inventoryItemHolder.dragOn = false;
                    inventoryItemHolder.IsDragging = false;
                }
            }
        }




        public void DragFromEquipment()
        {
            bool IsEquiped = false;


            //drag from equipment
            foreach (ItemHolder equipmentFrom in itemHolders)
            {
                if (equipmentFrom.dragOn && !equipmentFrom.item.IsSlot)
                {
                    ItemHolder currentHoveredItemHolder = null;


                    //from equipment to equipment
                    foreach (ItemHolder equipmentTo in itemHolders)
                    {
                        if (equipmentTo != equipmentFrom && !equipmentTo.IsClone)
                        {
                            equipmentTo.dragOn = false;
                            equipmentTo.IsDragging = false;

                            if (equipmentTo.itemBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
                            {
                                currentHoveredItemHolder = equipmentTo;
                                currentHoveredItemHolder.IsGlowing = true;
                                if (equipmentFrom.IsDraggedAndDropped)
                                {
                                    EquipItemFomToItemHolder(equipmentFrom, currentHoveredItemHolder.item, currentHoveredItemHolder.equipmentSlotId, false);
                                    IsEquiped = true;
                                    currentHoveredItemHolder.IsGlowing = false;
                                    break;
                                }


                            }
                            else
                            {
                                equipmentTo.IsGlowing = false;
                            }
                        }



                    }

                    //from equipment to throw out
                    if (!frameBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
                    {
                        if (equipmentFrom.IsDraggedAndDropped)
                        {
                            Globals.uiManager.DropItem(equipmentFrom.item);
                            break;
                        }
                    }



                    //from equipment to inventory
                    if (scrollPaneBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
                    {
                        if (equipmentFrom.IsDraggedAndDropped && (equipmentFrom.item.value == -3 || equipmentFrom.item.value >= -1))
                        {
                            UnEquipItem(equipmentFrom);
                            IsEquiped = true;
                        }
                    }




                    if (IsEquiped)
                    {
                        break;
                    }

                    if (currentHoveredItemHolder != null && currentHoveredItemHolder != previousHoveredItemHolder)
                    {
                        previousHoveredItemHolder = currentHoveredItemHolder;
                    }
                    else if (currentHoveredItemHolder == null)
                    {
                        previousHoveredItemHolder = null;
                    }




                }
                else
                {
                    equipmentFrom.dragOn = false;
                    equipmentFrom.IsDragging = false;
                }
            }
        }




        public void EquipItemByButtonMenu(Item item)
        {
            if (item.type == Item.ItemType.WEAPON)
            {
                Weapon.SlotType weapontype = ((Weapon)item).slotType;


                if (weapontype == Weapon.SlotType.oneHanded)
                {
                    if (itemHolders[0].item.IsSlot)
                    {
                        Globals.player.weapon1 = (Weapon)item;
                    }
                    else if (itemHolders[1].item.IsSlot)
                    {
                        Globals.player.weapon2 = (Weapon)item;
                    }
                    else
                    {
                        Globals.group.inventory.Add(Globals.player.weapon1);
                        Globals.player.weapon1 = (Weapon)item;
                        if (itemHolders[1].IsClone)
                        {
                            Globals.player.weapon2 = new Weapon(0);
                        }
                    }
                }
                else
                {
                    

                    if (!itemHolders[0].item.IsSlot)
                    {
                        Globals.group.inventory.Add(Globals.player.weapon1);
                    }
                    if (!itemHolders[1].item.IsSlot && !itemHolders[1].IsClone)
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

            RefreshEquipment();
            Globals.group.inventory.Remove(item);
            RefreshInventory();
        }


        public void UnEquipByButtonmenu(Item item)
        {
            ItemHolder holder = null;

            for (int i = 0; i < itemHolders.Count; i++)
            {
                if (itemHolders[i].item == item)
                {
                    holder = itemHolders[i];
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
                    Globals.player.weapon1 = new Weapon(0);
                }
                else
                {
                    Globals.player.weapon2 = new Weapon(0);
                }

                if (holder.IsClone)
                {
                    Globals.player.weapon1 = new Weapon(0);
                    Globals.player.weapon2 = new Weapon(0);
                }
            }


            Globals.group.AddToInventory(item);
            RefreshEquipment();
            RefreshInventory();
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
                            if (!itemHolders[equipmentSlotId + 1].item.IsSlot)
                            {
                                if (!itemHolders[equipmentSlotId + 1].IsClone)
                                {
                                    Globals.group.inventory.Add(itemHolders[equipmentSlotId + 1].item);
                                }

                                Globals.player.weapon2 = new Weapon(0);
                            }
                        }
                        else if (equipmentSlotId == 1)
                        {
                            if (!itemHolders[equipmentSlotId - 1].item.IsSlot)
                            {
                                if (!itemHolders[equipmentSlotId].IsClone)
                                {
                                    Globals.group.inventory.Add(itemHolders[equipmentSlotId - 1].item);
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
                    if (itemHolders[equipmentSlotId].item.type == Item.ItemType.ARMOR)
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



                RefreshEquipment();
                Globals.group.inventory.Remove(itemHolderFrom.item);
                RefreshInventory();
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
            RefreshEquipment();
            RefreshInventory();
        }


        public void RefreshInventory()
        {
            inventoryFrame.children.Clear();


            Vector2 framePos2 = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + InventoryFrameOffset);

            //inventory items
            Vector2 itemPadding = new Vector2(12, 12);
            Vector2 inventoryMargin = new Vector2(12, 12);

            Item.ItemType sortType;
            int sortIndex = 30;


            if (catBCP != null)
            {
                sortIndex = catBCP.currentChoice;
            }

            switch (sortIndex)
            {
                case 30: sortType = Item.ItemType.WEAPON; break;
                case 31: sortType = Item.ItemType.ARMOR; break;
                case 32: sortType = Item.ItemType.CONSUMABLE; break;
                case 33: sortType = Item.ItemType.MATERIAL; break;
                case 34: sortType = Item.ItemType.VALUEABLE; break;
                case 35: sortType = Item.ItemType.QUEST; break;
                default: sortType = Item.ItemType.WEAPON; break;
            }


            List<Item> itemlist = Globals.group.inventory;
            List<Item> newList = new List<Item>();

            for (int i = 0; i < itemlist.Count; i++)
            {
                if (itemlist[i].type == sortType)
                {
                    newList.Add(itemlist[i]);
                }
            }

            for (int i = 0; i < newList.Count; i++)
            {
                int cols = i % 7;
                int rows = i / 7;
                Vector2 itemPos = new Vector2(framePos2.X + inventoryMargin.X + cols * (itemSize.X + itemPadding.X), framePos2.Y + inventoryMargin.Y + rows * (itemSize.Y + itemPadding.Y));
                ItemHolder invItem = new ItemHolder(newList[i], itemPos);
                inventoryFrame.children.Add(invItem);
            }



        }


        public void RefreshEquipment()
        {
            // Remove existing item holders
            foreach (var itemHolder in itemHolders)
            {
                children.Remove(itemHolder);
            }
            itemHolders.Clear();

            // Helper method to create and add item holders
            ItemHolder CreateItemHolder(Item item, Vector2 position, string defaultName, Weapon weapon = null)
            {
                if (item.name == "Nothing")
                {
                    item.name = defaultName;
                }
                var itemHolder = new ItemHolder(item, position);
                if (weapon != null && weapon.slotType == Weapon.SlotType.twoHanded)
                {
                    itemHolder = new ItemHolder(weapon, position);
                    itemHolder.IsClone = true;
                }
                return itemHolder;
            }

            Vector2 padding = new Vector2(10, 10);
            iconSize = new Vector2(83, 55); // Assuming there's a method to get icon size
            Vector2 halfIconWithPadding = new Vector2((iconSize.X + padding.X) / 2, (iconSize.Y + padding.Y) / 2);

            // Create item holders
            itemHolders.Add(CreateItemHolder(Globals.player.weapon1, framePos + margin, "Left Weapon Slot"));
            itemHolders.Add(CreateItemHolder(Globals.player.weapon2, framePos + margin + new Vector2(iconSize.X + padding.X, 0), "Right Weapon Slot", Globals.player.weapon1));
            itemHolders.Add(CreateItemHolder(Globals.player.armor[LiveEntity.HELMET], framePos + new Vector2(frame.frameSize.X - margin.X - iconSize.X - halfIconWithPadding.X, margin.Y), "Helmet Slot"));
            itemHolders.Add(CreateItemHolder(Globals.player.armor[LiveEntity.CHESTPLATE], framePos + new Vector2(frame.frameSize.X - margin.X - iconSize.X * 2 - padding.X, margin.Y + iconSize.Y + padding.Y), "Chestplate Slot"));
            itemHolders.Add(CreateItemHolder(Globals.player.armor[LiveEntity.BOOTS], framePos + new Vector2(frame.frameSize.X - margin.X - iconSize.X - halfIconWithPadding.X, margin.Y + (iconSize.Y + padding.Y) * 3), "Boots Slot"));
            itemHolders.Add(CreateItemHolder(Globals.player.armor[LiveEntity.GLOVES], framePos + new Vector2(frame.frameSize.X - margin.X - iconSize.X - halfIconWithPadding.X, margin.Y + (iconSize.Y + padding.Y) * 2), "Gloves Slot"));
            itemHolders.Add(CreateItemHolder(Globals.player.armor[LiveEntity.CAPE], framePos + new Vector2(frame.frameSize.X - margin.X - iconSize.X, margin.Y + iconSize.Y + padding.Y), "Cape Slot"));
            itemHolders.Add(CreateItemHolder(Globals.player.armor[LiveEntity.BELT], framePos + margin + new Vector2(halfIconWithPadding.X, iconSize.Y * 2 + padding.Y * 2), "Belt Slot"));
            itemHolders.Add(CreateItemHolder(Globals.player.armor[LiveEntity.NECKLACE], framePos + margin + new Vector2(halfIconWithPadding.X, iconSize.Y + padding.Y), "Necklace Slot"));
            itemHolders.Add(CreateItemHolder(Globals.player.armor[LiveEntity.RING1], framePos + margin + new Vector2(0, (iconSize.Y + padding.Y) * 3), "Left Hand Ring Slot"));
            itemHolders.Add(CreateItemHolder(Globals.player.armor[LiveEntity.RING2], framePos + margin + new Vector2(iconSize.X + padding.X, (iconSize.Y + padding.Y) * 3), "Right Hand Ring Slot"));


            // Assign slot IDs and add to children
            for (int i = 0; i < itemHolders.Count; i++)
            {
                itemHolders[i].equipmentSlotId = i;
                children.Add(itemHolders[i]);
            }

            Globals.uiManager.RemoveCompositeWithType(UICompositeType.FLOATING_INFO_BOX);
        }



        public void RefreshCharFrame()
        {
            for (int i = 0; i < characterComposites.Count; i++)
            {
                children.Remove(characterComposites[i]);
            }
            characterComposites.Clear();

            float scale = 2.5f;
            Texture2D charTexture = Globals.player.texture[0];
            Vector2 spritePos = new Vector2(framePos.X + frameSize.X / 2 + 32 - (charTexture.Width / 2 * scale), framePos.Y + margin.Y * 1.5f);

            Frame charFrame = new Frame(new Vector2(spritePos.X - charTexture.Width / 2, spritePos.Y - margin.Y * 0.5f), charTexture.Bounds.Size.ToVector2() * scale);
            characterComposites.Add(charFrame);
            ImageHolder character = new ImageHolder(charTexture, spritePos, Globals.player.skinColor, new Vector2(scale, scale), null);
            character.floatingText = Globals.player.name;
            for (int i = 0; i < character.components.Count; i++)
            {
                character.components[i].color = Globals.player.skinColor;
            }
            characterComposites.Add(character);
            for (int i = 1; i < Globals.player.texture.Length; i++)
            {
                ImageHolder characterDetail = new ImageHolder(Globals.player.texture[i], spritePos, Color.White, new Vector2(scale, scale), null);
                characterComposites.Add(characterDetail);
            }



            // Center - Group icons
            float iconScale = Math.Min(1f, 2.5f / Globals.group.members.Count); // Adjust scale based on group frameSize
            float charIconWidth = Globals.player.characterIcon.Width * iconScale;
            float totalIconWidth = Globals.group.members.Count * charIconWidth; // Total width needed for icons
            float startX = spritePos.X - (charTexture.Width / 2 * scale); // Center icons within the frame



            Button[] buttonArray = new Button[Globals.group.members.Count];
            for (int i = 0; i < Globals.group.members.Count; i++)
            {
                Vector2 iconPos = new Vector2(startX + (i * charIconWidth), framePos.Y + margin.Y + charTexture.Height * scale + 32);

                Stroke stroke = null;
                if (Globals.group.members[i].isPlayer)
                {
                    stroke = new Stroke(1 * 2, Color.Yellow, MonoGame.StrokeType.OutlineAndTexture);
                }

                CharacterIconHolder icon = new CharacterIconHolder(Globals.group.members[i], iconPos, new Vector2(iconScale, iconScale), stroke, null, 0);
                characterComposites.Add(icon);


                buttonArray[i] = new Button(Globals.assetSetter.textures[Globals.assetSetter.PLACEHOLDERS][0][0], iconPos, 1, 100 + i, Globals.group.members[i].name);
            }

            characterBCP = new ButtonChoicePanel(buttonArray);
            characterComposites.Add(characterBCP);



            children.AddRange(characterComposites);
        }
    }
}
