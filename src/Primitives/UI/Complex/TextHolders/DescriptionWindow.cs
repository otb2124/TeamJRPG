using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class DescriptionWindow : UIComposite
    {


        public DescriptionWindow(Item item)
        {
            type = UICompositeType.DESCRIPTION_WINDOW;
            //frame
            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width / 2.5f, Globals.camera.viewport.Height - 40);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 2 - frameSize.X / 2, Globals.camera.viewport.Height / 2 - frameSize.Y / 2 - 10);

            Frame frame = new Frame(framePos, frameSize);

            children.Add(frame);


            Vector2 itemMargin = new Vector2(10, 0);

            Vector2 itemScale = new Vector2(2, 2);
            Vector2 itemPos = new Vector2(framePos.X + 20, framePos.Y + 10);
            ImageHolder itemImage = new ImageHolder(item.sprite, itemPos + itemMargin, Color.White, itemScale, null);
            



            Frame imageFrame = new Frame(itemPos - itemMargin/2, new Vector2(item.sprite.srcRect.Width * itemScale.X, item.sprite.srcRect.Height * itemScale.Y - 20));
            
            children.Add(imageFrame);
            children.Add(itemImage);


            string text = "adasdddddddddddddddddddddddddddddd. jaIAHSdo . Lorem ipsum dolor sit amet. balvalblabalalb";


            TextArea ta = new TextArea(text, new Vector2(framePos.X, itemPos.Y + item.sprite.srcRect.Height*itemScale.Y), 0, Color.White, null, (int)frameSize.X, (int)(frameSize.Y - item.sprite.srcRect.Height*itemScale.Y));
            children.Add(ta);
        }
    }
}
