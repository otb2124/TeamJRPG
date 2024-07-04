using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace TeamJRPG
{
    public class Label : UIComposite
    {


        public Vector2 textSize;

        public Label(string text, Vector2 startPosition, int fontID)
        {

            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);
            this.type = UICompositeType.TEXT_FRAME;

            SpriteFont font = Globals.assetSetter.fonts[fontID];


            textSize = font.MeasureString(text);
            Vector2 padding = new Vector2(40, 20);


            // Text
            UIComponent textElement = new UIComponent
            {
                position = new Vector2(this.position.X + padding.X / 2, this.position.Y + padding.Y / 2),
                texture = null,
                type = UIComponent.UIComponentType.TEXT,
                text = text,
                fontID = fontID,
                sourceRectangle = new Rectangle(0, 0, (int)textSize.X, (int)textSize.Y),
            };



            components.Add(textElement);


            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                components[i].IsStickToZoom = true;
            }

        }

    }
}
