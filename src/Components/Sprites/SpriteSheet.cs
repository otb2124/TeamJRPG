using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamJRPG
{
    public class SpriteSheet
    {
        public Texture2D texture;

        public SpriteSheet(Texture2D texture)
        {
            this.texture = texture;
        }

        public Sprite GetSprite(Vector2 sheetPosition, Vector2 size)
        {
            return new Sprite(this, sheetPosition, size);
        }




        public int GetTotalNumberOfSprites(Vector2 gridItemSize)
        {
            return GetTotalCols((int)gridItemSize.X) + GetTotalRows((int)gridItemSize.Y);
        }

        public int GetTotalNumberOfSpritesWithoutEmpty(Vector2 gridItemSize)
        {
            int totalSprites = 0;
            int cols = GetTotalCols((int)gridItemSize.X);
            int rows = GetTotalRows((int)gridItemSize.Y);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Rectangle sourceRectangle = new Rectangle(col * (int)gridItemSize.X, row * (int)gridItemSize.Y, (int)gridItemSize.X, (int)gridItemSize.Y);
                    if (!IsSourceRectangleEmpty(sourceRectangle))
                    {
                        totalSprites++;
                    }
                }
            }

            return totalSprites;
        }

        public int GetTotalCols(int spriteWidth)
        {
            return texture.Width / spriteWidth;
        }

        public int GetTotalRows(int spriteHeight)
        {
            return texture.Height / spriteHeight;
        }




        private bool IsSourceRectangleEmpty(Rectangle sourceRectangle)
        {
            Color[] textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);

            for (int y = sourceRectangle.Y; y < sourceRectangle.Y + sourceRectangle.Height; y++)
            {
                for (int x = sourceRectangle.X; x < sourceRectangle.X + sourceRectangle.Width; x++)
                {
                    if (textureData[y * texture.Width + x].A != 0)
                    {
                        return false; // Found a non-transparent pixel
                    }
                }
            }
            return true; // All pixels are transparent
        }




        public int GetTotalNumberOfSpritesInRow(int rowIndex, Vector2 gridItemSize)
        {
            int totalSprites = 0;
            int cols = GetTotalCols((int)gridItemSize.X);

            for (int col = 0; col < cols; col++)
            {
                Rectangle sourceRectangle = new Rectangle(col * (int)gridItemSize.X, rowIndex * (int)gridItemSize.Y, (int)gridItemSize.X, (int)gridItemSize.Y);
                if (!IsSourceRectangleEmpty(sourceRectangle))
                {
                    totalSprites++;
                }
            }

            return totalSprites;
        }



        public int GetTotalNumberOfSpritesInCol(int colIndex, Vector2 gridItemSize)
        {
            int totalSprites = 0;
            int rows = GetTotalRows((int)gridItemSize.Y);

            for (int row = 0; row < rows; row++)
            {
                Rectangle sourceRectangle = new Rectangle(colIndex * (int)gridItemSize.X, row * (int)gridItemSize.Y, (int)gridItemSize.X, (int)gridItemSize.Y);
                if (!IsSourceRectangleEmpty(sourceRectangle))
                {
                    totalSprites++;
                }
            }

            return totalSprites;
        }
    }
}
