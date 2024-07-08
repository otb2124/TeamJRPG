using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;


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

            Button weaponCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][0], framePos3 + catMargin, 2, 30);
            Button armorCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][1], new Vector2(framePos3.X + catMargin.X + catItemSize.X + catPadding.X, framePos3.Y + catMargin.Y), 2, 31);
            Button potionCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][2], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 2, framePos3.Y + catMargin.Y), 2, 32);
            Button materialCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][3], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 3, framePos3.Y + catMargin.Y), 2, 33);
            Button valueableCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][4], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 4, framePos3.Y + catMargin.Y), 2, 34);
            Button questItemCategory = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][6][5], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 5, framePos3.Y + catMargin.Y), 2, 35);

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
                                EquipItem(inventoryItemHolder, currentHoveredItemHolder.item, currentHoveredItemHolder.equipmentSlotId, true);
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
                        if (inventoryItemHolder.IsDraggedAndDropped)
                        {
                            Debug.WriteLine("throw out");
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
                                    EquipItem(equipmentFrom, currentHoveredItemHolder.item, currentHoveredItemHolder.equipmentSlotId, false);
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

                        }
                    }



                    //from equipment to inventory
                    if (scrollPaneBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
                    {
                        if (equipmentFrom.IsDraggedAndDropped)
                        {
                            UnEquipItem(equipmentFrom);
                            IsEquiped= true;
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


        public void EquipItem(ItemHolder itemHolderFrom, Item itemToReplace, int equipmentSlotId, bool isFromInventory)
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
                                Globals.group.inventory.Add(itemHolders[equipmentSlotId + 1].item);
                                Globals.player.weapon2 = new Weapon(0);
                            }
                        }
                        else if (equipmentSlotId == 1)
                        {
                            if (!itemHolders[equipmentSlotId - 1].item.IsSlot)
                            {
                                Globals.group.inventory.Add(itemHolders[equipmentSlotId - 1].item);
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
                if(itemHolder.equipmentSlotId == 0)
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
        //EQUIPMENT
        //left
        for (int i = 0; i < itemHolders.Count; i++)
        {
            children.Remove(itemHolders[i]);
        }
        itemHolders.Clear();



        Weapon weapon1Item = Globals.player.weapon1;

        if (weapon1Item.name == "Nothing")
        {
            weapon1Item.name = "Left Weapon Slot";
        }

        ItemHolder weapon1 = new ItemHolder(weapon1Item, framePos + margin);

        Vector2 padding = new Vector2(10, 10);
        iconSize = weapon1.frameSize;


        Weapon weapon2Item = Globals.player.weapon2;

        if (weapon2Item.name == "Nothing")
        {
            weapon2Item.name = "Right Weapon Slot";
        }


        ItemHolder weapon2 = new ItemHolder(weapon2Item, new Vector2(framePos.X + margin.X + iconSize.X + padding.X, framePos.Y + margin.Y));



        Armor necklaceItem = Globals.player.armor[LiveEntity.NECKLACE];

        if (necklaceItem.name == "Nothing")
        {
            necklaceItem.name = "Necklace Slot";
        }



        ItemHolder necklace = new ItemHolder(necklaceItem, new Vector2(framePos.X + margin.X + (iconSize.X + padding.X) / 2, framePos.Y + margin.Y + iconSize.Y + padding.Y));

        Armor beltItem = Globals.player.armor[LiveEntity.BELT];

        if (beltItem.name == "Nothing")
        {
            beltItem.name = "Belt Slot";
        }


        ItemHolder belt = new ItemHolder(beltItem, new Vector2(framePos.X + margin.X + (iconSize.X + padding.X) / 2, framePos.Y + margin.Y + iconSize.Y * 2 + padding.Y * 2));

        Armor ring1Item = Globals.player.armor[LiveEntity.RING1];

        if (ring1Item.name == "Nothing")
        {
            ring1Item.name = "Left Hand Ring Slot";
        }


        ItemHolder ring1 = new ItemHolder(ring1Item, new Vector2(framePos.X + margin.X, framePos.Y + margin.Y + (iconSize.Y + padding.Y) * 3));


        Armor ring2Item = Globals.player.armor[LiveEntity.RING2];

        if (ring2Item.name == "Nothing")
        {
            ring2Item.name = "Right Hand Ring Slot";
        }


        ItemHolder ring2 = new ItemHolder(ring2Item, new Vector2(framePos.X + margin.X + iconSize.X + padding.X, framePos.Y + margin.Y + (iconSize.Y + padding.Y) * 3));

        //right

        Armor helmetItem = Globals.player.armor[LiveEntity.HELMET];

        if (helmetItem.name == "Nothing")
        {
            helmetItem.name = "Helmet Slot";
        }


        ItemHolder helmet = new ItemHolder(helmetItem, new Vector2(framePos.X + frame.frameSize.X - margin.X - iconSize.X - (iconSize.X + padding.X) / 2, framePos.Y + margin.Y));


        Armor capeItem = Globals.player.armor[LiveEntity.CAPE];

        if (capeItem.name == "Nothing")
        {
            capeItem.name = "Cape Slot";
        }


        ItemHolder cape = new ItemHolder(capeItem, new Vector2(framePos.X + frame.frameSize.X - margin.X - iconSize.X, framePos.Y + margin.Y + iconSize.Y + padding.Y));


        Armor chestplateItem = Globals.player.armor[LiveEntity.CHESTPLATE];

        if (chestplateItem.name == "Nothing")
        {
            chestplateItem.name = "Chestplate Slot";
        }


        ItemHolder chestplate = new ItemHolder(chestplateItem, new Vector2(framePos.X + frame.frameSize.X - margin.X - iconSize.X * 2 - padding.X, framePos.Y + margin.Y + iconSize.Y + padding.Y));

        Armor glovesItem = Globals.player.armor[LiveEntity.GLOVES];

        if (glovesItem.name == "Nothing")
        {
            glovesItem.name = "Gloves Slot";
        }

        ItemHolder gloves = new ItemHolder(glovesItem, new Vector2(framePos.X + frame.frameSize.X - margin.X - iconSize.X - (iconSize.X + padding.X) / 2, framePos.Y + margin.Y + (iconSize.Y + padding.Y) * 2));


        Armor bootsItem = Globals.player.armor[LiveEntity.BOOTS];

        if (bootsItem.name == "Nothing")
        {
            bootsItem.name = "Boots Slot";
        }

        ItemHolder boots = new ItemHolder(bootsItem, new Vector2(framePos.X + frame.frameSize.X - margin.X - iconSize.X - (iconSize.X + padding.X) / 2, framePos.Y + margin.Y + (iconSize.Y + padding.Y) * 3));

        if (weapon1Item.slotType == Weapon.SlotType.twoHanded)
        {
            weapon2 = new ItemHolder(weapon1Item, weapon2.position);
            weapon2.IsClone = true;
        }

        itemHolders.Add(weapon1);
        itemHolders.Add(weapon2);
        itemHolders.Add(helmet);
        itemHolders.Add(chestplate);
        itemHolders.Add(boots);
        itemHolders.Add(gloves);
        itemHolders.Add(cape);
        itemHolders.Add(belt);
        itemHolders.Add(necklace);
        itemHolders.Add(ring1);
        itemHolders.Add(ring2);


        for (int i = 0; i < itemHolders.Count; i++)
        {
            itemHolders[i].equipmentSlotId = i;
        }

        children.AddRange(itemHolders);
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
        ImageHolder character = new ImageHolder(charTexture, spritePos, new Vector2(scale, scale));
        for (int i = 0; i < character.components.Count; i++)
        {
            character.components[i].color = Globals.player.skinColor;
        }
        characterComposites.Add(character);
        for (int i = 1; i < Globals.player.texture.Length; i++)
        {
            ImageHolder characterDetail = new ImageHolder(Globals.player.texture[i], spritePos, new Vector2(scale, scale));
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
            CharacterIconHolder icon = new CharacterIconHolder(Globals.group.members[i], iconPos, new Vector2(iconScale, iconScale));
            characterComposites.Add(icon);


            buttonArray[i] = new Button(Globals.assetSetter.textures[Globals.assetSetter.PLACEHOLDERS][0][0], iconPos, 1, 100 + i);
        }

        characterBCP = new ButtonChoicePanel(buttonArray);
        characterComposites.Add(characterBCP);



        children.AddRange(characterComposites);
    }
}
}
