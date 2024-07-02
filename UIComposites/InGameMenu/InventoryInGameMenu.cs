using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TeamJRPG
{
    public class InventoryInGameMenu : UIComposite
    {

        public ButtonChoicePanel catBCP;
        public ButtonChoicePanel characterBCP;
        //public int 


        public InventoryInGameMenu()
        {
            type = UICompositeType.INGAME_MENU_INVENTORY;


            //GENERAL
            //frame
            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50 + 150, Globals.camera.viewport.Height - 40);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f - 150, 10);


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
            float charIconWidth = Globals.player.characterTexture.Width * iconScale;
            float totalIconWidth = Globals.group.Count * charIconWidth; // Total width needed for icons
            float startX = spritePos.X - (charTexture.Width / 2 * scale); // Center icons within the frame



            Button[] buttonArray = new Button[Globals.group.Count];
            for (int i = 0; i < Globals.group.Count; i++)
            {
                Vector2 iconPos = new Vector2(startX + (i * charIconWidth), framePos.Y + margin.Y + charTexture.Height * scale + 24);
                CharacterIconHolder icon = new CharacterIconHolder(Globals.group[i], iconPos, new Vector2(iconScale, iconScale));
                children.Add(icon);


                buttonArray[i] = new Button(Globals.assetSetter.textures[6][0][0], iconPos, 100+i);
            }

            ButtonChoicePanel characterBCP = new ButtonChoicePanel(buttonArray);
            children.Add(characterBCP);





            //INVENTORY
            //categoriesFrame
            float InventoryFrameOffset = 40;
            Vector2 itemSize = belt.frameSize;
            Vector2 frameSize3 = new Vector2(frameSize.X, itemSize.Y);
            Vector2 framePos3 = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 - InventoryFrameOffset);

            Frame frame3 = new Frame(framePos3, frameSize3);

            children.Add(frame3);

            //Categories
            Vector2 catMargin = new Vector2(10, 5);
            Vector2 catItemSize = new Vector2(64, 64);
            Vector2 catPadding = new Vector2(catItemSize.X * 0.8f, 10);

            Button weaponCategory = new Button(Globals.assetSetter.textures[4][6][0], framePos3 + catMargin, 30);
            Button armorCategory = new Button(Globals.assetSetter.textures[4][6][1], new Vector2(framePos3.X + catMargin.X + catItemSize.X + catPadding.X, framePos3.Y + catMargin.Y), 31);
            Button potionCategory = new Button(Globals.assetSetter.textures[4][6][2], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X)*2, framePos3.Y + catMargin.Y), 32);
            Button materialCategory = new Button(Globals.assetSetter.textures[4][6][3], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 3, framePos3.Y + catMargin.Y), 33);
            Button valueableCategory = new Button(Globals.assetSetter.textures[4][6][4], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 4, framePos3.Y + catMargin.Y), 34);
            Button questItemCategory = new Button(Globals.assetSetter.textures[4][6][5], new Vector2(framePos3.X + catMargin.X + (catItemSize.X + catPadding.X) * 5, framePos3.Y + catMargin.Y), 35);

            catBCP = new ButtonChoicePanel(new Button[] { weaponCategory, armorCategory, potionCategory, materialCategory, valueableCategory, questItemCategory });
            children.Add(catBCP);


            //inventoryFrame
            Vector2 frameSize2 = new Vector2(frameSize.X, frameSize.Y / 2 - InventoryFrameOffset);
            Vector2 framePos2 = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + InventoryFrameOffset);


            ScrollableFrame inventoryFrame = new ScrollableFrame(framePos2, frameSize2, 7, itemSize);

            children.Add(inventoryFrame);


            //inventory items
            Vector2 itemPadding = new Vector2(12, 12);
            Vector2 inventoryMargin = new Vector2(12, 12);
            for (int cols = 0; cols < 7; cols++)
            {
                for (global::System.Int32 rows = 0; rows < 10; rows++)
                {
                    ItemHolder invItem = new ItemHolder(new Weapon("item"), new Vector2(framePos2.X + inventoryMargin.X + cols * (itemSize.X + itemPadding.X), framePos2.Y + inventoryMargin.Y + rows * (itemSize.Y + itemPadding.Y)));
                    inventoryFrame.children.Add(invItem);
                }
                
            }
            ItemHolder invItem2 = new ItemHolder(new Armor("item"), new Vector2(framePos2.X + inventoryMargin.X + 0 * (itemSize.X + itemPadding.X), framePos2.Y + inventoryMargin.Y + 10 * (itemSize.Y + itemPadding.Y)));
            inventoryFrame.children.Add(invItem2);
            children.Add(inventoryFrame);

        }


        public override void Update()
        {
            base.Update();
        }

    }
}
