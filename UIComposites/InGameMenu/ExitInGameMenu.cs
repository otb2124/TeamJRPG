using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class ExitInGameMenu : UIComposite
    {
        public ExitInGameMenu()
        {
            type = UICompositeType.INGAME_MENU_EXIT;


            //frame
            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width / 2, Globals.camera.viewport.Height / 4);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 2 - frameSize.X/2, Globals.camera.viewport.Height / 2 - frameSize.Y/2);

            Frame frame = new Frame(framePos, frameSize);

            children.Add(frame);


            //string
            string str = "Are you sure you want to exit?\nAll unsaved data will be lost.";
            Label label = new Label(str, new Vector2(framePos.X + frameSize.X/2, framePos.Y));
            Vector2 textSize = Globals.assetSetter.fonts[label.fontID].MeasureString(str);
            for (int i = 0; i < label.components.Count; i++)
            {
                label.components[i].position.X -= textSize.X / 2;
            }

            children.Add(label);


            //buttons
            TextButton noTextButton = new TextButton("No", new Vector2(framePos.X, framePos.Y + frameSize.Y), 11);
            for (int i = 0; i < noTextButton.components.Count; i++)
            {
                noTextButton.components[i].position.Y -= noTextButton.frameSize.Y / 4;
            }
            TextButton yesTextButton = new TextButton("Yes", new Vector2(framePos.X + frameSize.X, framePos.Y + frameSize.Y), 12);
            for (int i = 0; i < yesTextButton.components.Count; i++)
            {
                yesTextButton.components[i].position.Y -= noTextButton.frameSize.Y / 4;
                yesTextButton.components[i].position.X -= noTextButton.frameSize.X / 4 + 10;
            }


            children.Add(noTextButton);
            children.Add(yesTextButton);

        }


    }
}
