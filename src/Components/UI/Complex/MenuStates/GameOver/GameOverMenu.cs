using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class GameOverMenu : UIComposite
    {
        public GameOverMenu()
        {
            type = UICompositeType.GAMEOVER_MENU;

            Color alphaColor = Color.White * 0.4f;
            children.Add(new ImageHolder(Globals.textureManager.GetSprite(TextureManager.SheetCategory.placeholders, 0, new Vector2(64, 0), new Vector2(32, 32)), position, alphaColor, new Vector2(Globals.camera.viewport.Width / 32, (Globals.camera.viewport.Height + 16) / 32), null));

            Vector2 scale = new Vector2(2.5f, 2.5f);
            Vector2 titleSize = new Vector2(304, 160 - 64);
            this.position = new Vector2(Globals.camera.viewport.Width / 2 - (titleSize.X * scale.X) / 2, 20);

            ImageHolder title = new ImageHolder(Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 4, new Vector2(0, 0), titleSize), position, Color.White, scale, null);

            children.Add(title);

            // Texts for buttons
            string[] texts = new string[] { "Load Last Save", "Load Save", "Exit to Main Menu", "Exit from Game" };

            int YMargin = 80; 

            for (int i = 0; i < texts.Length; i++)
            {
                Vector2 buttonPos = new Vector2(position.X, (titleSize.Y * scale.Y) + 40 + (i * YMargin));

                TextButton button = new TextButton(texts[i], buttonPos, 1, 70 + i, Color.White, 1);

                children.Add(button);
            }
        }
    }
}
