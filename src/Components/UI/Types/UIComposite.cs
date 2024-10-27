using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TeamJRPG
{
    public class UIComposite
    {

        public Vector2 position;

        public enum UICompositeType { MOUSE_CURSOR, TEXT_FRAME, SCROLLPANE, ITEM_HOLDER, FLOATING_INFO_BOX, BUTTON, GROUP_BARS, DESCRIPTION_WINDOW, CHARACTER_POINTER,
            INGAME_MENU, INGAME_MENU_CHARACTERS, INGAME_MENU_INVENTORY, INGAME_MENU_SKILLS, INGAME_MENU_QUESTBOOK, INGAME_MENU_STATS, INGAME_MENU_MAP, INGAME_MENU_SETTINGS, INGAME_MENU_EXIT,
            MAIN_MENU, PRESS_ANY_KEY, MAIN_MENU_CONTENT,
            DIALOGUE_TEXT_BOX,
            BATTLE_TURN_BAR, BATTLE_MENU, BATTLE_SKILL_MENU, BATTLE_CONSUMABLE_MENU, BATTLE_INTERRACTION_MENU, BATTLE_TARGET_MENU, BATTLE_STATUS,
            GAMEOVER_MENU,
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
