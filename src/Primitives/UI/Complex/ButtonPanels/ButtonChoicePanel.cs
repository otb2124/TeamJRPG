using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class ButtonChoicePanel : UIComposite
    {

        public int oldChoice;
        public int currentChoice = 0;
        public bool Changed = false;
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

            oldChoice = currentChoice;
            Changed = false; // Reset the Changed flag at the beginning of each update cycle

            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Active)
                {
                    currentChoice = buttons[i].id;

                    // Check if the current choice is different from the old choice
                    if (currentChoice != oldChoice)
                    {
                        Changed = true;
                        break; // Exit the loop early since we found a change
                    }
                }
            }
        }
    }
}
