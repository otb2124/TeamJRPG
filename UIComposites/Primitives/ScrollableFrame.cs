using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace TeamJRPG
{
    public class ScrollableFrame : UIComposite
    {

        public float value;

        // Viewport properties
        private int startIndex; // Index of the first visible row
        private int endIndex;   // Index of the last visible row
        private int maxVisibleRows; // Maximum number of visible rows
        private int itemsPerRow; // Number of items per row
        private float rowHeight; // Height of each row
        private float scrollValue; // Current scroll position (0 to 1)
        private Vector2 scrollPosition; // Scroll position in pixels

        public Vector2 frameSize;
        public Vector2 size;

        public ScrollableFrame(Vector2 startPosition, Vector2 size, int itemsPerRow, Vector2 frameSize)
        {
            this.position = startPosition;
            this.size = size;
            this.itemsPerRow = itemsPerRow;
            this.rowHeight = frameSize.Y;
            this.type = UICompositeType.SCROLLPANE;

            // Initialize viewport properties
            startIndex = 0;
            endIndex = 0;
            maxVisibleRows = 7;
            scrollValue = 0;
            scrollPosition = Vector2.Zero;
            this.frameSize = frameSize;
        }

        public override void Update()
        {
            // Calculate the mouse wheel delta
            int mouseWheelDelta = Globals.inputManager.currentMouseState.ScrollWheelValue - Globals.inputManager.previousMouseState.ScrollWheelValue;

            // Check if the mouse wheel was scrolled
            if (mouseWheelDelta != 0)
            {
                scrollPosition.Y = MathHelper.Clamp(scrollPosition.Y - mouseWheelDelta / 10, 0, Math.Max(0, rowHeight * ((children.Count + itemsPerRow - 1) / itemsPerRow) - size.Y));
            }

            // Update the components
            foreach (var child in children)
            {
                child.Update();
            }
        }

        public override void Draw()
        {

            startIndex = (int)(scrollPosition.Y / rowHeight);
            endIndex = Math.Min(startIndex + maxVisibleRows, (children.Count + itemsPerRow - 1) / itemsPerRow);

            for (int rowIndex = startIndex; rowIndex < endIndex; rowIndex++)
            {
                for (int itemIndex = 0; itemIndex < itemsPerRow; itemIndex++)
                {
                    int childIndex = rowIndex * itemsPerRow + itemIndex;
                    if (childIndex < children.Count && children[childIndex] != null)
                    {
                        // Adjust the position of the component based on its index and scroll position
                        Vector2 drawPosition = new Vector2(
                            position.X + itemIndex * (frameSize.X + 10), 
                            position.Y + (rowIndex - startIndex) * rowHeight - (scrollPosition.Y % rowHeight)
                        );
                        children[childIndex].position = drawPosition;
                        children[childIndex].Draw();
                    }
                }
            }
        }
    }
}
