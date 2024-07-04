using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class SkilsInGameMenu : UIComposite
    {
        public SkilsInGameMenu()
        {
            type = UICompositeType.INGAME_MENU_SKILLS;

            //frame
            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50, Globals.camera.viewport.Height - 40);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f, 10);


            Frame frame = new Frame(framePos, frameSize);

            components.AddRange(frame.components);



            //top character pic
            CharacterIconHolder currentCharacter = new CharacterIconHolder(Globals.player, new Vector2(framePos.X + frameSize.X / 2, framePos.Y + 8), new Vector2(1, 1));
            components.AddRange(currentCharacter.components);

            // Top arrows
            Texture2D buttonTexture = Globals.assetSetter.textures[Globals.assetSetter.UI][3][0];
            Vector2 buttonOffset = new Vector2(20, 0);

            Button leftArrow = new Button(buttonTexture, new Vector2(Globals.camera.viewport.Width / 2 + currentCharacter.position.X - buttonTexture.Width - buttonOffset.X, framePos.Y + currentCharacter.texture.Height / 2 - buttonTexture.Height / 3), 1, 13);
            for (int i = 0; i < leftArrow.components.Count; i++)
            {
                leftArrow.components[i].spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Button rightArrow = new Button(buttonTexture, new Vector2(Globals.camera.viewport.Width / 2 + currentCharacter.position.X + currentCharacter.texture.Width + buttonOffset.X, framePos.Y + currentCharacter.texture.Height / 2 - buttonTexture.Height / 3), 1, 14);

            children.Add(leftArrow);
            children.Add(rightArrow);

        }


    }
}
