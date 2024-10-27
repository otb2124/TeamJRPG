using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;


namespace TeamJRPG
{
    public class MainMenuTitle : UIComposite
    {

        public MainMenuTitle()
        {
            type = UICompositeType.MAIN_MENU;


            Vector2 titlescale = new Vector2(2, 2);

            Sprite title = Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 1, Vector2.Zero, new Vector2(304, 96));

            Vector2 titleMargin = new Vector2((Globals.camera.viewport.Width - title.Width * titlescale.X) / 2, 36);

            ImageHolder gameTitle = new ImageHolder(title, titleMargin, Color.White, titlescale, null);
            children.Add(gameTitle);


        }





    }
}
