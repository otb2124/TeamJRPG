using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class TextButton : Button
    {

        public string text;
        public int type;
        public bool IsActive;

        public TextFrame textFrame;

        public TextButton(string text, Vector2 startPosition, int fontId, int buttonID, Color color, int type = 0) : base(Globals.TextureManager.GetSprite(TextureManager.SheetCategory.placeholders, 0, new Vector2(0, 0), new Vector2(32, 32)), startPosition, Vector2.One, buttonID, null)
        {
            this.type = type;
            this.position = startPosition;
            this.text = text;
            Vector2 adjposition = new Vector2(position.X - Globals.camera.viewport.Width / 2, position.Y - Globals.camera.viewport.Height / 2);


            textFrame = new TextFrame(text, position, fontId, color, type);

            children.Add(textFrame);
            
            

            this.frameSize = textFrame.frameSize;
            buttonBox = new System.Drawing.RectangleF(adjposition.X, adjposition.Y, frameSize.X, frameSize.Y);
        }


        public override void Update()
        {
            IsActive = textFrame.IsActive;

            base.Update();
        }

    }
}
