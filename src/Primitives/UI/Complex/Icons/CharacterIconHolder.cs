using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;

namespace TeamJRPG
{
    public class CharacterIconHolder : UIComposite
    {

        public Sprite charSprite;

        public CharacterIconHolder(LiveEntity entity, Vector2 startPosition, Vector2 scale, Stroke stroke, string hint, int frameType)
        {
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);
            charSprite = entity.characterIcon;


            Sprite backg = entity.backGroundIcon;

            Stroke bgStroke = null;
            if(frameType == 0)
            {
                bgStroke = stroke;
            }
            ImageHolder backGround = new ImageHolder(backg, new Vector2(startPosition.X, startPosition.Y), Color.White, scale*2, bgStroke);
            

            ImageHolder charac = new ImageHolder(charSprite, new Vector2(startPosition.X, startPosition.Y), entity.skinColor, scale, null);
            if (hint != null)
            {
                charac.floatingText = hint;
            }


            /*Sprite frameTexture = Globals.assetSetter.textures[Globals.assetSetter.PLACEHOLDERS][0][0];
            switch (frameType) 
            {
                case 0:
                    break;
                case 1:
                    frameTexture = Globals.assetSetter.textures[Globals.assetSetter.UI][4][0];
                    break;
            }

            ImageHolder frame = new ImageHolder(frameTexture, new Vector2(startPosition.X, startPosition.Y), Color.White, scale, stroke);
            */
            
            children.Add(backGround);
            children.Add(charac);
            //children.Add(frame);
            



            

            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                components[i].IsStickToZoom = true;
            }
        }
    }
}
