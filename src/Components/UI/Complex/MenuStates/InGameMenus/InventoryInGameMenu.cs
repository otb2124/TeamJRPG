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
            Globals.inventoryHandler.inventory = this;

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


            


            //CATEGORIES FRAME
            Frame frame3 = new Frame(framePos3, frameSize3);

            children.Add(frame3);

            //Categories
            Vector2 catMargin = new Vector2(10, 5);
            Vector2 catItemSize = new Vector2(64, 64);
            Vector2 catPadding = new Vector2(catItemSize.X * 0.8f, 10);

            Button weaponCategory = new Button(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(0*32, 32*4), new Vector2(32, 32)), framePos3 + catMargin, new Vector2(2, 2), 30, new List<string> { "Weapons" });
            Button armorCategory = new Button(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(1 * 32, 32 * 4), new Vector2(32, 32)), new Vector2(framePos3.X + catMargin.X + catItemSize.X + catPadding.X, framePos3.Y + catMargin.Y), new Vector2(2, 2), 31, new List<string> { "Armor" });
            Button potionCategory = new Button(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(2 * 32, 32 * 4), new Vector2(32, 32)), new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 2, framePos3.Y + catMargin.Y), new Vector2(2, 2), 32, new List<string> { "Consumables" });
            Button materialCategory = new Button(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(3 * 32, 32 * 4), new Vector2(32, 32)), new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 3, framePos3.Y + catMargin.Y), new Vector2(2, 2), 33, new List<string> { "Materials" });
            Button valueableCategory = new Button(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(4 * 32, 32 * 4), new Vector2(32, 32)), new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 4, framePos3.Y + catMargin.Y), new Vector2(2, 2), 34, new List<string> { "Valuables" });
            Button questItemCategory = new Button(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(5 * 32, 32 * 4), new Vector2(32, 32)), new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 5, framePos3.Y + catMargin.Y), new Vector2(2, 2), 35, new List<string> { "Quest Items" });

            catBCP = new ButtonChoicePanel(new Button[] { weaponCategory, armorCategory, potionCategory, materialCategory, valueableCategory, questItemCategory });
            children.Add(catBCP);


            RefreshInventory();
        }


        public override void Update()
        {

            

            if (catBCP.Changed || Globals.group.PlayerChanged)
            {
                RefreshInventory();
            }

            CheckEquipmentChange();

            if (Globals.group.PlayerChanged)
            {
                Globals.uiManager.RemoveAllCompositesOfTypes(UICompositeType.FLOATING_INFO_BOX);
                RefreshCharFrame();
            }


            if (Globals.group.PlayerChanged)
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
                                Globals.inventoryHandler.EquipItemFomToItemHolder(inventoryItemHolder, currentHoveredItemHolder.item, currentHoveredItemHolder.equipmentSlotId, true);
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
                            Globals.inventoryHandler.DropItem(inventoryItemHolder.item);
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
                                    Globals.inventoryHandler.EquipItemFomToItemHolder(equipmentFrom, currentHoveredItemHolder.item, currentHoveredItemHolder.equipmentSlotId, false);
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
                            Globals.inventoryHandler.DropItem(equipmentFrom.item);
                            break;
                        }
                    }



                    //from equipment to inventory
                    if (scrollPaneBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
                    {
                        if (equipmentFrom.IsDraggedAndDropped && (equipmentFrom.item.value == -3 || equipmentFrom.item.value >= -1))
                        {
                            Globals.inventoryHandler.UnEquipItem(equipmentFrom);
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




        


        public void RefreshInventory()
        {

            children.Remove(inventoryFrame);
            

            //inventoryFrame
            InventoryFrameOffset = 40;
            Vector2 frameSize2 = new Vector2(frameSize.X, frameSize.Y / 2 - InventoryFrameOffset);
            Vector2 framePos2 = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + InventoryFrameOffset);


            inventoryFrame = new ScrollableFrame(framePos2, frameSize2, 7, itemSize);
            inventoryFrame.OpacityCut = 1;
            scrollPaneBox = frameBox;
            scrollPaneBox.Y += frameSize.Y / 2;

            inventoryFrame.children.Clear();


            children.Add(inventoryFrame);

            

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
            Sprite charTexture = Globals.player.sprites[0];
            Vector2 size = LiveEntity.DEFAULT_HUMANOID_BODY_SPRITE_SIZE;
            Vector2 spritePos = new Vector2(framePos.X + frameSize.X / 2 + 32 - (size.X / 2 * scale), framePos.Y + margin.Y * 1.5f);

            Frame charFrame = new Frame(new Vector2(spritePos.X - size.X / 2, spritePos.Y - margin.Y * 0.5f), size * scale);
            characterComposites.Add(charFrame);
            ImageHolder character = new ImageHolder(charTexture, spritePos, Globals.player.skinColor, new Vector2(scale, scale), null);
            character.floatingText.Add(Globals.player.name);
            for (int i = 0; i < character.components.Count; i++)
            {
                character.components[i].color = Globals.player.skinColor;
            }
            characterComposites.Add(character);
            for (int i = 1; i < Globals.player.sprites.Length; i++)
            {
                ImageHolder characterDetail = new ImageHolder(Globals.player.sprites[i], spritePos, Color.White, new Vector2(scale, scale), null);
                characterComposites.Add(characterDetail);
            }



            // Center - Group icons
            float iconScale = Math.Min(1f, 2.5f / Globals.group.members.Count); // Adjust scale based on group frameSize
            float charIconWidth = Globals.player.characterIcon.texture.Width * iconScale;
            float totalIconWidth = Globals.group.members.Count * charIconWidth; // Total width needed for icons
            float startX = spritePos.X - (charTexture.srcRect.Width / 2 * scale); // Center icons within the frame



            Button[] buttonArray = new Button[Globals.group.members.Count];
            for (int i = 0; i < Globals.group.members.Count; i++)
            {
                Vector2 iconPos = new Vector2(startX + (i * charIconWidth), framePos.Y + margin.Y + charTexture.srcRect.Height * scale + 32);

                Stroke stroke = null;
                if (Globals.group.members[i].isPlayer)
                {
                    stroke = new Stroke(1 * 2, Color.Yellow, MonoGame.StrokeType.OutlineAndTexture);
                }

                CharacterIconHolder icon = new CharacterIconHolder(Globals.group.members[i], iconPos, new Vector2(iconScale, iconScale), stroke, Globals.group.members[i].name, 0);
                characterComposites.Add(icon);


                buttonArray[i] = new Button(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.placeholders, 0, new Vector2(0, 0), new Vector2(32, 32)), iconPos, Vector2.One, 100 + i, null);
            }

            characterBCP = new ButtonChoicePanel(buttonArray);
            characterComposites.Add(characterBCP);



            children.AddRange(characterComposites);
        }
    }
}
