using Microsoft.Xna.Framework;


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



            Vector2 padding = frameMargin + new Vector2(8, 8);

            string[] hints = new string[] { "Continue", "Characters", "Inventory", "Skills", "QuestBook", "Stats", "Map", "Settings", "Exit"  };

            for (int i = 0; i < 9; i++)
            {
                Button button = new Button(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(i * 32, 32*2), new Vector2(32, 32)), new Vector2(padding.X, padding.Y + ((Globals.camera.viewport.Height-32)/9 *i)), 2, i, hints[i]);
                children.Add(button);
            }


            
        }


    }
}
