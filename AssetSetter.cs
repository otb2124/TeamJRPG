using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Linq;

namespace TeamJRPG
{
    public class AssetSetter
    {

        public Texture2D[][][] textures;
        public SpriteFont[] fonts;
        public Effect[] effects;


        public readonly int 
            TILES = 0,
            CHARACHTER_BODIES = 1, CHARACTER_BATTLESPRITES = 2, CHARACTER_ICONS = 3,
            CHARACTER_EYES = 10, CHARACTER_MOUTHS = 11,
            OBJECTS = 20,
            ITEMS_WEAPONS = 40, ITEMS_ARMOR = 41, ITEMS_CONSUMABLES = 42, ITEMS_MATERIALS = 43, ITEMS_VALUABLES = 44, ITEMS_QUESTITEMS = 45, ITEMS_CURRENCY = 46,
            ARMOR_BODIES = 50,
            UI = 60,
            PLACEHOLDERS = 70;


        public AssetSetter()
        {
            textures = new Texture2D[100][][];

            for (int i = 0; i < textures.Length; i++)
            {
                textures[i] = new Texture2D[20][];
            }

            fonts = new SpriteFont[10];
            effects = new Effect[10];
        }


        public void SetAssets()
        {
            SetTextures();
            SetFonts();
        }


        public void SetTextures()
        {
            //Tiles
            LoadTextures(0, "Content/res/tiles/tile");    // Load tile textures

            //Characters
            LoadTextures(1, "Content/res/characters/bodies/body"); // Load body textures
            LoadTextures(2, "Content/res/characters/battleSprites/battleSprite"); //Load BattleSprites
            LoadTextures(3, "Content/res/characters/icons/icon"); //Load Icon Sprites

            //Character Details
            LoadTextures(10, "Content/res/characters/characterDetails/eyes/eye"); //Load eyes
            LoadTextures(11, "Content/res/characters/characterDetails/mouths/mouth"); //Load mouths

            //Objects
            LoadTextures(20, "Content/res/objects/object");// Load object textures

            //Mobs
            //LoadTextures(30, "Content/res/mobs/bodies/body");// Load mob textures
            //LoadTextures(31, "Content/res/mobs/battleSprites/battleSprite"); //Load mob BattleSprites
            //LoadTextures(32, "Content/res/mobs/icons/icon"); //Load mob Icon Sprites

            //ITEMS
            LoadTextures(40, "Content/res/items/weaponItems/weaponItem");
            LoadTextures(41, "Content/res/items/armorItems/armorItem");
            LoadTextures(42, "Content/res/items/consumableItems/consumableItem");
            LoadTextures(43, "Content/res/items/materialItems/materialItem");
            LoadTextures(44, "Content/res/items/valuableItems/valuableItem");
            LoadTextures(45, "Content/res/items/questItems/questItem");
            LoadTextures(46, "Content/res/items/currencyItems/currencyItem");

            //ARMOR BODIES
            //LoadTextures(50, "Content/res/armorBodies/armor");

            //UI
            LoadTextures(60, "Content/res/ui/uielement");// Load ui textures

            //PLACEHOLDERS
            LoadTextures(70, "Content/res/placeholders/placeholder");
        }

        private void LoadTextures(int index, string basePath)
        {
            for (int i = 0; i < textures[index].Length; i++)
            {
                string directoryPath = Path.GetDirectoryName(basePath);
                string searchPattern = $"{Path.GetFileName(basePath)}{i}*.png";
                string[] files = Directory.GetFiles(directoryPath, searchPattern);

                if (files.Length > 0)
                {
                    textures[index][i] = files.Select(filePath => LoadTexture(filePath)).ToArray();
                }
            }
        }

        public Texture2D LoadTexture(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return Texture2D.FromStream(Globals.graphics.GraphicsDevice, fileStream);
            }
        }


        public void SetFonts()
        {
            string fontsDirectory = Path.Combine("Content", "res", "fonts");

            if (!Directory.Exists(fontsDirectory))
            {
                Console.WriteLine("Fonts directory not found.");
                return;
            }

            string[] fontFiles = Directory.GetFiles(fontsDirectory, "*.xnb");

            if (fontFiles.Length > 0)
            {
                fonts = new SpriteFont[fontFiles.Length];

                for (int i = 0; i < fontFiles.Length; i++)
                {
                    string fontPath = Path.Combine("res", "fonts", Path.GetFileNameWithoutExtension(fontFiles[i]));
                    fonts[i] = Globals.Content.Load<SpriteFont>(fontPath);
                }
            }
            else
            {
                Console.WriteLine("No font files found.");
            }
        }







        public Texture2D CreateSolidColorTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(Globals.graphics.GraphicsDevice, width, height);
            Color[] data = Enumerable.Repeat(color, width * height).ToArray();
            texture.SetData(data);
            return texture;
        }
    }
}
