using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TeamJRPG
{
    public class Label : UIComposite
    {


        public Vector2 textSize;
        public Color textColor;
        public Stroke stroke;

        public Label(string text, Vector2 startPosition, int fontID, Color color, Stroke stroke)
        {
            this.stroke = stroke;
            this.textColor = color;
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);
            this.type = UICompositeType.TEXT_FRAME;

            SpriteFont font = Globals.assetSetter.fonts[fontID];


            textSize = font.MeasureString(text);
            Vector2 padding = new Vector2(40, 20);


            // Text
            UIComponent textElement = new UIComponent
            {
                position = new Vector2(this.position.X + padding.X / 2, this.position.Y + padding.Y / 2),
                type = UIComponent.UIComponentType.TEXT,
                text = text,
                fontID = fontID,
                sourceRectangle = new Rectangle(0, 0, (int)textSize.X, (int)textSize.Y),
                color = textColor,
            };

            if(stroke != null)
            {
                textElement.HasStroke = true;
                textElement.strokeSize = stroke.size;
                textElement.strokeColor = stroke.color;
                textElement.strokeType = stroke.effects;
            }



            components.Add(textElement);


            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                components[i].IsStickToZoom = true;
            }

        }

    }
}
