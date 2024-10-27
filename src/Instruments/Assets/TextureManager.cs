using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;


namespace TeamJRPG
{
    public class TextureManager
    {




        public SpriteSheet[][] spriteSheets;
        public enum SheetCategory { tiles, character_bodies, character_details, mob_bodies, entity_icons, entity_icon_backgrounds, objects_decorative, objects_interractive, items, armor_bodies, ui, placeholders, battle_backgrounds };



        public TextureManager() 
        {
            spriteSheets = new SpriteSheet[20][];

            for (int i = 0; i < spriteSheets.Length; i++)
            {
                spriteSheets[i] = new SpriteSheet[10];
            }
        }



        public void SetAllSheets()
        {

            Console.WriteLine("Setting Up Spritesheets...");

            //tiles
            AddSheet(SheetCategory.tiles, 0, "tiles/tilemap_forest");

            //characterBodies
            AddSheet(SheetCategory.character_bodies, 0, "characters/bodies/body_male_spritesheet0");

            //characterDetails

            //mobBodies

            //entityIcons
            AddSheet(SheetCategory.entity_icons, 0, "characters/icons/icon0");

            //entityIconBackGrounds
            AddSheet(SheetCategory.entity_icon_backgrounds, 0, "characters/icons/iconbackgrounds");

            //objects_decorative
            AddSheet(SheetCategory.objects_decorative, 0, "objects/decorativeObjects/decorativeObjects");

            //objects_interractive
            AddSheet(SheetCategory.objects_interractive, 0, "objects/interractiveObjects/interractiveObjects");

            //items
            //weapons
            AddSheet(SheetCategory.items, 0, "items/weaponItems/tilemap_weapon_items");
            //armors
            AddSheet(SheetCategory.items, 1, "items/armorItems/tilemap_armor_items");
            //consumables
            AddSheet(SheetCategory.items, 2, "items/consumableItems/tilemap_consumable_items");
            //materials
            AddSheet(SheetCategory.items, 3, "items/materialItems/materialItem0");
            //valuables
            AddSheet(SheetCategory.items, 4, "items/valuableItems/valuableItem0");
            //questItems
            AddSheet(SheetCategory.items, 5, "items/questItems/questItem0");
            //currency
            AddSheet(SheetCategory.items, 6, "items/currencyItems/currencyItem0");

            //armorBodies

            //ui
            AddSheet(SheetCategory.ui, 0, "ui/ui_spritesheet");
            AddSheet(SheetCategory.ui, 1, "ui/menu_title");
            AddSheet(SheetCategory.ui, 2, "ui/battle_sprites");
            AddSheet(SheetCategory.ui, 3, "ui/skills_ui");
            AddSheet(SheetCategory.ui, 4, "ui/gameover_title");

            //placeholders
            AddSheet(SheetCategory.placeholders, 0, "placeholders/placeholder_spritesheet");

            //battleBackGrounds
            AddSheet(SheetCategory.battle_backgrounds, 0, "backgrounds/backgrounds_backgroundLayer");
            AddSheet(SheetCategory.battle_backgrounds, 1, "backgrounds/backgrounds_midgroundLayer");
            AddSheet(SheetCategory.battle_backgrounds, 2, "backgrounds/backgrounds_midbackgroundLayer");
            AddSheet(SheetCategory.battle_backgrounds, 3, "backgrounds/backgrounds_foregroundLayer");


            Console.WriteLine("Spritesheets Have Been Set Up");
        }



        public Texture2D LoadTexture(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return Texture2D.FromStream(Globals.graphics.GraphicsDevice, fileStream);
            }
        }






        public Sprite GetSprite(SheetCategory cat, int packId, Vector2 sheetPosition, Vector2 size)
        {
            return new Sprite(GetSheet(cat, packId), sheetPosition, size);
        }

        public void AddSheet(SheetCategory cat, int id, string localPath)
        {
            spriteSheets[SwitchSheetCategory(cat)][id] = new SpriteSheet(LoadTexture("Content/res/" + localPath + ".png"));
        }

        public SpriteSheet GetSheet(SheetCategory cat, int id)
        {
            return spriteSheets[SwitchSheetCategory(cat)][id];
        }




        public int SwitchSheetCategory(SheetCategory cat)
        {
            switch (cat)
            {
                case SheetCategory.tiles:
                    return 0;
                case SheetCategory.character_bodies:
                    return 1;
                case SheetCategory.character_details:
                    return 3;
                case SheetCategory.mob_bodies:
                    return 4;
                case SheetCategory.entity_icons:
                    return 5;
                case SheetCategory.objects_decorative:
                    return 6;
                case SheetCategory.objects_interractive:
                    return 7;
                case SheetCategory.items:
                    return 8;
                case SheetCategory.armor_bodies:
                    return 9;
                case SheetCategory.ui:
                    return 10;
                case SheetCategory.placeholders:
                    return 11;
                case SheetCategory.entity_icon_backgrounds:
                    return 12;
                case SheetCategory.battle_backgrounds:
                    return 13;
            }
            return -1;
        }
    }
}
