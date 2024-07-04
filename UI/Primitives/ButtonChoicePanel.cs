using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class ButtonChoicePanel : UIComposite
    {

        public int currentChoice = 0;
        public Button[] buttons;

        public ButtonChoicePanel(Button[] buttons)
        {
            this.buttons = buttons;
            children.AddRange(buttons);

            this.position = new Vector2(buttons[0].position.X, buttons[0].position.Y);

        }



        public override void Update()
        {
            base.Update();

            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Active)
                {
                    currentChoice = buttons[i].id;
                }
            }

        }
    }
}
