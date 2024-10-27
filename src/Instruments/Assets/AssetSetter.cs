using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Linq;

namespace TeamJRPG
{
    public class AssetSetter
    {

        public SpriteFont[] fonts;
        public Effect[] effects;


        public bool AllAssetsLoaded = false;





        public AssetSetter()
        {
            Globals.textureManager = new TextureManager();

            fonts = new SpriteFont[10];
            effects = new Effect[10];
        }


        public void SetAllAssets()
        {
            Globals.textureManager.SetAllSheets();
            SetFonts();
            AllAssetsLoaded = true;
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
