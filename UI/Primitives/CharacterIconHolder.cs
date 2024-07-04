using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class CharacterIconHolder : UIComposite
    {

        public Texture2D texture;

        public CharacterIconHolder(Entity entity, Vector2 startPosition, Vector2 scale)
        {
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width / 2, startPosition.Y - Globals.camera.viewport.Height / 2);
            texture = entity.characterIcon;


            Texture2D backg= Globals.assetSetter.textures[Globals.assetSetter.UI][5][0];
            ImageHolder backGround = new ImageHolder(backg, new Vector2(startPosition.X, startPosition.Y), scale);
            components.AddRange(backGround.components);


            UIComponent image = new UIComponent
            {
                position = position,
                texture = texture,
                sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height),
                scale = scale,
            };

            components.Add(image);



            

            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                components[i].IsStickToZoom = true;
            }
        }
    }
}
