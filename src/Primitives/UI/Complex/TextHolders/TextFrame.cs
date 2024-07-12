﻿using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class TextFrame : UIComposite
    {

        public Vector2 frameSize;

        public TextFrame(string text, Vector2 startPosition, int fontId, Color color)
        {
            
            this.position = startPosition;
            this.type = UICompositeType.TEXT_FRAME;


            Label label = new Label(text, position, fontId, color, null);
            Frame frame = new Frame(position, label.textSize);
            frameSize = frame.frameSize;

            children.Add(frame);
            children.Add(label);

        }

    }
}