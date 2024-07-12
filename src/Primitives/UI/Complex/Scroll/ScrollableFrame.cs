using Microsoft.Xna.Framework;
using System;
using System.DirectoryServices;

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

        public Vector2 childrenSize;
        public Vector2 frameSize;

        public bool IsTopLimit, IsBottomLimit;

        public int OpacityCut = 0;

        public float newPosition = 0, oldPosition = 0;

        public ScrollableFrame(Vector2 startPosition, Vector2 size, int itemsPerRow, Vector2 childrenSize)
        {
            this.position = startPosition;
            this.frameSize = size;
            this.itemsPerRow = itemsPerRow;
            this.rowHeight = childrenSize.Y;
            this.type = UICompositeType.SCROLLPANE;

            // Initialize viewport properties
            startIndex = 0;
            endIndex = 0;
            scrollValue = 0;
            scrollPosition = Vector2.Zero;
            this.childrenSize = childrenSize;
        }

        public override void Update()
        {


            //scrolling
            oldPosition = scrollPosition.Y;

            int mouseWheelDelta = Globals.inputManager.currentMouseState.ScrollWheelValue - Globals.inputManager.previousMouseState.ScrollWheelValue;

            if (mouseWheelDelta != 0)
            {
                newPosition = MathHelper.Clamp(scrollPosition.Y - mouseWheelDelta / 100, -Math.Max(0, rowHeight * ((children.Count + itemsPerRow - 1) / itemsPerRow) - frameSize.Y), Math.Max(0, rowHeight * ((children.Count + itemsPerRow - 1) / itemsPerRow) - frameSize.Y));

                scrollCounter++;
                if (scrollCounter >= 10)
                {
                    newPosition = 0;
                    scrollCounter = 0;
                }
            }

            if (IsTopLimit)
            {
                if (newPosition <= oldPosition)
                {
                    mouseWheelDelta = 0;
                    newPosition = 0;
                }
            }
            else if (IsBottomLimit)
            {
                if (newPosition >= oldPosition)
                {
                    mouseWheelDelta = 0;
                    newPosition = 0;
                }
            }

            scrollPosition.Y = newPosition;


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


                        DrawChildren(children[childIndex]);



                    }


                }

            }
        }



        public void DrawChildren(UIComposite child)
        {
            Vector2 drawPosition = new Vector2(
                             0,
                             (scrollPosition.Y % rowHeight)
                        );
            child.position = drawPosition;


            bool isOutOver = false;
            bool isOutUnder = false;

            float TopBound = position.Y - Globals.camera.viewport.Height / 2 - childrenSize.Y,
            BottomBound = position.Y - Globals.camera.viewport.Height / 2 + frameSize.Y - childrenSize.Y / 2;

            int originalSrcRectHeight = 32;

            int offset = 0;


            for (global::System.Int32 i = 0; i < child.components.Count; i++)
            {

                child.components[i].position -= drawPosition;


                if (child.components[i].position.Y < TopBound)
                {
                    isOutOver = true;
                }
                else
                {
                    isOutOver = false;

                    if (children[children.Count - 1].components.Count > 0)
                    {
                        if (children[children.Count - 1].components[i].position.Y < BottomBound - childrenSize.Y)
                        {
                            IsBottomLimit = true;
                        }
                        else
                        {
                            IsBottomLimit = false;
                        }
                    }
                    
                }


                if (child.components[i].position.Y > BottomBound)
                {
                    isOutUnder = true;
                }
                else
                {
                    isOutUnder = false;

                    if (children[0].components.Count > 0)
                    {
                        if (children[0].components[i].position.Y > TopBound + childrenSize.Y)
                        {
                            IsTopLimit = true;
                        }
                        else
                        {
                            IsTopLimit = false;
                        }
                    }
                }





                if (isOutOver)
                {

                    offset = (int)(TopBound - child.components[i].position.Y);
                    

                    if(OpacityCut == 1)
                    {
                        child.components[i].sourceRectangle.Y += offset;
                        child.components[i].sourceRectangle.Height -= offset;

                    }

                    
                        
                        int alpha = 20 - offset;
                        child.components[i].color = new Color(child.components[i].color, alpha);
                    
                        
                    
                }


                if (isOutUnder)
                {

                    offset = (int)((child.components[i].position.Y + child.components[i].sourceRectangle.Height) - BottomBound);


                    if (OpacityCut == 1)
                    {
                        child.components[i].sourceRectangle.Height -= offset;
                    }

                        int alpha = 50 - offset;
                        child.components[i].color = new Color(child.components[i].color, alpha);
                       
                    
                    
                    

                        
                    
                }



                if (!isOutUnder && !isOutOver)
                {
                    child.components[i].sourceRectangle.Y = 0;
                    child.components[i].sourceRectangle.Height = originalSrcRectHeight;
                    child.components[i].color = new Color(child.components[i].color, 255);
                    
                }
            }

            child.Draw();


            foreach(var doubleChild in child.children)
            {
                DrawChildren(doubleChild);
            }
        }
    }
}
