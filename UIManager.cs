using SharpDX.Direct2D1.Effects;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TeamJRPG
{
    public class UIManager
    {

        public List<UIComposite> composites;
        public enum MenuState { inGameMenu, charachters, inventory, skills, questBook, statistics, map, settings, exit, clean };
        public MenuState currentMenuState;
        public bool MenuStateNeedsChange;

        public UIManager()
        {
            composites = new List<UIComposite>();
        }


        public void Init()
        {
            currentMenuState = MenuState.clean;
            MenuStateNeedsChange = true;
        }


        public void CheckState()
        {
            if (MenuStateNeedsChange)
            {

                RemoveAllCompositesOfTypes(UIComposite.UICompositeType.MOUSE_CURSOR, UIComposite.UICompositeType.INGAME_MENU, UIComposite.UICompositeType.INGAME_MENU_CHARACTERS, UIComposite.UICompositeType.INGAME_MENU_INVENTORY, UIComposite.UICompositeType.INGAME_MENU_SKILLS, UIComposite.UICompositeType.INGAME_MENU_QUESTBOOK, UIComposite.UICompositeType.INGAME_MENU_STATS, UIComposite.UICompositeType.INGAME_MENU_MAP, UIComposite.UICompositeType.INGAME_MENU_SETTINGS, UIComposite.UICompositeType.INGAME_MENU_EXIT);


                switch (currentMenuState)
                {
                    case MenuState.inGameMenu:
                        Add(UIComposite.UICompositeType.MOUSE_CURSOR);
                        Add(UIComposite.UICompositeType.INGAME_MENU);
                        break;
                    case MenuState.charachters:
                        Add(UIComposite.UICompositeType.MOUSE_CURSOR);
                        Add(UIComposite.UICompositeType.INGAME_MENU);
                        Add(UIComposite.UICompositeType.INGAME_MENU_CHARACTERS);
                        break;
                    case MenuState.inventory:
                        Add(UIComposite.UICompositeType.MOUSE_CURSOR);
                        Add(UIComposite.UICompositeType.INGAME_MENU);
                        Add(UIComposite.UICompositeType.INGAME_MENU_INVENTORY);
                        break;
                    case MenuState.skills:
                        Add(UIComposite.UICompositeType.MOUSE_CURSOR);
                        Add(UIComposite.UICompositeType.INGAME_MENU);
                        Add(UIComposite.UICompositeType.INGAME_MENU_SKILLS);
                        break;
                    case MenuState.questBook:
                        Add(UIComposite.UICompositeType.MOUSE_CURSOR);
                        Add(UIComposite.UICompositeType.INGAME_MENU);
                        Add(UIComposite.UICompositeType.INGAME_MENU_QUESTBOOK);
                        break;
                    case MenuState.statistics:
                        Add(UIComposite.UICompositeType.MOUSE_CURSOR);
                        Add(UIComposite.UICompositeType.INGAME_MENU);
                        Add(UIComposite.UICompositeType.INGAME_MENU_STATS);
                        break;
                    case MenuState.map:
                        Add(UIComposite.UICompositeType.MOUSE_CURSOR);
                        Add(UIComposite.UICompositeType.INGAME_MENU);
                        Add(UIComposite.UICompositeType.INGAME_MENU_MAP);
                        break;
                    case MenuState.settings:
                        Add(UIComposite.UICompositeType.MOUSE_CURSOR);
                        Add(UIComposite.UICompositeType.INGAME_MENU);
                        Add(UIComposite.UICompositeType.INGAME_MENU_SETTINGS);
                        break;
                    case MenuState.exit:
                        Add(UIComposite.UICompositeType.MOUSE_CURSOR);
                        Add(UIComposite.UICompositeType.INGAME_MENU);
                        Add(UIComposite.UICompositeType.INGAME_MENU_EXIT);
                        break;
                    case MenuState.clean:
                        RemoveAllCompositesOfTypes(UIComposite.UICompositeType.INGAME_MENU);
                        break;
                }



                MenuStateNeedsChange = false;
            }

        }



        public void Add(UIComposite.UICompositeType compositeType)
        {
            switch (compositeType)
            {
                case UIComposite.UICompositeType.MOUSE_CURSOR:
                    composites.Add(new MouseCursor());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU:
                    composites.Add(new InGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_CHARACTERS:
                    composites.Add(new CharactersInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_INVENTORY:
                    composites.Add(new InventoryInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_SKILLS:
                    composites.Add(new SkilsInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_QUESTBOOK:
                    composites.Add(new QuestBookInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_STATS:
                    composites.Add(new StatsInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_MAP:
                    composites.Add(new MapInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_SETTINGS:
                    composites.Add(new SettingsInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_EXIT:
                    composites.Add(new ExitInGameMenu());
                    break;

            }
        }


        public void RemoveAllCompositesOfTypes(params UIComposite.UICompositeType[] types)
        {
            foreach (var type in types)
            {
                foreach (var composite in composites.Where(c => c.type == type).ToList())
                {
                    composites.Remove(composite);
                }
            }
        }

        public void RemoveCompositeWithType(UIComposite.UICompositeType type)
        {

            for (int i = 0; i < composites.Count; i++)
            {
                if (composites[i].type == type)
                {
                    composites.Remove(composites[i]);
                    break;
                }
            }
        }

        public bool HasCompositesOfType(UIComposite.UICompositeType type)
        {
            return composites.Any(c => c.type == type);
        }


        public List<UIComposite> GetAllChildrenOfType(UIComposite.UICompositeType type)
        {
            List<UIComposite> result = new List<UIComposite>();
            foreach (var composite in composites)
            {
                result.AddRange(GetChildrenOfTypeRecursive(composite, type));
            }
            return result;
        }

        private List<UIComposite> GetChildrenOfTypeRecursive(UIComposite composite, UIComposite.UICompositeType type)
        {
            List<UIComposite> result = new List<UIComposite>();
            if (composite.type == type)
            {
                result.Add(composite);
            }
            foreach (var child in composite.children)
            {
                result.AddRange(GetChildrenOfTypeRecursive(child, type));
            }
            return result;
        }





        public void Update()
        {

            CheckState();

            for (int i = 0; i < composites.Count; i++)
            {
                composites[i].Update();
            }
        }

        public void Draw()
        {

            foreach (var composite in composites)
            {
                if (composite.type != UIComposite.UICompositeType.FLOATING_INFO_BOX && composite.type != UIComposite.UICompositeType.MOUSE_CURSOR)
                {
                    composite.Draw();
                }
            }

            // Draw the FloatingInfoBox
            foreach (var infoBox in composites)
            {
                if (infoBox.type == UIComposite.UICompositeType.FLOATING_INFO_BOX)
                {
                    infoBox.Draw();
                }
            }

            // Draw the cursor last
            foreach (var cursor in composites)
            {
                if (cursor.type == UIComposite.UICompositeType.MOUSE_CURSOR)
                {
                    cursor.Draw();
                }
            }
        }
    }
}
