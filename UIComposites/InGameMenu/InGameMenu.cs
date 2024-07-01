using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class InGameMenu : UIComposite
    {
        public InGameMenu() 
        {
            type = UICompositeType.INGAME_MENU;

            Frame frame = new Frame(new Vector2(10, 10), new Vector2(50, Globals.camera.viewport.Height - 40));

            children.Add(frame);


            for (int i = 0; i < 9; i++)
            {
                Button button = new Button(Globals.assetSetter.textures[4][2][i], new Vector2(10 + 16, 10 + 16 + ((Globals.camera.viewport.Height-32)/9 *i)), i);
                children.Add(button);
            }


            
        }


    }
}
