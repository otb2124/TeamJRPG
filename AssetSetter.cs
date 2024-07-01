using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TeamJRPG
{
    public class AssetSetter
    {

        public Texture2D[][][] textures;
        public SpriteFont[] fonts;
        public Effect[] effects;

        public AssetSetter()
        {
            textures = new Texture2D[10][][];

            textures[0] = new Texture2D[10][];
            textures[1] = new Texture2D[10][];
            textures[2] = new Texture2D[10][];
            textures[3] = new Texture2D[10][];
            textures[4] = new Texture2D[20][];
            textures[5] = new Texture2D[10][];
            textures[6] = new Texture2D[10][];
            textures[7] = new Texture2D[10][];


            textures[9] = new Texture2D[10][];

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
            LoadTextures(0, "Content/res/tiles/tile");    // Load tile textures
            LoadTextures(1, "Content/res/player/player"); // Load player textures
            LoadTextures(2, "Content/res/objects/object");// Load object textures
            //LoadTextures(3, "Content/res/mobs/mob");// Load mob textures
            LoadTextures(4, "Content/res/ui/uielement");// Load ui textures
            LoadTextures(5, "Content/res/battlesprites/battlesprite");
            LoadTextures(6, "Content/res/placeholders/placeholder");
            LoadTextures(7, "Content/res/items/item");
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
