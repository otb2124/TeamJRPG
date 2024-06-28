using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class UIComponent
    {

        //for drawing
        public Texture2D texture;
        public Vector2 position; //needs reload
        public Color color;
        public float rotation;
        public Vector2 origin; //needs reload
        public Vector2 scale; //needs reload
        public SpriteEffects spriteEffects;

        public Vector2 adjustedPosition;
        public Vector2 adjustedOrigin;
        public Vector2 adjustedScale;

        //text
        public string text;
        public int fontID;



        //conditions
        public bool IsStickToCamera;
        public bool IsStickToZoom;
        public bool IsStickToMouseCursor;
        public bool IsHalfedOrigin;


        //for sorting in list
        public enum UIComponentType { MOUSE_CURSOR, TEXT, FRAME_PART}
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
        }


        //for updates
        public void Update()
        {
            adjustedPosition = position;
            adjustedOrigin = origin;
            adjustedScale = scale;

            

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



            if (IsHalfedOrigin)
            {
                if (type == UIComponentType.TEXT)
                {
                    adjustedOrigin = new Vector2(Globals.assetSetter.fonts[fontID].MeasureString(text).X / 2, Globals.assetSetter.fonts[fontID].MeasureString(text).Y / 2);
                }
                else
                {
                    adjustedOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
                }
            }
        }


        //for drawing
        public void Draw() 
        {

            if (type != UIComponentType.TEXT)
            {
                Globals.spriteBatch.Draw(texture, adjustedPosition, null, color, rotation, adjustedOrigin, adjustedScale, spriteEffects, 0f);
            }
            else
            {
                Globals.spriteBatch.DrawString(Globals.assetSetter.fonts[fontID], text, adjustedPosition, color, rotation, adjustedOrigin, adjustedScale, spriteEffects, 0f);
            }
            
        }
    }
}
