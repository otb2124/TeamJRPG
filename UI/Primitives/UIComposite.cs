using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TeamJRPG
{
    public class UIComposite
    {

        public Vector2 position;

        public enum UICompositeType { MOUSE_CURSOR, TEXT_FRAME, SCROLLPANE, ITEM_HOLDER, FLOATING_INFO_BOX,
            INGAME_MENU, INGAME_MENU_CHARACTERS, INGAME_MENU_INVENTORY, INGAME_MENU_SKILLS, INGAME_MENU_QUESTBOOK, INGAME_MENU_STATS, INGAME_MENU_MAP, INGAME_MENU_SETTINGS, INGAME_MENU_EXIT,
        };
        public UICompositeType type;



        public List<UIComponent> components;
        public List<UIComposite> children;

        public UIComposite() 
        {
            components = new List<UIComponent>();
            children = new List<UIComposite>();
        }


        public virtual void Update()
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Update();
            }

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update();
            }

        }



        public virtual void Draw()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Draw();
            }

            for (int i = 0; i < children.Count; i++)
            {
                children[i].Draw();
            }
        }
    }
}
