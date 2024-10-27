using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class Frame : UIComposite
    {

        public Vector2 frameSize;

        public Frame(Vector2 startPosition, Vector2 size)
        {
            float generalScale = 3.5f;
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);
            this.type = UICompositeType.TEXT_FRAME;


            Vector2 textSize = size;
            Vector2 padding = new Vector2(40, 20);


            Sprite backgroundTexture = Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(32*3, 32), new Vector2(32, 32));
            Sprite cornerTexture = Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(32 * 0, 32), new Vector2(32, 32));
            Sprite HborderTexture = Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(32 * 1, 32), new Vector2(32, 32));
            Sprite VborderTexture = Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(32 * 2, 32), new Vector2(32, 32));


            Vector2 backgroundScale = new Vector2((textSize.X + padding.X - 8) / backgroundTexture.srcRect.Width, (textSize.Y + padding.Y) / backgroundTexture.srcRect.Height);

            //BackGround
            UIComponent backGround = new UIComponent
            {
                position = position,
                sprite = backgroundTexture,
                scale = backgroundScale,
                color = Color.Black
            };




            //Frame
            Vector2 frameScale = new Vector2(0.5f, 0.5f);
            frameScale *= generalScale;


            Vector2 spriteEffectOffset = new Vector2(cornerTexture.srcRect.Width * frameScale.X / 2, cornerTexture.srcRect.Height * frameScale.Y / 2);
            frameSize = new Vector2(backgroundTexture.srcRect.Width * backgroundScale.X, backgroundTexture.srcRect.Height * backgroundScale.Y);

            //corners
            UIComponent topLeftCorner = new UIComponent
            {
                position = new Vector2(position.X - spriteEffectOffset.X, position.Y - spriteEffectOffset.Y),
                sprite = cornerTexture,
                spriteEffects = SpriteEffects.None,
                scale = frameScale,
                sourceRectangle = new Rectangle(0, 0, cornerTexture.srcRect.Width, cornerTexture.srcRect.Height),
            };

            UIComponent bottomLeftCorner = new UIComponent
            {
                position = new Vector2(position.X - spriteEffectOffset.X, position.Y - (cornerTexture.srcRect.Height * frameScale.Y) / 2 + frameSize.Y),
                sprite = cornerTexture,
                spriteEffects = SpriteEffects.FlipVertically,
                scale = frameScale,
                sourceRectangle = new Rectangle(0, 0, cornerTexture.srcRect.Width, cornerTexture.srcRect.Height),
            };

            UIComponent topRightCorner = new UIComponent
            {
                position = new Vector2(position.X - (cornerTexture.srcRect.Width * frameScale.X) / 2 + frameSize.X, position.Y - spriteEffectOffset.Y),
                sprite = cornerTexture,
                spriteEffects = SpriteEffects.FlipHorizontally,
                scale = frameScale,
                sourceRectangle = new Rectangle(0, 0, cornerTexture.srcRect.Width, cornerTexture.srcRect.Height),
            };

            UIComponent bottomRightCorner = new UIComponent
            {
                position = new Vector2(position.X - (cornerTexture.srcRect.Width * frameScale.X) / 2 + frameSize.X, position.Y - (cornerTexture.srcRect.Height * frameScale.Y) / 2 + frameSize.Y),
                sprite = cornerTexture,
                spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically,
                scale = frameScale,
                sourceRectangle = new Rectangle(0, 0, cornerTexture.srcRect.Width, cornerTexture.srcRect.Height),
            };




            //Borders

            //Horizontal
            float borderWidthScale = frameScale.X / generalScale * ((textSize.X + padding.X) / HborderTexture.srcRect.Width) * 2;

            UIComponent topBorder = new UIComponent
            {
                position = new Vector2(position.X - (HborderTexture.srcRect.Width * borderWidthScale) / 2 + frameSize.X / 2, position.Y - spriteEffectOffset.Y),
                sprite = HborderTexture,
                scale = new Vector2(borderWidthScale, frameScale.Y),
                sourceRectangle = new Rectangle(0, 0, HborderTexture.srcRect.Width, HborderTexture.srcRect.Height),
            };

            UIComponent bottomBorder = new UIComponent
            {
                position = new Vector2(position.X - (HborderTexture.srcRect.Width * borderWidthScale) / 2 + frameSize.X / 2, position.Y - spriteEffectOffset.Y + frameSize.Y),
                sprite = HborderTexture,
                scale = new Vector2(borderWidthScale, frameScale.Y),
                sourceRectangle = new Rectangle(0, 0, HborderTexture.srcRect.Width, HborderTexture.srcRect.Height),
            };


            //Vertical
            float borderHeightScale = frameScale.Y / generalScale * ((textSize.Y + padding.Y) / VborderTexture.srcRect.Height) * 2;

            UIComponent leftBorder = new UIComponent
            {
                position = new Vector2(position.X - spriteEffectOffset.X, position.Y),
                sprite = VborderTexture,
                scale = new Vector2(frameScale.X, borderHeightScale),
                sourceRectangle = new Rectangle(0, 0, VborderTexture.srcRect.Width, VborderTexture.srcRect.Height),
            };

            UIComponent rightBorder = new UIComponent
            {
                position = new Vector2(position.X - spriteEffectOffset.X + frameSize.X, position.Y),
                sprite = VborderTexture,
                scale = new Vector2(frameScale.X, borderHeightScale),
                sourceRectangle = new Rectangle(0, 0, VborderTexture.srcRect.Width, VborderTexture.srcRect.Height),
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
