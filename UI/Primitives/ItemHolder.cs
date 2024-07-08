using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;


namespace TeamJRPG
{
    public class ItemHolder : UIComposite
    {
        public Vector2 frameSize;
        public System.Drawing.RectangleF itemBoxOld;
        public System.Drawing.RectangleF itemBox;
        public Item item;
        public FloatingInfoBox infoBox;
        public FloatingButtonMenu buttonMenu;
        public ImageHolder drag;

        public bool buttonMenuOn = false;
        public bool infoOn = false;
        public bool dragOn = false;

        public bool IsDraggedAndDropped = false;
        public bool IsDragging = false;
        public bool WasDragging = false; // New state to track if dragging was happening
        public static bool IsAnyItemDragging = false;


        public int equipmentSlotId = -1;



        public bool IsGlowing = false;
        public bool IsClone = false;


        public ItemHolder(Item item, Vector2 startPosition)
        {
            this.position = startPosition;
            this.item = item;
            this.type = UICompositeType.ITEM_HOLDER;
            float scale = 1f;
            Vector2 padding = new Vector2(10, 0);
            Frame frame = new Frame(position, new Vector2(item.texture.Width * scale + padding.X, item.texture.Height * scale + padding.Y));
            frameSize = frame.frameSize;
            components.AddRange(frame.components);
            children.Add(frame);

            ImageHolder icon = new ImageHolder(item.texture, new Vector2(position.X + padding.X + 16 / 4, position.Y + padding.Y), new Vector2(scale * 1.5f, scale * 1.5f));
            components.AddRange(icon.components);
            children.Add(icon);

            Label amountLabel = new Label(item.amount.ToString(), position + frameSize / 2, 1, Color.Black, new Stroke(2, Color.White, MonoGame.StrokeType.OutlineAndTexture));
            if (item.IsStackable && item.amount > 1)
            {
                children.Add(amountLabel);
            }

            Vector2 boxPos = new Vector2(position.X - Globals.camera.viewport.Width / 2, position.Y - Globals.camera.viewport.Height / 2);
            itemBoxOld = new System.Drawing.RectangleF(boxPos.X, boxPos.Y, frameSize.X, frameSize.Y);
            itemBox = itemBoxOld;
        }

        public override void Update()
        {
            Vector2 cursorPos = Globals.inputManager.GetCursorPos();
            System.Drawing.PointF cursorPointF = new System.Drawing.PointF(cursorPos.X, cursorPos.Y);

            if (itemBox.Contains(cursorPointF))
            {
                if (!infoOn)
                {
                    SetInfoList(out List<string>infoList, out List<Color>colors);


                    infoBox = new FloatingInfoBox(infoList, colors);
                    Globals.uiManager.AddElement(infoBox);

                    infoOn = true;
                }

                if (Globals.inputManager.IsMouseButtonClick(InputManager.MouseButton.Right))
                {
                    if (!buttonMenuOn)
                    {
                        Vector2 adjustedPos = new Vector2(cursorPos.X + Globals.camera.viewport.Width / 2, cursorPos.Y + Globals.camera.viewport.Height / 2);

                        List<string> textButtonTexts = new List<string>
                        {
                            "Equip",
                            "Drop",
                            "Destroy",
                        };

                        TextButton[] buttons = new TextButton[textButtonTexts.Count];

                        for (int i = 0; i < textButtonTexts.Count; i++)
                        {
                            buttons[i] = new TextButton(textButtonTexts[i], new Vector2(adjustedPos.X, adjustedPos.Y + ((Globals.assetSetter.fonts[1].MeasureString("K").Y + 28) * i)), i + 40);
                        }

                        buttonMenu = new FloatingButtonMenu(buttons.ToArray());
                        Globals.uiManager.AddElement(buttonMenu);

                        // Calculate the frame frameSize based on the total height of all buttons and the width of the longest button
                        float totalHeight = buttons.Sum(button => button.frameSize.Y);
                        float maxWidth = buttons.Max(button => button.frameSize.X);

                        itemBox.Width += maxWidth;
                        itemBox.Height += totalHeight;

                        buttonMenuOn = true;
                        Globals.uiManager.RemoveElement(infoBox);
                    }
                }

                if (Globals.inputManager.IsMouseButtonHold(InputManager.MouseButton.Left) && !buttonMenuOn)
                {
                    if (!Globals.uiManager.IsDraggingItemInInventory)
                    {
                        itemBox.X = 0 - Globals.camera.viewport.Width / 2;
                        itemBox.Y = 0 - Globals.camera.viewport.Height / 2;
                        itemBox.Height = Globals.camera.viewport.Height;
                        itemBox.Width = Globals.camera.viewport.Width;
                    }
                    






                    Globals.uiManager.RemoveElement(infoBox);
                    Globals.uiManager.RemoveElement(buttonMenu);


                    IsDraggedAndDropped = false;

                    if (!dragOn && !Globals.uiManager.IsDraggingItemInInventory)
                    {
                        Vector2 adjustedPos = new Vector2(Globals.camera.viewport.Width / 2, Globals.camera.viewport.Height / 2);
                        drag = new ImageHolder(item.texture, adjustedPos, new Vector2(2, 2));
                        for (int i = 0; i < drag.components.Count; i++)
                        {
                            drag.components[i].IsStickToMouseCursor = true;
                            drag.components[i].color = new Color((int)drag.components[i].color.R, (int)drag.components[i].color.G, (int)drag.components[i].color.B, 150);
                        }
                        children.Add(drag);
                        IsDragging = true;
                        dragOn = true;
                        Globals.uiManager.IsDraggingItemInInventory = true;
                    }
                }
                else
                {
                    if (IsDragging) // Only set IsDraggedAndDropped if an item was actually being dragged
                    {
                        IsDraggedAndDropped = true;
                    }
                    IsDragging = false;
                    if (!buttonMenuOn)
                    {
                        itemBox = itemBoxOld;
                    }
                    Globals.uiManager.IsDraggingItemInInventory = false;

                }
            }
            else
            {
                Globals.uiManager.RemoveElement(infoBox);
                Globals.uiManager.RemoveElement(buttonMenu);
                children.Remove(drag);

                infoOn = false;
                buttonMenuOn = false;
                dragOn = false;

                if (!buttonMenuOn)
                {
                    itemBox = itemBoxOld;
                }
                
            }

            // Track if the item was dragged in the previous update
            WasDragging = IsDragging;


            base.Update();
        }


        public override void Draw()
        {
            if (IsGlowing && equipmentSlotId > -1)
            {

                children[0].components[0].color = Color.Yellow * 0.5f;

            }
            else
            {
                children[0].components[0].color = Color.Black;
            }


            if (IsClone)
            {
                children[1].components[0].color = Color.White * 0.5f;
            }

            base.Draw();
        }




        public void SetInfoList(out List<string> infoList, out List<Color> colors)
        {
            infoList = new List<string>();
            colors = new List<Color>();

            
            infoList.Add(item.name);
            colors.Add(Color.White);


            if (!item.name.Contains("Slot"))
            {
                string itemType = GetItemType(item);
                infoList.Add(itemType);
                colors.Add(Color.Gray);

                if (item is Weapon weapon)
                {
                    string weaponSlotType = GetWeaponSlotType(weapon);
                    infoList.Add(weaponSlotType);
                    colors.Add(Color.Gray);

                    AddWeaponInfo(weapon, infoList, colors);
                }
                else if (item is Armor armor)
                {
                    AddArmorInfo(armor, infoList, colors);
                }


                infoList.Add(item.description);
                colors.Add(Color.LightGray);

                AddValueInfo(item, infoList, colors);
            }
        }

        private string GetItemType(Item item)
        {
            return item.type switch
            {
                Item.ItemType.WEAPON => GetWeaponType(item as Weapon),
                Item.ItemType.ARMOR => GetArmorType(item as Armor),
                Item.ItemType.CONSUMABLE => GetConsumableType(item as Consumable),
                Item.ItemType.MATERIAL => "Material",
                Item.ItemType.VALUEABLE => "Valuable",
                Item.ItemType.QUEST => "Quest Item",
                _ => "Unknown"
            };
        }

        

        private string GetWeaponType(Weapon weapon)
        {
            return weapon.weaponType switch
            {
                Weapon.WeaponType.sword => "Sword",
                Weapon.WeaponType.mace => "Mace",
                Weapon.WeaponType.axe => "Axe",
                Weapon.WeaponType.staff => "Staff",
                Weapon.WeaponType.shield => "Shield",
                Weapon.WeaponType.bow => "Bow",
                Weapon.WeaponType.crossbow => "Crossbow",
                Weapon.WeaponType.magicStaff => "Magic Staff",
                _ => "Weapon"
            };
        }
        private string GetArmorType(Armor armor)
        {
            return armor.slotType switch
            {
                Armor.SlotType.helmet => "Helmet",
                Armor.SlotType.chestplate => "Chestplate",
                Armor.SlotType.cape => "Cape",
                Armor.SlotType.gloves => "Gloves",
                Armor.SlotType.boots => "Boots",
                Armor.SlotType.ring => "Ring",
                Armor.SlotType.belt => "Belt",
                Armor.SlotType.necklace => "Necklace",
                _ => "Armor"
            };
        }


        private string GetConsumableType(Consumable consumable)
        {
            return consumable.consumableType switch
            {
                Consumable.ConsumableType.food => "Food",
                Consumable.ConsumableType.potion => "Potion",
                Consumable.ConsumableType.throwable => "Throwable",
                _ => "Consumable"
            };
        }

        private string GetWeaponSlotType(Weapon weapon)
        {
            return weapon.slotType switch
            {
                Weapon.SlotType.oneHanded => "One-Handed",
                Weapon.SlotType.twoHanded => "Two-Handed",
                _ => "Empty"
            };
        }

        private void AddWeaponInfo(Weapon weapon, List<string> infoList, List<Color> colors)
        {

            if (weapon.PhysicalDMG != 0 || weapon.MagicalDMG != 0)
            {
                if (weapon.PhysicalDMG != 0)
                {
                    infoList.Add($"Physical Damage: {weapon.PhysicalDMG}");
                    colors.Add(Color.Red);
                }
                if (weapon.MagicalDMG != 0)
                {
                    infoList.Add($"Magical Damage: {weapon.MagicalDMG}");
                    colors.Add(Color.Blue);
                }
                if (weapon.SkinDMGMULTIPLIER != 1)
                {
                    infoList.Add($"Skin Damage Multiplier: {(weapon.SkinDMGMULTIPLIER * 100):F1}%");
                    colors.Add(Color.DarkRed);
                }
                if (weapon.ArmorDMGMULTIPLIER != 1)
                {
                    infoList.Add($"Armor Damage Multiplier: {(weapon.ArmorDMGMULTIPLIER * 100):F1}%");
                    colors.Add(Color.DarkRed);
                }
            }

            if (weapon.PhysicalDEF != 0)
            {
                infoList.Add($"Physical Protection: {weapon.PhysicalDEF}");
                colors.Add(Color.Green);
            }
            if (weapon.MagicalDEF != 0)
            {
                infoList.Add($"Magical Protection: {weapon.MagicalDEF}");
                colors.Add(Color.Green);
            }
            if (weapon.FireDEF != 0)
            {
                infoList.Add($"Fire Protection: {weapon.FireDEF}");
                colors.Add(Color.Orange);
            }
            if (weapon.ColdDEF != 0)
            {
                infoList.Add($"Cold Protection: {weapon.ColdDEF}");
                colors.Add(Color.LightBlue);
            }
            if (weapon.LightningDEF != 0)
            {
                infoList.Add($"Lightning Protection: {weapon.LightningDEF}");
                colors.Add(Color.Yellow);
            }
        }

        private void AddArmorInfo(Armor armor, List<string> infoList, List<Color> colors)
        {
            if (armor.PhysicalDEF != 0)
            {
                infoList.Add($"Physical Protection: {armor.PhysicalDEF}");
                colors.Add(Color.Green);
            }
            if (armor.MagicalDEF != 0)
            {
                infoList.Add($"Magical Protection: {armor.MagicalDEF}");
                colors.Add(Color.Green);
            }
            if (armor.FireDEF != 0)
            {
                infoList.Add($"Fire Protection: {armor.FireDEF}");
                colors.Add(Color.Orange);
            }
            if (armor.ColdDEF != 0)
            {
                infoList.Add($"Cold Protection: {armor.ColdDEF}");
                colors.Add(Color.LightBlue);
            }
            if (armor.LightningDEF != 0)
            {
                infoList.Add($"Lightning Protection: {armor.LightningDEF}");
                colors.Add(Color.Yellow);
            }
        }

        private void AddValueInfo(Item item, List<string> infoList, List<Color> colors)
        {
            string valueInfo = item.value switch
            {
                -4 => "Cannot be Destroyed, Dropped or Sold",
                -3 => "Cannot be Destroyed or Sold",
                -2 => "Cannot be Dropped or Sold",
                -1 => "Cannot be Sold",
                0 => "Worthless",
                _ => item.value.ToString()
            };
            infoList.Add(valueInfo);
            colors.Add(Color.Yellow);
        }


    }
}
