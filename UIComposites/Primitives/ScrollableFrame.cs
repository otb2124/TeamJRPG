using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private int scrollCounter = 0;

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
            scrollValue = 0;
            scrollPosition = Vector2.Zero;
            this.frameSize = frameSize;
        }

        public override void Update()
        {
            // Calculate the mouse wheel delta
            int mouseWheelDelta = Globals.inputManager.currentMouseState.ScrollWheelValue - Globals.inputManager.previousMouseState.ScrollWheelValue;


            scrollCounter++;
            if (scrollCounter >= 1 * 30)
            {
                scrollPosition.Y = 0;
                scrollCounter = 0;
            }

            if (mouseWheelDelta != 0)
            {
                scrollPosition.Y = MathHelper.Clamp(scrollPosition.Y - mouseWheelDelta / 100, -Math.Max(0, rowHeight * ((children.Count + itemsPerRow - 1) / itemsPerRow) - size.Y), Math.Max(0, rowHeight * ((children.Count + itemsPerRow - 1) / itemsPerRow) - size.Y));
            }

            base.Update();
        }

        public override void Draw()
        {

            startIndex = (int)(scrollPosition.Y / rowHeight);
            endIndex = Math.Min(startIndex + children.Count, (children.Count + itemsPerRow - 1) / itemsPerRow);

            for (int rowIndex = startIndex; rowIndex < endIndex; rowIndex++)
            {
                for (int itemIndex = 0; itemIndex < itemsPerRow; itemIndex++)
                {
                    int childIndex = rowIndex * itemsPerRow + itemIndex;
                    if (childIndex < children.Count && children[childIndex] != null)
                    {

                        Vector2 drawPosition = new Vector2(
                             0,
                             (scrollPosition.Y % rowHeight)
                        );
                        children[childIndex].position = drawPosition;
                        bool isOut = false;

                        for (global::System.Int32 i = 0; i < children[childIndex].components.Count; i++)
                        {
                            children[childIndex].components[i].position -= drawPosition;
                            if(children[childIndex].components[i].position.Y < frameSize.Y - 64 || children[childIndex].components[i].position.Y > frameSize.Y*6)
                            {
                                isOut = true;
                            }
                        }

                        if (!isOut)
                        {
                            children[childIndex].Draw();
                        }
                        
                    }


                }

            }
        }
    }
}
