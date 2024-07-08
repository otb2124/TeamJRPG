using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;

namespace TeamJRPG
{
    public class InGameMenu : UIComposite
    {
        public InGameMenu() 
        {
            type = UICompositeType.INGAME_MENU;


            Vector2 frameMargin = new Vector2(10, 10);
            Frame frame = new Frame(frameMargin, new Vector2(50, Globals.camera.viewport.Height - 40));

            children.Add(frame);


            Texture2D icontext = Globals.assetSetter.textures[Globals.assetSetter.UI][2][0];
            Vector2 padding = frameMargin + new Vector2(icontext.Width/4, icontext.Height/4);


            for (int i = 0; i < 9; i++)
            {
                Button button = new Button(Globals.assetSetter.textures[Globals.assetSetter.UI][2][i], new Vector2(padding.X, padding.Y + ((Globals.camera.viewport.Height-32)/9 *i)), 2, i);
                children.Add(button);
            }


            
        }


    }
}
