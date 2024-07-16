using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class MainMenuContent : UIComposite
    {

        public TextButton[] buttons;
        public ChoicePointer cp;
        public bool cpOn = false;



        public MainMenuContent()
        {
            type = UICompositeType.MAIN_MENU_CONTENT;

            string[] texts = new string[] { "Continue", "Load Game", "New Game", "Options", "Extras", "Exit" };
            buttons = new TextButton[texts.Length];

            float buttonPaddingY = 48;

            for (int i = 0; i < texts.Length; i++)
            {
                Vector2 textSize = Globals.assetSetter.fonts[1].MeasureString(texts[i]);
                // Align text to the right
                Vector2 textMargin = new Vector2(Globals.camera.viewport.Width/1.25f - textSize.X - 10, (Globals.camera.viewport.Height - textSize.Y) / 2 + buttonPaddingY * i);
                TextButton button = new TextButton(texts[i], textMargin, 1, 50 + i, Color.White, 1);
                buttons[i] = button;
                
            }

            children.AddRange(buttons);
        }

        public override void Update()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].IsActive)
                {
                    
                    RefreshCP(buttons[i]);
                    break;
                }
                else
                {
                    children.Remove(cp);
                }
            }

            base.Update();
        }


        public void RefreshCP(TextButton button)
        {

            children.Remove(cp);

            cp = new ChoicePointer(new Vector2(button.position.X + button.frameSize.X, button.position.Y + 8), true);

            children.Add(cp);
            cpOn = true;
            
        }
    }
}
