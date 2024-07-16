using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class ChoicePointer : UIComposite
    {

        public Vector2 size;

        public ChoicePointer(Vector2 startPos, bool HorFlip = false) 
        {

            this.position = startPos;

            Sprite sprite = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 1, new Vector2(0, 96), new Vector2(32, 32));
            this.size = sprite.size;


            ImageHolder imgHolder = new ImageHolder(sprite, position, Color.White, Vector2.One, null, HorFlip);
            children.Add(imgHolder);
        }
    }
}
