using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class TextButton : Button
    {
        public Vector2 frameSize;

        public TextButton(string text, Vector2 startPosition, int id) : base(Globals.assetSetter.textures[6][0][0], startPosition, id)
        {
            this.position = startPosition;
            Vector2 adjposition = new Vector2(position.X - Globals.camera.viewport.Width / 2, position.Y - Globals.camera.viewport.Height / 2);


            TextFrame textFrame = new TextFrame(text, position);
            children.Add(textFrame);

            frameSize = textFrame.frameSize*10;
            buttonBox = new System.Drawing.RectangleF(adjposition.X, adjposition.Y, frameSize.X, frameSize.Y);
        }

    }
}
