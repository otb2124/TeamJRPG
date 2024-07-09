using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class DescriptionWindow : UIComposite
    {


        public DescriptionWindow(Item item)
        {
            type = UICompositeType.FLOATING_INFO_BOX;
            //frame
            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width / 2.5f, Globals.camera.viewport.Height - 40);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 2 - frameSize.X / 2, Globals.camera.viewport.Height / 2 - frameSize.Y / 2 - 10);

            Frame frame = new Frame(framePos, frameSize);

            children.Add(frame);


            Vector2 itemMargin = new Vector2(10, 0);

            Vector2 itemScale = new Vector2(2, 2);
            Vector2 itemPos = new Vector2(framePos.X + 20, framePos.Y + 10);
            ImageHolder itemImage = new ImageHolder(item.texture, itemPos + itemMargin, Color.White, itemScale, null);
            



            Frame imageFrame = new Frame(itemPos - itemMargin/2, new Vector2(item.texture.Width * itemScale.X, item.texture.Height * itemScale.Y - 20));
            
            children.Add(imageFrame);
            children.Add(itemImage);
        }
    }
}
