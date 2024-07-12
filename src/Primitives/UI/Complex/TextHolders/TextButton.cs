using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class TextButton : Button
    {
        public Vector2 frameSize;
        public string text;
        public TextButton(string text, Vector2 startPosition, int fontId, int id, Color color) : base(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.placeholders, 0, new Vector2(0, 0), new Vector2(32, 32)), startPosition, 1, id, "empty")
        {
            this.position = startPosition;
            this.text = text;
            Vector2 adjposition = new Vector2(position.X - Globals.camera.viewport.Width / 2, position.Y - Globals.camera.viewport.Height / 2);


            TextFrame textFrame = new TextFrame(text, position, fontId, color);
            children.Add(textFrame);

            frameSize = textFrame.frameSize;
            buttonBox = new System.Drawing.RectangleF(adjposition.X, adjposition.Y, frameSize.X, frameSize.Y);
        }

    }
}
