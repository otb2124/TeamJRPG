using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System.ComponentModel.Design.Serialization;


namespace TeamJRPG
{
    public class MouseCursor : UIComposite
    {


        public MouseCursor()
        {
            type = UICompositeType.MOUSE_CURSOR;
            UIComponent component = new UIComponent();
            component.type = UIComponent.UIComponentType.MOUSE_CURSOR;
            component.texture = Globals.assetSetter.textures[4][0][0];
            component.IsStickToMouseCursor = true;
            component.IsStickToCamera = true;
            component.IsStickToZoom = true;
            component.sourceRectangle = new Rectangle(0, 0, component.texture.Width, component.texture.Height);
            components.Add(component);
            
        }
    }
}
