using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class CharactersInGameMenu : UIComposite
    {

        public string name = "Character Name";
        
        public CharactersInGameMenu()
        {
            type = UICompositeType.INGAME_MENU_CHARACTERS;


            //frame
            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50 + 100, Globals.camera.viewport.Height - 40);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f - 100, 10);
           

            Frame frame = new Frame(framePos, frameSize);

            children.Add(frame);


            //top label
            Label characterName = new Label(name, new Vector2(framePos.X + frameSize.X/2, framePos.Y), 2);

            Vector2 textSize = Globals.assetSetter.fonts[2].MeasureString(name);
            for (int i = 0; i < characterName.components.Count; i++)
            {
                characterName.components[i].position.X -= textSize.X / 2;
            }

            children.Add(characterName);


            // Top arrows
            Texture2D buttonTexture = Globals.assetSetter.textures[4][3][0];
            Vector2 buttonOffset = new Vector2(20, 0);

            Button leftArrow = new Button(buttonTexture, new Vector2(Globals.camera.viewport.Width/2 + characterName.components[0].position.X - buttonTexture.Width - buttonOffset.X, framePos.Y + textSize.Y/2 - buttonTexture.Height/3), 9);
            for (int i = 0; i < leftArrow.components.Count; i++)
            {
                leftArrow.components[i].spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Button rightArrow = new Button(buttonTexture, new Vector2(Globals.camera.viewport.Width / 2 + characterName.components[0].position.X + textSize.X + buttonOffset.X, framePos.Y + textSize.Y/2 - buttonTexture.Height / 3), 10);

            children.Add(leftArrow);
            children.Add(rightArrow);

            //Character sprite
            Texture2D texture = Globals.assetSetter.textures[5][0][0];
            Vector2 spritePos = new Vector2(framePos.X + frameSize.X/2 - texture.Width/2, framePos.Y + frameSize.Y / 3 - texture.Height / 1.5f);
            ImageHolder character = new ImageHolder(texture, spritePos, new Vector2(1f, 1f));
            children.Add(character);



            // Table of labels
            Vector2 cellSize = new Vector2(160, 16); // Example cell size

            //LEFT
            //EXPIRIENCE
            string title = "EXPIRIENCE";
            Label tableTitle = new Label(title, new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 - cellSize.Y), 1);
            children.Add(tableTitle);

            string[][] playerStatTableData = new string[][]
            {
                new string[] { "Level", "100" },
                new string[] { "Expirience", "100" },
                new string[] { "Next Level", "50" },
                new string[] { "Skill Points", "200" },
            };

            Vector2 tableStartPosition = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + cellSize.Y);
            LabelTable labelTable = new LabelTable(playerStatTableData, tableStartPosition, cellSize);
            children.Add(labelTable);




            //ATTRIBUTES
            title = "ATTRIBUTES";
            tableTitle = new Label(title, new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + cellSize.Y * 6), 1);
            children.Add(tableTitle);


            string[][] playerAttributesTableData = new string[][]
            {
                new string[] { "Strength", "100" },
                new string[] { "Dexterity", "100" },
                new string[] { "Wisdom", "50" },
                new string[] { "Mana", "200/200" },
                new string[] { "Life", "100/100" },
            };

            tableStartPosition = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + 32 + cellSize.Y*6);
            labelTable = new LabelTable(playerAttributesTableData, tableStartPosition, cellSize);
            children.Add(labelTable);



            //FIGHTING SKILLS
            title = "FIGHTING SKILLS";
            tableTitle = new Label(title, new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + cellSize.Y * 14), 1);
            children.Add(tableTitle);

            playerAttributesTableData = new string[][]
            {
                new string[] { "One-Handed", "100%" },
                new string[] { "Two-Handed", "25%" },
                new string[] { "Bow", "10%" },
                new string[] { "Crossbow", "2%" },
                new string[] { "Magic Circle", "5" },
            };

            tableStartPosition = new Vector2(framePos.X, framePos.Y + frameSize.Y / 2 + 32 + cellSize.Y * 14);
            labelTable = new LabelTable(playerAttributesTableData, tableStartPosition, cellSize);
            children.Add(labelTable);



            //RIGHT
            float XOffset = frameSize.X / 2;

            //WEAPONS
            title = "ATTACK";
            tableTitle = new Label(title, new Vector2(framePos.X + XOffset, framePos.Y + frameSize.Y / 2 - cellSize.Y), 1);
            children.Add(tableTitle);

            playerAttributesTableData = new string[][]
            {
                new string[] { "Physical Damage", "1" },
                new string[] { "Magical Damage", "0" },
                new string[] { "Elemental Damage", "0" },
                new string[] { "Crit Chance", "2%" },
            };

            tableStartPosition = new Vector2(framePos.X + XOffset, framePos.Y + frameSize.Y / 2 + cellSize.Y);
            labelTable = new LabelTable(playerAttributesTableData, tableStartPosition, cellSize);
            children.Add(labelTable);


            //WEAPONS
            title = "PROTECTION";
            tableTitle = new Label(title, new Vector2(framePos.X + XOffset, framePos.Y + frameSize.Y / 2 + cellSize.Y*6), 1);
            children.Add(tableTitle);

            playerAttributesTableData = new string[][]
            {
                new string[] { "Physical Protection", "10%"},
                new string[] { "Magical Protection", "1%" },
                new string[] { "Fire Protection", "1%" },
                new string[] { "Cold Protection", "1%" },
                new string[] { "Lightning Protection", "1%" },
                new string[] { "", ""},
                new string[] { "Armor Defense", "150" },
                new string[] { "Other Defense", "10"},
            };

            tableStartPosition = new Vector2(framePos.X + XOffset, framePos.Y + frameSize.Y / 2 + cellSize.Y *8);
            labelTable = new LabelTable(playerAttributesTableData, tableStartPosition, cellSize);
            children.Add(labelTable);
        }


    }
}
