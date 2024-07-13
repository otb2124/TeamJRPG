using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace TeamJRPG
{
    public class CharactersInGameMenu : UIComposite
    {

        public Vector2 frameSize;
        public Vector2 framePos;


        public List<UIComposite> nameAndArrows;


        public CharactersInGameMenu()
        {
            type = UICompositeType.INGAME_MENU_CHARACTERS;


            //frame
            frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50 + 100, Globals.camera.viewport.Height - 40);
            framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f - 100, 10);
           

            Frame frame = new Frame(framePos, frameSize);

            children.Add(frame);



            nameAndArrows = new List<UIComposite>();

            RefreshPage();


            

            



            
        }




        public override void Update()
        {

            if (Globals.group.PlayerChanged)
            {
                RefreshPage();
            }


            base.Update();
        }






        public void RefreshPage()
        {

            for (int i = 0; i < nameAndArrows.Count; i++)
            {
                children.Remove(nameAndArrows[i]);
            }

            nameAndArrows.Clear();

            //top label
            string name = Globals.player.name;
            Label characterName = new Label(name, new Vector2(framePos.X + frameSize.X / 2, framePos.Y), 2, Color.White, null);

            Vector2 textSize = Globals.assetSetter.fonts[2].MeasureString(name);
            for (int i = 0; i < characterName.components.Count; i++)
            {
                characterName.components[i].position.X -= textSize.X / 2;
            }

            nameAndArrows.Add(characterName);



            // Top arrows
            Sprite buttonTexture = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(0, 32*3), new Vector2(32, 32));
            Vector2 buttonOffset = new Vector2(20, 0);

            Button leftArrow = new Button(buttonTexture, new Vector2(Globals.camera.viewport.Width / 2 + characterName.components[0].position.X - buttonTexture.srcRect.Width - buttonOffset.X, framePos.Y + textSize.Y / 2 - buttonTexture.srcRect.Height / 3), 1, 9, "Previous");
            for (int i = 0; i < leftArrow.children[0].components.Count; i++)
            {
                leftArrow.children[0].components[i].spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Button rightArrow = new Button(buttonTexture, new Vector2(Globals.camera.viewport.Width / 2 + characterName.components[0].position.X + textSize.X + buttonOffset.X, framePos.Y + textSize.Y / 2 - buttonTexture.srcRect.Height / 3), 1, 10, "Next");

            nameAndArrows.Add(leftArrow);
            nameAndArrows.Add(rightArrow);




            //Character sprite
            Sprite texture = Globals.player.sprites[0];
            Vector2 spritePos = new Vector2(framePos.X + frameSize.X / 2 - texture.srcRect.Width / 2, framePos.Y + frameSize.Y / 3 - texture.srcRect.Height / 1.5f);
            ImageHolder character = new ImageHolder(texture, spritePos, Globals.player.skinColor, new Vector2(1f, 1f), null);
            nameAndArrows.Add(character);


            











            // Table of labels
            Vector2 cellSize = new Vector2(160, 16); // Example cell frameSize

            //LEFT
            //EXPIRIENCE
            string title = "EXPIRIENCE";
            Label tableTitle = new Label(title, new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 - cellSize.Y), 1, Color.White, null);
            nameAndArrows.Add(tableTitle);

            string[][] playerStatTableData = new string[][]
            {
                new string[] { "Level", Globals.player.level.ToString() },
                new string[] { "Expirience", Globals.player.currentExp.ToString() },
                new string[] { "Next Level", Globals.player.GetExpToNextLevel().ToString() },
                new string[] { "Skill Points", Globals.player.skillPoints.ToString() },
            };

            Vector2 tableStartPosition = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + cellSize.Y);
            LabelTable labelTable = new LabelTable(playerStatTableData, tableStartPosition, cellSize);
            nameAndArrows.Add(labelTable);




            //ATTRIBUTES
            title = "ATTRIBUTES";
            tableTitle = new Label(title, new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + cellSize.Y * 6), 1, Color.White, null);
            nameAndArrows.Add(tableTitle);


            string[][] playerAttributesTableData = new string[][]
            {
                new string[] { "Strength", Globals.player.strength.ToString() },
                new string[] { "Dexterity", Globals.player.dexterity.ToString() },
                new string[] { "Wisdom", Globals.player.wisdom.ToString() },
                new string[] { "Mana", Globals.player.currentMana.ToString() + "/" + Globals.player.maxMana.ToString() },
                new string[] { "Life", Globals.player.currentHP.ToString() + "/" + Globals.player.maxHP.ToString() },
            };

            tableStartPosition = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + 32 + cellSize.Y * 6);
            labelTable = new LabelTable(playerAttributesTableData, tableStartPosition, cellSize);
            nameAndArrows.Add(labelTable);



            //FIGHTING SKILLS
            title = "FIGHTING SKILLS";
            tableTitle = new Label(title, new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + cellSize.Y * 14), 1, Color.White, null);
            nameAndArrows.Add(tableTitle);

            playerAttributesTableData = new string[][]
            {
                new string[] { "One-Handed", (Globals.player.onehandedSkill * 100).ToString() + "%" },
                new string[] { "Two-Handed", (Globals.player.twohandedSkill * 100).ToString() + "%" },
                new string[] { "Bow", (Globals.player.bowSkill * 100).ToString() + "%" },
                new string[] { "Crossbow", (Globals.player.crossbowSkill * 100).ToString() + "%"},
                new string[] { "Magic Circle", (Globals.player.magicSkill * 100).ToString() + "%" },
            };

            tableStartPosition = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + 32 + cellSize.Y * 14);
            labelTable = new LabelTable(playerAttributesTableData, tableStartPosition, cellSize);
            nameAndArrows.Add(labelTable);



            //RIGHT
            float XOffset = frameSize.X / 2;

            //WEAPONS
            title = "ATTACK";
            tableTitle = new Label(title, new Vector2(framePos.X + XOffset, framePos.Y + frameSize.Y / 2 - cellSize.Y), 1, Color.White, null);
            nameAndArrows.Add(tableTitle);

            playerAttributesTableData = new string[][]
            {
                new string[] { "Physical Damage", Globals.player.GetTotalPhysicalDamage().ToString() },
                new string[] { "Magical Damage", Globals.player.GetTotalMagicalDamage().ToString() },
                new string[] { "Fire Damage", Globals.player.GetTotalFireDamage().ToString() },
                new string[] { "Cold Damage", Globals.player.GetTotalColdDamage().ToString() },
                new string[] { "Lightning Damage", Globals.player.GetTotalLightningDamage().ToString() },
                new string[] { "Crit Chance", (Globals.player.GetAvgCritChance() * 100).ToString() + "%" },
            };

            tableStartPosition = new Vector2(framePos.X + XOffset, framePos.Y + frameSize.Y / 2 + cellSize.Y);
            labelTable = new LabelTable(playerAttributesTableData, tableStartPosition, cellSize);
            nameAndArrows.Add(labelTable);


            //WEAPONS
            title = "PROTECTION";
            tableTitle = new Label(title, new Vector2(framePos.X + XOffset, framePos.Y + frameSize.Y / 2 + cellSize.Y * 9), 1, Color.White, null);
            nameAndArrows.Add(tableTitle);

            playerAttributesTableData = new string[][]
            {
                new string[] { "Physical Protection", Globals.player.GetTotalPhysicalDefense().ToString()},
                new string[] { "Magical Protection", Globals.player.GetTotalMagicalDefense().ToString() },
                new string[] { "Fire Protection", Globals.player.GetTotalFireDefense().ToString() },
                new string[] { "Cold Protection", Globals.player.GetTotalColdDefense().ToString() },
                new string[] { "Lightning Protection", Globals.player.GetTotalLightningDefense().ToString() },
            };

            tableStartPosition = new Vector2(framePos.X + XOffset, framePos.Y + frameSize.Y / 2 + cellSize.Y * 11);
            labelTable = new LabelTable(playerAttributesTableData, tableStartPosition, cellSize);
            nameAndArrows.Add(labelTable);






            children.AddRange(nameAndArrows);
        }


    }
}
