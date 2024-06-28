
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class TextFrame : UIComposite
    {


        public TextFrame(string text, Vector2 position)
        {
            this.position = position;
            this.type = UICompositeType.TEXT_FRAME;

            int fontID = 4;
            Vector2 textSize = Globals.assetSetter.fonts[fontID].MeasureString(text);
            Vector2 padding = new Vector2(textSize.X + 20, textSize.Y);
            int rescale = 2;
            padding /= rescale;
            // Calculate scaling factors
            float singleCharacterWidth = textSize.X / text.Length;
            float horizontalScale = singleCharacterWidth / Globals.assetSetter.textures[4][1][1].Width * text.Length;
            float verticalScale = textSize.Y;

            // Create and position the UI elements with scaling
            float centredX = position.X - padding.X + Globals.assetSetter.textures[4][1][0].Width;


            UIComponent topCenter = new UIComponent();
            topCenter.position = new Vector2(centredX, position.Y);
            topCenter.texture = Globals.assetSetter.textures[4][1][1];
            topCenter.type = UIComponent.UIComponentType.FRAME_PART;
            topCenter.scale.X *= horizontalScale;

            UIComponent bottomCenter = new UIComponent();
            bottomCenter.position = new Vector2(centredX, position.Y + padding.Y);
            bottomCenter.texture = Globals.assetSetter.textures[4][1][1];
            bottomCenter.type = UIComponent.UIComponentType.FRAME_PART;
            bottomCenter.scale.X *= horizontalScale;
            bottomCenter.spriteEffects = SpriteEffects.FlipVertically;

            UIComponent leftTopCorner = new UIComponent();
            leftTopCorner.position = new Vector2(position.X, position.Y);
            leftTopCorner.texture = Globals.assetSetter.textures[4][1][0];
            leftTopCorner.type = UIComponent.UIComponentType.FRAME_PART;

            UIComponent leftBottomCorner = new UIComponent();
            leftBottomCorner.position = new Vector2(position.X, position.Y + padding.Y);
            leftBottomCorner.texture = Globals.assetSetter.textures[4][1][0];
            leftBottomCorner.type = UIComponent.UIComponentType.FRAME_PART;
            leftBottomCorner.spriteEffects = SpriteEffects.FlipVertically;

            UIComponent rightTopCorner = new UIComponent();
            rightTopCorner.position = new Vector2(position.X + padding.X, position.Y);
            rightTopCorner.texture = Globals.assetSetter.textures[4][1][0];
            rightTopCorner.type = UIComponent.UIComponentType.FRAME_PART;
            rightTopCorner.spriteEffects = SpriteEffects.FlipHorizontally;

            UIComponent rightBottomCorner = new UIComponent();
            rightBottomCorner.position = new Vector2(position.X + padding.X, position.Y + padding.Y);
            rightBottomCorner.texture = Globals.assetSetter.textures[4][1][0];
            rightBottomCorner.type = UIComponent.UIComponentType.FRAME_PART;
            rightBottomCorner.spriteEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;

            topCenter.scale /= rescale;
            bottomCenter.scale /= rescale;
            components.Add(topCenter);
            components.Add(bottomCenter);
            // Add UI elements to the list
            leftTopCorner.scale /= rescale;
            leftBottomCorner.scale /= rescale;
            rightBottomCorner.scale /= rescale;
            rightTopCorner.scale /= rescale;

            components.Add(leftTopCorner);
            components.Add(leftBottomCorner);
            components.Add(rightTopCorner);
            components.Add(rightBottomCorner);

            // Text
            UIComponent textElement = new UIComponent();
            textElement.position = new Vector2(position.X - padding.X + Globals.assetSetter.textures[4][1][0].Width, position.Y + Globals.assetSetter.textures[4][1][0].Height - textSize.Y);
            textElement.texture = null;
            textElement.type = UIComponent.UIComponentType.TEXT;
            textElement.text = text;
            textElement.fontID = fontID;
            textElement.scale /= rescale;
            components.Add(textElement);


            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                //components[i].IsStickToZoom = true;
            }

        }

    }
}
