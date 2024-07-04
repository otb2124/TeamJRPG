using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public InventoryInGameMenu()
        {
            type = UICompositeType.INGAME_MENU_INVENTORY;


            //GENERAL
            //frame
            frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50 + 150, Globals.camera.viewport.Height - 40);
            framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f - 150, 10);


            Frame frame = new Frame(framePos, frameSize);

           children.Add(frame);


            //EQUIPMENT
            //left
            Vector2 margin = new Vector2(20, 30);
            ItemHolder weapon1 = new ItemHolder(new Weapon("sword1"), framePos + margin);
            children.Add(weapon1);

            Vector2 iconSize = weapon1.frameSize;
            Vector2 padding = new Vector2(10, 10);
            ItemHolder weapon2 = new ItemHolder(new Weapon("sword2"), new Vector2(framePos.X + margin.X + iconSize.X + padding.X, framePos.Y + margin.Y));
            children.Add(weapon2);

            ItemHolder necklace = new ItemHolder(new Armor("necklace"), new Vector2(framePos.X + margin.X + (iconSize.X + padding.X)/2, framePos.Y + margin.Y + iconSize.Y + padding.Y));
            children.Add(necklace);

            ItemHolder belt = new ItemHolder(new Armor("belt"), new Vector2(framePos.X + margin.X + (iconSize.X + padding.X) / 2, framePos.Y + margin.Y + iconSize.Y*2 + padding.Y*2));
            children.Add(belt);

            ItemHolder ring1 = new ItemHolder(new Armor("ring1"), new Vector2(framePos.X + margin.X, framePos.Y + margin.Y + (iconSize.Y + padding.Y) * 3));
            children.Add(ring1);
            ItemHolder ring2 = new ItemHolder(new Armor("ring2"), new Vector2(framePos.X + margin.X + iconSize.X + padding.X, framePos.Y + margin.Y + (iconSize.Y + padding.Y) * 3));
            children.Add(ring2);


            //right
            ItemHolder helmet = new ItemHolder(new Armor("helmet"), new Vector2(framePos.X + frame.frameSize.X - margin.X - iconSize.X - (iconSize.X + padding.X)/2, framePos.Y + margin.Y));
            children.Add(helmet);

            ItemHolder cape = new ItemHolder(new Armor("cape"), new Vector2(framePos.X + frame.frameSize.X - margin.X - iconSize.X, framePos.Y + margin.Y + iconSize.Y + padding.Y));
            children.Add(cape);
            ItemHolder chestplate = new ItemHolder(new Armor("chestplate"), new Vector2(framePos.X + frame.frameSize.X - margin.X - iconSize.X*2 - padding.X, framePos.Y + margin.Y + iconSize.Y + padding.Y));
            children.Add(chestplate);


            ItemHolder gloves = new ItemHolder(new Armor("gloves"), new Vector2(framePos.X + frame.frameSize.X - margin.X - iconSize.X - (iconSize.X + padding.X) / 2, framePos.Y + margin.Y + (iconSize.Y + padding.Y)*2));
            children.Add(gloves);

            ItemHolder boots = new ItemHolder(new Armor("boots"), new Vector2(framePos.X + frame.frameSize.X - margin.X - iconSize.X - (iconSize.X + padding.X) / 2, framePos.Y + margin.Y + (iconSize.Y + padding.Y) * 3));
            children.Add(boots);


            // Center - Character sprite
            float scale = 2.5f;
            Texture2D charTexture = Globals.assetSetter.textures[1][0][0];
            Vector2 spritePos = new Vector2(framePos.X + frameSize.X / 2 + 32 - (charTexture.Width / 2 * scale), framePos.Y + margin.Y);
            ImageHolder character = new ImageHolder(charTexture, spritePos, new Vector2(scale, scale));
            children.Add(character);

            // Center - Group icons
            float iconScale = Math.Min(1f, 2.5f / Globals.group.Count); // Adjust scale based on group size
            float charIconWidth = Globals.player.characterIcon.Width * iconScale;
            float totalIconWidth = Globals.group.Count * charIconWidth; // Total width needed for icons
            float startX = spritePos.X - (charTexture.Width / 2 * scale); // Center icons within the frame



            Button[] buttonArray = new Button[Globals.group.Count];
            for (int i = 0; i < Globals.group.Count; i++)
            {
                Vector2 iconPos = new Vector2(startX + (i * charIconWidth), framePos.Y + margin.Y + charTexture.Height * scale + 24);
                CharacterIconHolder icon = new CharacterIconHolder(Globals.group[i], iconPos, new Vector2(iconScale, iconScale));
                children.Add(icon);


                buttonArray[i] = new Button(Globals.assetSetter.textures[Globals.assetSetter.PLACEHOLDERS][0][0], iconPos, 1, 100+i);
            }

            characterBCP = new ButtonChoicePanel(buttonArray);
            children.Add(characterBCP);


            


            //INVENTORY
            itemSize = belt.frameSize;
            Vector2 frameSize3 = new Vector2(frameSize.X, itemSize.Y);
            Vector2 framePos3 = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 - InventoryFrameOffset);





            FillInventory();








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

        }


        public override void Update()
        {

            RefreshInventory();

            base.Update();
        }



        public void FillInventory()
        {
            //inventoryFrame
            InventoryFrameOffset = 40;
            Vector2 frameSize2 = new Vector2(frameSize.X, frameSize.Y / 2 - InventoryFrameOffset);
            Vector2 framePos2 = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + InventoryFrameOffset);


            inventoryFrame = new ScrollableFrame(framePos2, frameSize2, 7, itemSize);

            RefreshInventory();
            
            children.Add(inventoryFrame);
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


            List<Item> itemlist = Globals.player.inventory;
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
    }
}
