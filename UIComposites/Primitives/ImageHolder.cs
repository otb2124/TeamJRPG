using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class ImageHolder : UIComposite
    {

        public ImageHolder(Texture2D texture, Vector2 startPosition, Vector2 scale)
        {
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);

            UIComponent image = new UIComponent
            {
                position = position,
                texture = texture,
                scale = scale,
                sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height),
            };

            components.Add(image);

            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                components[i].IsStickToZoom = true;
            }
        }
    }
}
