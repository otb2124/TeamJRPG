using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace TeamJRPG
{
    public class ImageHolder : UIComposite
    {
        public string floatingText = "empty";
        public FloatingInfoBox fib;
        public System.Drawing.RectangleF hintBox;
        public bool hintOn = false;


        public ImageHolder(Sprite sprite, Vector2 startPosition, Color color, Vector2 scale, Stroke stroke)
        {
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);

            UIComponent image = new UIComponent
            {
                color = color,
                position = position,
                sprite = sprite,
                scale = scale,
                sourceRectangle = new Rectangle(0, 0, sprite.srcRect.Width, sprite.srcRect.Height),
            };

            hintBox = new System.Drawing.RectangleF(position.X, position.Y, image.sprite.srcRect.Width * scale.X, image.sprite.srcRect.Height * scale.Y);

            if (stroke != null)
            {
                image.HasStroke = true;
                image.strokeSize = stroke.size;
                image.strokeColor = stroke.color;
                image.strokeType = stroke.effects;
            }



            components.Add(image);

            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                components[i].IsStickToZoom = true;
            }
        }


        public override void Update()
        {
            if (floatingText != "empty")
            {
                Vector2 cursorPos = Globals.inputManager.GetCursorPos();
                System.Drawing.PointF cursorPointF = new System.Drawing.PointF(cursorPos.X, cursorPos.Y);

                if (hintBox.Contains(cursorPointF))
                {
                    if (!hintOn)
                    {
                        if(floatingText != null)
                        {
                            fib = new FloatingInfoBox(new List<string>() { floatingText }, new List<Color>() { Color.White });
                            Globals.uiManager.AddElement(fib);
                            hintOn = true;
                        }
                        
                    }
                }
                else
                {
                    Globals.uiManager.RemoveElement(fib);
                    hintOn = false;
                }
            }


            base.Update();

        }
    }
}
