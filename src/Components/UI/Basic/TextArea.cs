using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System.Collections.Generic;
using System.Text;

namespace TeamJRPG
{
    public class TextArea : UIComposite
    {
        public Vector2 textSize;
        public Color textColor;
        public Color defautColor;
        public Stroke stroke;
        public int maxWidth;
        public int maxHeight;
        public List<string> lines;
        public int fontID;


        public TextArea(string text, Vector2 startPosition, int fontID, Color color, Stroke stroke, int maxWidth, int maxHeight)
        {
            this.stroke = stroke;
            this.textColor = color;
            this.defautColor = color;
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);
            this.type = UICompositeType.TEXT_FRAME;
            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            this.fontID = fontID;

            SpriteFont font = Globals.assetSetter.fonts[fontID];

            lines = WrapText(text, font, maxWidth);

            Vector2 padding = new Vector2(40, 20);
            Vector2 textPosition = new Vector2(this.position.X + padding.X / 2, this.position.Y + padding.Y / 2);
            Vector2 currentPosition = textPosition;

            foreach (string line in lines)
            {
                if (currentPosition.Y - textPosition.Y >= maxHeight)
                {
                    break; // Stop adding lines if the maximum height is reached
                }

                UIComponent textElement = new UIComponent
                {
                    position = currentPosition,
                    type = UIComponent.UIComponentType.TEXT,
                    text = line,
                    fontID = fontID,
                    sourceRectangle = new Rectangle(0, 0, (int)font.MeasureString(line).X, (int)font.MeasureString(line).Y),
                    color = textColor,
                };

                if (stroke != null)
                {
                    textElement.HasStroke = true;
                    textElement.strokeSize = stroke.size;
                    textElement.strokeColor = stroke.color;
                    textElement.strokeType = stroke.effects;
                }

                components.Add(textElement);

                currentPosition.Y += font.LineSpacing;
            }

            foreach (var component in components)
            {
                component.IsStickToCamera = true;
                component.IsStickToZoom = true;
            }

            textSize = new Vector2(maxWidth, currentPosition.Y - textPosition.Y);

        }



        private List<string> WrapText(string text, SpriteFont font, int maxWidth)
        {
            List<string> lines = new List<string>();
            string[] paragraphs = text.Split(new[] { "\n" }, System.StringSplitOptions.None);

            foreach (var paragraph in paragraphs)
            {
                string[] words = paragraph.Split(' ');
                StringBuilder sb = new StringBuilder();
                float lineWidth = 0f;

                foreach (string word in words)
                {
                    Vector2 size = font.MeasureString(word + " ");
                    if (lineWidth + size.X < maxWidth)
                    {
                        sb.Append(word + " ");
                        lineWidth += size.X;
                    }
                    else
                    {
                        if (sb.Length > 0)
                        {
                            lines.Add(sb.ToString().TrimEnd());
                            sb.Clear();
                        }

                        sb.Append(word + " ");
                        lineWidth = size.X;
                    }
                }

                if (sb.Length > 0)
                {
                    lines.Add(sb.ToString().TrimEnd());
                }

                lines.Add(""); // Add an empty line to separate paragraphs
            }

            return lines;
        }

        private string BreakWord(string word, SpriteFont font, int maxWidth)
        {
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;

            foreach (char c in word)
            {
                Vector2 size = font.MeasureString(c.ToString());
                if (lineWidth + size.X < maxWidth)
                {
                    sb.Append(c);
                    lineWidth += size.X;
                }
                else
                {
                    sb.Append('-');
                    break;
                }
            }

            return sb.ToString();
        }
    }
}
