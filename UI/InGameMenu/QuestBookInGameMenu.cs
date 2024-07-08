using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class QuestBookInGameMenu : UIComposite
    {

        public string name = "Main Quests";

        public QuestBookInGameMenu()
        {
            type = UICompositeType.INGAME_MENU_QUESTBOOK;


            //frame
            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50, Globals.camera.viewport.Height - 40);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f, 10);


            Frame frame = new Frame(framePos, frameSize);

            children.Add(frame);


            //top label
            Label characterName = new Label(name, new Vector2(framePos.X + frameSize.X / 2, framePos.Y), 2, Color.White, null);

            Vector2 textSize = Globals.assetSetter.fonts[2].MeasureString(name);
            for (int i = 0; i < characterName.components.Count; i++)
            {
                characterName.components[i].position.X -= textSize.X / 2;
            }

            children.Add(characterName);


            // Top arrows
            Texture2D buttonTexture = Globals.assetSetter.textures[Globals.assetSetter.UI][3][0];
            Vector2 buttonOffset = new Vector2(20, 0);

            Button leftArrow = new Button(buttonTexture, new Vector2(Globals.camera.viewport.Width / 2 + characterName.components[0].position.X - buttonTexture.Width - buttonOffset.X, framePos.Y + textSize.Y / 2 - buttonTexture.Height / 3), 1,15);
            for (int i = 0; i < leftArrow.components.Count; i++)
            {
                leftArrow.components[i].spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Button rightArrow = new Button(buttonTexture, new Vector2(Globals.camera.viewport.Width / 2 + characterName.components[0].position.X + textSize.X + buttonOffset.X, framePos.Y + textSize.Y / 2 - buttonTexture.Height / 3), 1, 16);

            children.Add(leftArrow);
            children.Add(rightArrow);

            
            //quest list frame
            framePos = new Vector2(framePos.X, framePos.Y + textSize.Y + 16);
            Frame questFrame = new Frame(framePos, new Vector2(frameSize.X/3, frameSize.Y - textSize.Y - 16));
            children.Add(questFrame);

            //quest list
            Button[] buttonArray = new Button[7];
            Vector2 Offset = new Vector2(10, 64);

            for (int i = 0; i < 7; i++)
            {
                TextButton txtBtn = new TextButton("Quest Name", new Vector2(framePos.X + Offset.X, framePos.Y + i * Offset.Y), 30 + i);
                buttonArray[i] = txtBtn;
            }

            ButtonChoicePanel questBCP = new ButtonChoicePanel(buttonArray);
            children.Add(questBCP);


            //quest descript frame
            framePos = new Vector2(framePos.X + frameSize.X / 3 + 36, framePos.Y);
            questFrame = new Frame(framePos, new Vector2(frameSize.X / 3 * 2 - 36, frameSize.Y - textSize.Y - 16));
            children.Add(questFrame);

            Label questTitle = new Label("Quest Name", new Vector2(framePos.X + questFrame.frameSize.X/4, framePos.Y), 1, Color.White, null);
            children.Add(questTitle);

            Label questDesr = new Label("This quest is just a regular one to describe\nhow ui kinda works/doesnt.\nI actually like next paragraph.\n\nUh here it  is.", new Vector2(framePos.X, framePos.Y + 64), 0, Color.White, null);
            children.Add(questDesr);

        }


    }
}
