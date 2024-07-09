using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace TeamJRPG
{
    public class FloatingButtonMenu : UIComposite
    {

        public Vector2 frameSize;
        public TextButton[] buttons;
        public Item item;

        public FloatingButtonMenu(TextButton[] buttons, Item item)
        {
            this.item = item;
            this.buttons = buttons;
            this.type = UICompositeType.FLOATING_INFO_BOX;
            this.position = buttons[0].position;

            this.frameSize = new Vector2(buttons[0].frameSize.X, buttons[0].frameSize.Y * buttons.Length - buttons[0].frameSize.Y / 2);

            for (int i = 0; i < buttons.Length; i++)
            {
                children.Add(buttons[i]);
            }

        }


        public override void Update()
        {

            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].buttonBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y)))
                {

                    if (Globals.inputManager.IsMouseButtonClick(InputManager.MouseButton.Left))
                    {

                        switch (buttons[i].id)
                        {
                            //inventory button menus
                            case 40: //uneqip
                                Globals.uiManager.UneqipItem(item);
                                break;
                            case 41: //equip
                                Globals.uiManager.EquipItem(item);
                                break;
                            case 42: //description
                                Globals.uiManager.DescribeItem(item);
                                break;
                            case 43: //drop
                                Globals.uiManager.DropItem(item);
                                break;
                            case 44: //destroy
                                Globals.uiManager.DestroyItem(item);
                                break;
                            case 45: //consume
                                Globals.uiManager.ConsumeItem(item);
                                break;
                        }
                    }
                }
            }

            


           base.Update();
        }

    }
}
