using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG { 
    public class ItemHolder : UIComposite
    {

        public Vector2 frameSize;

        public ItemHolder(Item item, Vector2 startPosition)
        {
            this.position = startPosition;
            this.type = UICompositeType.ITEM_HOLDER;
            float scale = 1f;
            Vector2 padding = new Vector2 (10, 0);
            Frame frame = new Frame(position, new Vector2(item.texture.Width * scale + padding.X, item.texture.Height * scale + padding.Y));
            frameSize = frame.frameSize;
            components.AddRange(frame.components);

            ImageHolder icon = new ImageHolder(item.texture, position + padding*2, new Vector2(scale*1.5f, scale*1.5f));
            components.AddRange(icon.components);
        }
    }
}
