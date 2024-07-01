using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace TeamJRPG
{
    public class Frame : UIComposite
    {

        public Vector2 frameSize;

        public Frame(Vector2 startPosition, Vector2 size)
        {

            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);
            this.type = UICompositeType.TEXT_FRAME;


            Vector2 textSize = size;
            Vector2 padding = new Vector2(40, 20);


            Texture2D backgroundTexture = Globals.assetSetter.textures[4][1][3];
            Texture2D cornerTexture = Globals.assetSetter.textures[4][1][0];
            Texture2D HborderTexture = Globals.assetSetter.textures[4][1][1];
            Texture2D VborderTexture = Globals.assetSetter.textures[4][1][2];

            Vector2 backgroundScale = new Vector2((textSize.X + padding.X) / backgroundTexture.Width, (textSize.Y + padding.Y) / backgroundTexture.Height);

            //BackGround
            UIComponent backGround = new UIComponent
            {
                position = position,
                texture = backgroundTexture,
                sourceRectangle = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height),
                scale = backgroundScale,
            };




            //Frame
            Vector2 frameScale = new Vector2(0.5f, 0.5f);
            Vector2 spriteEffectOffset = new Vector2(cornerTexture.Width * frameScale.X / 2, cornerTexture.Height * frameScale.Y / 2);
            frameSize = new Vector2(backgroundTexture.Width * backgroundScale.X, backgroundTexture.Height * backgroundScale.Y);

            //corners
            UIComponent topLeftCorner = new UIComponent
            {
                position = new Vector2(position.X - spriteEffectOffset.X, position.Y - spriteEffectOffset.Y),
                texture = cornerTexture,
                spriteEffects = SpriteEffects.None,
                scale = frameScale,
                sourceRectangle = new Rectangle(0, 0, cornerTexture.Width, cornerTexture.Height),
            };

            UIComponent bottomLeftCorner = new UIComponent
            {
                position = new Vector2(position.X - spriteEffectOffset.X, position.Y - (cornerTexture.Height * frameScale.Y) / 2 + frameSize.Y),
                texture = cornerTexture,
                spriteEffects = SpriteEffects.FlipVertically,
                scale = frameScale,
                sourceRectangle = new Rectangle(0, 0, cornerTexture.Width, cornerTexture.Height),
            };

            UIComponent topRightCorner = new UIComponent
            {
                position = new Vector2(position.X - (cornerTexture.Width * frameScale.X) / 2 + frameSize.X, position.Y - spriteEffectOffset.Y),
                texture = cornerTexture,
                spriteEffects = SpriteEffects.FlipHorizontally,
                scale = frameScale,
                sourceRectangle = new Rectangle(0, 0, cornerTexture.Width, cornerTexture.Height),
            };

            UIComponent bottomRightCorner = new UIComponent
            {
                position = new Vector2(position.X - (cornerTexture.Width * frameScale.X) / 2 + frameSize.X, position.Y - (cornerTexture.Height * frameScale.Y) / 2 + frameSize.Y),
                texture = cornerTexture,
                spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically,
                scale = frameScale,
                sourceRectangle = new Rectangle(0, 0, cornerTexture.Width, cornerTexture.Height),
            };




            //Borders

            //Horizontal
            float borderWidthScale = frameScale.X * ((textSize.X + padding.X) / HborderTexture.Width) * 2;

            UIComponent topBorder = new UIComponent
            {
                position = new Vector2(position.X - (HborderTexture.Width * borderWidthScale) / 2 + frameSize.X / 2, position.Y - spriteEffectOffset.Y),
                texture = HborderTexture,
                scale = new Vector2(borderWidthScale, frameScale.Y),
                sourceRectangle = new Rectangle(0, 0, HborderTexture.Width, HborderTexture.Height),
            };

            UIComponent bottomBorder = new UIComponent
            {
                position = new Vector2(position.X - (HborderTexture.Width * borderWidthScale) / 2 + frameSize.X / 2, position.Y - spriteEffectOffset.Y + frameSize.Y),
                texture = HborderTexture,
                scale = new Vector2(borderWidthScale, frameScale.Y),
                sourceRectangle = new Rectangle(0, 0, HborderTexture.Width, HborderTexture.Height),
            };


            //Vertical
            float borderHeightScale = frameScale.Y * ((textSize.Y + padding.Y) / VborderTexture.Height) * 2;

            UIComponent leftBorder = new UIComponent
            {
                position = new Vector2(position.X - spriteEffectOffset.X, position.Y),
                texture = VborderTexture,
                scale = new Vector2(frameScale.X, borderHeightScale),
                sourceRectangle = new Rectangle(0, 0, VborderTexture.Width, VborderTexture.Height),
            };

            UIComponent rightBorder = new UIComponent
            {
                position = new Vector2(position.X - spriteEffectOffset.X + frameSize.X, position.Y),
                texture = VborderTexture,
                scale = new Vector2(frameScale.X, borderHeightScale),
                sourceRectangle = new Rectangle(0, 0, VborderTexture.Width, VborderTexture.Height),
            };






            components.Add(backGround);

            components.Add(topLeftCorner);
            components.Add(bottomLeftCorner);
            components.Add(topRightCorner);
            components.Add(bottomRightCorner);

            components.Add(topBorder);
            components.Add(bottomBorder);
            components.Add(leftBorder);
            components.Add(rightBorder);


            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                components[i].IsStickToZoom = true;
            }

        }

    }
}
