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
            TextFrame textFrame = new TextFrame(text, startPosition);
            components.AddRange(textFrame.components);

            frameSize = textFrame.frameSize*2;
            buttonBox = new System.Drawing.RectangleF(textFrame.position.X - frameSize.X/2, textFrame.position.Y - frameSize.Y/2, frameSize.X, frameSize.Y);
        }


        public override void Update()
        {
            base.Update();
        }
    }
}
