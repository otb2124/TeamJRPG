using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TeamJRPG
{
    public class FloatingInfoBox : UIComposite
    {
        public Vector2 frameSize;
        public List<string> info;

        public FloatingInfoBox(List<string> info)
        {
            this.info = info;
            this.position = new Vector2(position.X + Globals.camera.viewport.Width / 2, position.Y + Globals.camera.viewport.Height / 2 + Globals.assetSetter.textures[Globals.assetSetter.UI][0][0].Height);
            this.type = UICompositeType.FLOATING_INFO_BOX;

            // Measure the size of the longest string
            Vector2 longestTextSize = Vector2.Zero;

            foreach (var text in info)
            {
                Vector2 textSize = Globals.assetSetter.fonts[0].MeasureString(text);
                if (textSize.X > longestTextSize.X)
                {
                    longestTextSize = textSize;
                }
            }

            // Adjust the height based on the total number of lines
            float totalHeight = 0;
            foreach (var text in info)
            {
                totalHeight += Globals.assetSetter.fonts[0].MeasureString(text).Y;
            }

            Frame frame = new Frame(position, new Vector2(longestTextSize.X, totalHeight));
            this.frameSize = frame.frameSize;

            children.Add(frame);

            float currentY = position.Y;
            for (int i = 0; i < info.Count; i++)
            {
                Vector2 textSize = Globals.assetSetter.fonts[0].MeasureString(info[i]);
                Vector2 labelPos = new Vector2(position.X, currentY);
                Label label = new Label(info[i], labelPos, 0);
                children.Add(label);

                currentY += textSize.Y; // Move down by the height of the current text
            }

            foreach (var child in children)
            {
                foreach (var component in child.components)
                {
                    component.IsStickToMouseCursor = true;
                }
            }
        }
    }
}
