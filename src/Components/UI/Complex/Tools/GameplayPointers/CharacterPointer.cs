using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class CharacterPointer : UIComposite
    {

        public float alpha;
        private float duration;
        public Entity entity;
        public ImageHolder pointer;
        public Sprite pointerSprite;
        public Vector2 offset;
        public Vector2 reposition;
        public Vector2 scale;

        public CharacterPointer(Entity entity, float duration = -1) 
        {
            type = UICompositeType.CHARACTER_POINTER;

            this.duration = duration;
            this.alpha = duration;
            this.entity = entity;
            pointerSprite = Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(32, 0), new Vector2(32, 32));
            if(duration == -1)
            {
                pointerSprite = Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 2, new Vector2(32, 0), new Vector2(32, 32));
            }
            Sprite entitySprite = entity.sprites[0];
            scale = new Vector2(2, 2);
            float xOffset = (entitySprite.Width*Globals.gameScale - pointerSprite.Width * scale.X) / 2;
            offset = new Vector2(xOffset, -entitySprite.Height - 12);

            RefreshPointer();
        }


        public override void Update()
        {
            if(duration != -1)
            {
                alpha -= 0.05f;
            }
            else
            {
                alpha = 1;
            }
            
            


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

            if(Globals.currentGameState == Globals.GameState.battleState)
            {
                LiveEntity lent = (LiveEntity)entity;
                position = lent.battleScreenPosition;
            }
            else
            {
                position = entity.position;
            }
            
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
