using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;

namespace TeamJRPG
{
    public class CharacterIconHolder : UIComposite
    {

        public Texture2D texture;

        public CharacterIconHolder(Entity entity, Vector2 startPosition, Vector2 scale, Stroke stroke, string hint, int frameType)
        {
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);
            texture = entity.characterIcon;


            Texture2D backg= Globals.assetSetter.textures[Globals.assetSetter.UI][5][0];

            Stroke bgStroke = null;
            if(frameType == 0)
            {
                bgStroke = stroke;
            }
            ImageHolder backGround = new ImageHolder(backg, new Vector2(startPosition.X, startPosition.Y), Color.White, scale, bgStroke);
            

            ImageHolder charac = new ImageHolder(texture, new Vector2(startPosition.X, startPosition.Y), entity.skinColor, scale, null);
            if (hint != null)
            {
                charac.floatingText = hint;
            }


            Texture2D frameTexture = Globals.assetSetter.textures[Globals.assetSetter.PLACEHOLDERS][0][0];
            switch (frameType) 
            {
                case 0:
                    break;
                case 1:
                    frameTexture = Globals.assetSetter.textures[Globals.assetSetter.UI][4][0];
                    break;
            }

            ImageHolder frame = new ImageHolder(frameTexture, new Vector2(startPosition.X, startPosition.Y), Color.White, scale, stroke);

            
            children.Add(backGround);
            children.Add(charac);
            children.Add(frame);
            



            

            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                components[i].IsStickToZoom = true;
            }
        }
    }
}
