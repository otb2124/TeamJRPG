﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TeamJRPG
{
    public class UIManager
    {

        public List<UIComposite> children = new List<UIComposite>();
        private List<UIComposite> elementsToAdd = new List<UIComposite>();
        private List<UIComposite> elementsToRemove = new List<UIComposite>();



        public enum MenuState { inGameMenu, charachters, inventory, skills, questBook, statistics, map, settings, exit, clean };
        public MenuState currentMenuState;
        public MenuState previousMenuState;
        public bool MenuStateNeedsChange;



        public UIManager()
        {
            children = new List<UIComposite>();
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

                RemoveAllCompositesOfTypes(UIComposite.UICompositeType.INGAME_MENU_CHARACTERS, UIComposite.UICompositeType.INGAME_MENU_INVENTORY, UIComposite.UICompositeType.INGAME_MENU_SKILLS, UIComposite.UICompositeType.INGAME_MENU_QUESTBOOK, UIComposite.UICompositeType.INGAME_MENU_STATS, UIComposite.UICompositeType.INGAME_MENU_MAP, UIComposite.UICompositeType.INGAME_MENU_SETTINGS, UIComposite.UICompositeType.INGAME_MENU_EXIT);


                switch (currentMenuState)
                {
                    case MenuState.inGameMenu:
                        if (!HasCompositesOfType(UIComposite.UICompositeType.INGAME_MENU))
                        {
                            Add(UIComposite.UICompositeType.INGAME_MENU);
                        }
                        if (!HasCompositesOfType(UIComposite.UICompositeType.MOUSE_CURSOR))
                        {
                            Add(UIComposite.UICompositeType.MOUSE_CURSOR);
                        }

                        break;
                    case MenuState.charachters:
                        Add(UIComposite.UICompositeType.INGAME_MENU_CHARACTERS);
                        break;
                    case MenuState.inventory:
                        Add(UIComposite.UICompositeType.INGAME_MENU_INVENTORY);
                        break;
                    case MenuState.skills:
                        Add(UIComposite.UICompositeType.INGAME_MENU_SKILLS);
                        break;
                    case MenuState.questBook:
                        Add(UIComposite.UICompositeType.INGAME_MENU_QUESTBOOK);
                        break;
                    case MenuState.statistics:
                        Add(UIComposite.UICompositeType.INGAME_MENU_STATS);
                        break;
                    case MenuState.map:
                        Add(UIComposite.UICompositeType.INGAME_MENU_MAP);
                        break;
                    case MenuState.settings:
                        Add(UIComposite.UICompositeType.INGAME_MENU_SETTINGS);
                        break;
                    case MenuState.exit:
                        Add(UIComposite.UICompositeType.INGAME_MENU_EXIT);
                        break;
                    case MenuState.clean:
                        RemoveAllCompositesOfTypes(UIComposite.UICompositeType.INGAME_MENU);
                        RemoveAllCompositesOfTypes(UIComposite.UICompositeType.MOUSE_CURSOR);
                        RemoveAllCompositesOfTypes(UIComposite.UICompositeType.FLOATING_INFO_BOX);
                        RemoveAllCompositesOfTypes(UIComposite.UICompositeType.DESCRIPTION_WINDOW);
                        if (GetAllChildrenOfType(UIComposite.UICompositeType.GROUP_BARS).Count == 0)
                        {
                            AddElement(new GroupmemberBars(new Vector2(0, 0)));
                        }
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
                    AddElement(new MouseCursor());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU:
                    AddElement(new InGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_CHARACTERS:
                    AddElement(new CharactersInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_INVENTORY:
                    AddElement(new InventoryInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_SKILLS:
                    AddElement(new SkilsInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_QUESTBOOK:
                    AddElement(new QuestBookInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_STATS:
                    AddElement(new StatsInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_MAP:
                    AddElement(new MapInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_SETTINGS:
                    AddElement(new SettingsInGameMenu());
                    break;
                case UIComposite.UICompositeType.INGAME_MENU_EXIT:
                    AddElement(new ExitInGameMenu());
                    break;

            }
        }


        public void RemoveAllCompositesOfTypes(params UIComposite.UICompositeType[] types)
        {
            foreach (var type in types)
            {
                foreach (var composite in children.Where(c => c.type == type).ToList())
                {
                    RemoveElement(composite);
                }
            }
        }

        public void RemoveCompositeWithType(UIComposite.UICompositeType type)
        {

            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].type == type)
                {
                    RemoveElement(children[i]);
                    break;
                }
            }
        }

        public bool HasCompositesOfType(UIComposite.UICompositeType type)
        {
            return children.Any(c => c.type == type);
        }


        public List<UIComposite> GetAllChildrenOfType(UIComposite.UICompositeType type)
        {
            List<UIComposite> result = new List<UIComposite>();
            foreach (var composite in children)
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



        public bool CheckMenuStateChange()
        {
            if(currentMenuState != previousMenuState)
            {
                previousMenuState = currentMenuState;
                return true;
            }

            return false;
        }



        public void Update()
        {

            CheckState();

            // Process all current children
            foreach (var child in children)
            {
                child.Update();
            }

            // Add new elements
            foreach (var element in elementsToAdd)
            {
                children.Add(element);
            }
            elementsToAdd.Clear();

            // Remove old elements
            foreach (var element in elementsToRemove)
            {
                children.Remove(element);
            }
            elementsToRemove.Clear();
        }

        public void Draw()
        {

            foreach (var composite in children)
            {
                if (composite.type != UIComposite.UICompositeType.FLOATING_INFO_BOX && composite.type != UIComposite.UICompositeType.MOUSE_CURSOR)
                {
                    composite.Draw();
                }
            }

            // Draw the FloatingInfoBox
            foreach (var infoBox in children)
            {
                if (infoBox.type == UIComposite.UICompositeType.FLOATING_INFO_BOX)
                {
                    infoBox.Draw();
                }
            }

            // Draw the cursor last
            foreach (var cursor in children)
            {
                if (cursor.type == UIComposite.UICompositeType.MOUSE_CURSOR)
                {
                    cursor.Draw();
                }
            }
        }




        public void AddElement(UIComposite element)
        {
            elementsToAdd.Add(element);
        }

        public void RemoveElement(UIComposite element)
        {
            elementsToRemove.Add(element);
        }












        
    }
}