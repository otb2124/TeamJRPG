using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class TextButton : Button
    {
        public Vector2 frameSize;
        public string text;
        public TextButton(string text, Vector2 startPosition, int fontId, int id) : base(Globals.assetSetter.textures[Globals.assetSetter.PLACEHOLDERS][0][0], startPosition, 1, id, "empty")
        {
            this.position = startPosition;
            this.text = text;
            Vector2 adjposition = new Vector2(position.X - Globals.camera.viewport.Width / 2, position.Y - Globals.camera.viewport.Height / 2);


            TextFrame textFrame = new TextFrame(text, position, fontId);
            children.Add(textFrame);

            frameSize = textFrame.frameSize;
            buttonBox = new System.Drawing.RectangleF(adjposition.X, adjposition.Y, frameSize.X, frameSize.Y);
        }

    }
}
