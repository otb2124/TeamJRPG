using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame;


namespace TeamJRPG
{
    public class UIComponent
    {

        //for drawing
        public Texture2D texture;
        public Vector2 position; //needs reload
        public Rectangle sourceRectangle;
        public Color color;
        public float rotation;
        public Vector2 origin; //needs reload
        public Vector2 scale; //needs reload
        public SpriteEffects spriteEffects;

        public Vector2 adjustedPosition;
        public Vector2 adjustedOrigin;
        public Vector2 adjustedScale;
        public Rectangle adjustedSourceRectangle;

        //text
        public string text;
        public int fontID;

        //stroke
        public bool HasStroke;
        public int strokeSize;
        public Color strokeColor;
        public StrokeType strokeType;

        //conditions
        //general
        public bool IsStickToCamera;
        public bool IsStickToZoom;
        public bool IsStickToMouseCursor;
        public bool IsHalfedOriginX;
        public bool IsHalfedOriginY;

        //specific
        public bool DrawHead;
       

        //for sorting in list
        public enum UIComponentType { MOUSE_CURSOR, TEXT, FRAME_PART }
        public UIComponentType type;


        //for defaults
        public UIComponent()
        {
            position = Vector2.Zero;
            texture = null;
            color = Color.White;
            rotation = 0;
            origin = Vector2.Zero;
            scale = new Vector2(1, 1);
            spriteEffects = SpriteEffects.None;
            sourceRectangle = new Rectangle(0, 0, 0, 0);
        }


        //for updates
        public void Update()
        {
            adjustedPosition = position;
            adjustedOrigin = origin;
            adjustedScale = scale;
            adjustedSourceRectangle = sourceRectangle;


            if (IsStickToMouseCursor)
            {
                adjustedPosition += Globals.inputManager.GetCursorPos();
            }

            if (IsStickToZoom)
            {
                adjustedPosition /= Globals.camera.zoom;
                adjustedScale /= Globals.camera.zoom;
            }

            if (IsStickToCamera)
            {
                adjustedPosition += Globals.camera.position;
            }



            if (IsHalfedOriginX)
            {
                if (type == UIComponentType.TEXT)
                {
                    adjustedOrigin = new Vector2(Globals.assetSetter.fonts[fontID].MeasureString(text).X / 2, origin.Y);
                }
                else
                {
                    adjustedOrigin = new Vector2(texture.Width / 2, origin.Y);
                }
            }

            if (IsHalfedOriginY)
            {
                if (type == UIComponentType.TEXT)
                {
                    adjustedOrigin = new Vector2(origin.X, Globals.assetSetter.fonts[fontID].MeasureString(text).Y / 2);
                }
                else
                {
                    adjustedOrigin = new Vector2(origin.X, texture.Height / 2);
                }
            }


            if (DrawHead)
            {
                adjustedSourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height / 4);
            }
        }


        //for drawing
        public void Draw()
        {


            Texture2D textureToDraw = texture;


            if (type != UIComponentType.TEXT)
            {
                if (HasStroke)
                {
                    textureToDraw = StrokeEffect.CreateStroke(texture, strokeSize, strokeColor, Globals.graphics.GraphicsDevice, strokeType);
                }

                Globals.spriteBatch.Draw(textureToDraw, adjustedPosition, adjustedSourceRectangle, color, rotation, adjustedOrigin, adjustedScale, spriteEffects, 0f);
            }
            else
            {
                if (HasStroke)
                {
                    textureToDraw = StrokeEffect.CreateStrokeSpriteFont(Globals.assetSetter.fonts[fontID], text, color, Vector2.One, strokeSize, strokeColor, Globals.graphics.GraphicsDevice, strokeType);
                    Globals.spriteBatch.Draw(textureToDraw, adjustedPosition, adjustedSourceRectangle, strokeColor, rotation, adjustedOrigin, adjustedScale, spriteEffects, 0f);
                }
                else
                {
                    Globals.spriteBatch.DrawString(Globals.assetSetter.fonts[fontID], text, adjustedPosition, color, rotation, adjustedOrigin, adjustedScale, spriteEffects, 0f);
                }
                
            }

        }
    }
}
