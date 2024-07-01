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
            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50, Globals.camera.viewport.Height - 40);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f, 10);
           

            Frame frame = new Frame(framePos, frameSize);

            children.Add(frame);


            //top label
            Label characterName = new Label(name, new Vector2(framePos.X + frameSize.X/2, framePos.Y));

            Vector2 textSize = Globals.assetSetter.fonts[characterName.fontID].MeasureString(name);
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
            Vector2 spritePos = new Vector2(framePos.X + frameSize.X/2 - texture.Width/2, framePos.Y + frameSize.Y / 3 - texture.Height / 2);
            ImageHolder character = new ImageHolder(texture, spritePos, new Vector2(1f, 1f));
            children.Add(character);



            //Table
        }


    }
}
