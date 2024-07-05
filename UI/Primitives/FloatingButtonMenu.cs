using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class FloatingButtonMenu : UIComposite
    {

        public Vector2 frameSize;
        public TextButton[] buttons;

        public FloatingButtonMenu(TextButton[] buttons)
        {
            this.buttons = buttons;
            this.type = UICompositeType.FLOATING_INFO_BOX;
            this.position = buttons[0].position;

            this.frameSize = new Vector2(buttons[0].frameSize.X, buttons[0].frameSize.Y * buttons.Length - buttons[0].frameSize.Y / 2);

            for (int i = 0; i < buttons.Length; i++)
            {
                children.Add(buttons[i]);
            }

        }

    }
}
