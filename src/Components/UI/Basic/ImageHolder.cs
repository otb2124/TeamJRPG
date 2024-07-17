using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace TeamJRPG
{
    public class ImageHolder : UIComposite
    {
        public List<string> floatingText;
        public List<Color> floatingTextColors;
        public FloatingInfoBox fib;
        public System.Drawing.RectangleF hintBox;
        public bool hintOn = false;

        public ImageHolder(Sprite sprite, Vector2 startPosition, Color color, Vector2 scale, Stroke stroke, bool HorFlip = false)
        {
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);

            floatingText = new List<string>();
            floatingTextColors = new List<Color>();



            SpriteEffects effects = SpriteEffects.None;
            if (HorFlip)
            {
                effects = SpriteEffects.FlipHorizontally;
            }

            UIComponent image = new UIComponent
            {
                color = color,
                position = position,
                sprite = sprite,
                scale = scale,
                sourceRectangle = new Rectangle(0, 0, sprite.srcRect.Width, sprite.srcRect.Height),
                spriteEffects = effects,
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
            if(Globals.currentGameState == Globals.GameState.ingamemenustate || Globals.currentGameState == Globals.GameState.dialoguestate || Globals.currentGameState == Globals.GameState.battle)
            {
                if (floatingText.Count > 0)
                {
                    Vector2 cursorPos = Globals.inputManager.GetCursorPos();
                    System.Drawing.PointF cursorPointF = new System.Drawing.PointF(cursorPos.X, cursorPos.Y);

                    if (hintBox.Contains(cursorPointF))
                    {
                        if (!hintOn)
                        {
                            if (floatingText.Count > 0)
                            {
                                if (floatingTextColors.Count <= 0)
                                {
                                    floatingTextColors = new List<Color>();
                                    for (global::System.Int32 i = 0; i < floatingText.Count; i++)
                                    {
                                        floatingTextColors.Add(Color.White);
                                    }
                                }
                                fib = new FloatingInfoBox(floatingText, floatingTextColors);
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
            }
            


            base.Update();

        }
    }
}
