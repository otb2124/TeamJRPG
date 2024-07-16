using Microsoft.Xna.Framework;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace TeamJRPG
{
    public class TextFrame : UIComposite
    {

        public Vector2 frameSize;
        public System.Drawing.RectangleF frameBox;
        public Label label;
        public Color labelOriginalColor;
        public int typeID;
        public bool LabelUpdated = false;
        public bool IsActive = false;

        public TextFrame(string text, Vector2 startPosition, int fontId, Color color, int type = 0)
        {
            this.typeID = type;
            this.position = startPosition;
            this.labelOriginalColor = color;
            this.type = UICompositeType.TEXT_FRAME;


            label = new Label(text, position, fontId, labelOriginalColor, null);
            Frame frame = new Frame(position, label.textSize);
            frameSize = frame.frameSize;


            Vector2 frameBoxPosition = new Vector2(position.X - Globals.camera.viewport.Width / 2, position.Y - Globals.camera.viewport.Height / 2);
            this.frameBox = new System.Drawing.RectangleF(frameBoxPosition.X, frameBoxPosition.Y, frameSize.X, frameSize.Y);

            if(type == 0)
            {
                children.Add(frame);
            }
            
            children.Add(label);

        }


        public override void Update()
        {
            if(typeID == 1)
            {

                IsActive = frameBox.Contains(new System.Drawing.PointF(Globals.inputManager.GetCursorPos().X, Globals.inputManager.GetCursorPos().Y));


                if (IsActive)
                {
                    if (!LabelUpdated)
                    {
                        RepaintLabel(Color.Orange);

                        LabelUpdated = true;
                    }

                }
                else
                {
                    if (LabelUpdated)
                    {
                        RepaintLabel(labelOriginalColor);

                        LabelUpdated = false;
                    }
                }

            }


            base.Update();
        }


        public void RepaintLabel(Color color)
        {
            children.Remove(label);
            label = new Label(label.text, position, label.fontID, color, null);
            children.Add(label);
        }

    }
}
