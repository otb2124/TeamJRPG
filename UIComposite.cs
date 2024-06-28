using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TeamJRPG
{
    public class UIComposite
    {

        public Vector2 position;

        public enum UICompositeType { MOUSE_CURSOR, TEXT_FRAME };
        public UICompositeType type;



        public List<UIComponent> components;

        public UIComposite() 
        {
            components = new List<UIComponent>();
        }


        public void Update()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update();
            }
        }



        public void Draw()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Draw();
            }
        }
    }
}
