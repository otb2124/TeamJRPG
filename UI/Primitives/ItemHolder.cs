using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;


namespace TeamJRPG
{
    public class ItemHolder : UIComposite
    {

        public Vector2 frameSize;
        public System.Drawing.RectangleF itemBox;
        public Item item;
        public FloatingInfoBox infoBox;
        public FloatingButtonMenu buttonMenu;

        public bool buttonMenuOn = false;
        public bool infoOn = false;

        public ItemHolder(Item item, Vector2 startPosition)
        {
            this.position = startPosition;
            this.item = item;
            this.type = UICompositeType.ITEM_HOLDER;
            float scale = 1f;
            Vector2 padding = new Vector2(10, 0);
            Frame frame = new Frame(position, new Vector2(item.texture.Width * scale + padding.X, item.texture.Height * scale + padding.Y));
            frameSize = frame.frameSize;
            components.AddRange(frame.components);
            children.Add(frame);

            ImageHolder icon = new ImageHolder(item.texture, position + padding * 2, new Vector2(scale * 1.5f, scale * 1.5f));
            components.AddRange(icon.components);
            children.Add(icon);


            Vector2 boxPos = new Vector2(position.X - Globals.camera.viewport.Width / 2, position.Y - Globals.camera.viewport.Height / 2);
            itemBox = new System.Drawing.RectangleF(boxPos.X, boxPos.Y, frameSize.X, frameSize.Y);
        }



        public override void Update()
        {
            Vector2 cursorPos = Globals.inputManager.GetCursorPos();
            PointF cursorPointF = new PointF(cursorPos.X, cursorPos.Y);

            if (itemBox.Contains(cursorPointF))
            {

                if (!infoOn)
                {

                    string itemtype = "BLANK_ITEM_TYPE";

                    switch (item.type)
                    {
                        case Item.ItemType.WEAPON:
                            itemtype = "Weapon";
                            break;
                        case Item.ItemType.ARMOR:
                            itemtype = "Armor";
                            break;
                        case Item.ItemType.CONSUMABLE:
                            itemtype = "Consumable";
                            break;
                        case Item.ItemType.MATERIAL:
                            itemtype = "Material";
                            break;
                        case Item.ItemType.VALUEABLE:
                            itemtype = "Valuable";
                            break;
                        case Item.ItemType.QUEST:
                            itemtype = "Quest Item";
                            break;
                    }

                    List<string> infoList = new List<string>();

                    infoList.Add(item.name);
                    if (!item.name.Contains("Slot"))
                    {
                        infoList.Add(itemtype);
                        infoList.Add(item.description);


                        if (item.value >= -4)
                        {
                            if (item.value == -4)
                            {
                                infoList.Add("Cannot be Destroyed, Dropped or Sold");
                            }
                            if (item.value == -3)
                            {
                                infoList.Add("Cannot be Destroyed or Sold");
                            }
                            else if (item.value == -2)
                            {
                                infoList.Add("Cannot be Dropped or Sold");
                            }
                            else if (item.value == -1)
                            {
                                infoList.Add("Cannot be Sold");
                            }
                            else if (item.value == 0)
                            {
                                infoList.Add("Worthless");
                            }
                            else
                            {
                                infoList.Add(item.value.ToString());
                            }
                        }

                    }


                    infoBox = new FloatingInfoBox(infoList);
                    children.Add(infoBox);

                    infoOn = true;
                }


                if (Globals.inputManager.IsMouseButtonClick(InputManager.MouseButton.Right))
                {
                    if (!buttonMenuOn)
                    {
                        Vector2 adjustedPos = new Vector2(cursorPos.X + Globals.camera.viewport.Width / 2, cursorPos.Y + Globals.camera.viewport.Height / 2);

                        List<string> textButtonTexts = new List<string>
                        {
                            "Equip",
                            "Drop",
                            "Destroy",
                        };

                        TextButton[] buttons = new TextButton[textButtonTexts.Count];

                        for (int i = 0; i < textButtonTexts.Count; i++)
                        {
                            buttons[i] = new TextButton(textButtonTexts[i], new Vector2(adjustedPos.X, adjustedPos.Y + ((Globals.assetSetter.fonts[1].MeasureString("K").Y + 28) * i)), i + 40);
                        }

                        buttonMenu = new FloatingButtonMenu(buttons.ToArray());
                        children.Add(buttonMenu);

                        // Calculate the frame size based on the total height of all buttons and the width of the longest button
                        float totalHeight = buttons.Sum(button => button.frameSize.Y);
                        float maxWidth = buttons.Max(button => button.frameSize.X);

                        itemBox.Width += maxWidth;
                        itemBox.Height += totalHeight;

                        buttonMenuOn = true;
                        children.Remove(infoBox);
                    }

                }





            }
            else
            {
                children.Remove(infoBox);
                children.Remove(buttonMenu);

                infoOn = false;
                buttonMenuOn = false;

                itemBox.Width = frameSize.X;
                itemBox.Height = frameSize.Y;
            }

            base.Update();
        }
    }
}
