using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class CurrentCharacterPointer : UIComposite
    {

        public float alpha;
        public Entity entity;
        public ImageHolder pointer;
        public Sprite pointerSprite;
        public Vector2 offset;
        public Vector2 reposition;
        public Vector2 scale;

        public CurrentCharacterPointer(Entity entity, float duration) 
        {
            type = UICompositeType.CURRENT_CHARACTER_POINTER;

            this.alpha = duration;
            this.entity = entity;
            pointerSprite = Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(32, 0), new Vector2(32, 32));
            Sprite entitySprite = entity.sprites[0];
            scale = new Vector2(2, 2);
            float xOffset = (entitySprite.Width*Globals.gameScale - pointerSprite.Width * scale.X) / 2;
            offset = new Vector2(xOffset, -entitySprite.Height - 12);

            RefreshPointer();
        }


        public override void Update()
        {
            alpha-=0.05f;



            RefreshPointer();


            if (alpha < 0)
            {
                Globals.uiManager.RemoveCompositeWithType(type);
            }

            base.Update();
        }



        public void RefreshPointer()
        {
            children.Clear();

            position = entity.position;
            reposition = new Vector2(position.X - Globals.camera.position.X + Globals.camera.viewport.Width / 2, position.Y - Globals.camera.position.Y + Globals.camera.viewport.Height / 2);
            pointer = new ImageHolder(pointerSprite, reposition + offset, Color.White * alpha, scale, null);

            foreach(var component in pointer.components)
            {
                component.IsStickToZoom = false;
            }

            children.Add(pointer);
        }
    }
}
