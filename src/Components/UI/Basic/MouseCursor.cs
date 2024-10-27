using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class MouseCursor : UIComposite
    {


        public MouseCursor()
        {
            type = UICompositeType.MOUSE_CURSOR;
            UIComponent component = new UIComponent();
            component.type = UIComponent.UIComponentType.MOUSE_CURSOR;
            component.sprite = Globals.textureManager.GetSprite(TextureManager.SheetCategory.ui, 0, new Vector2(0, 0), new Vector2(32, 32));
            component.IsStickToMouseCursor = true;
            component.IsStickToCamera = true;
            component.IsStickToZoom = true;
            component.sourceRectangle = new Rectangle(0, 0, component.sprite.srcRect.Width, component.sprite.srcRect.Height);
            components.Add(component);
            
        }
    }
}
