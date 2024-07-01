using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class TextFrame : UIComposite
    {

        public Vector2 frameSize;

        public TextFrame(string text, Vector2 startPosition)
        {
            
            this.position = new Vector2(startPosition.X - Globals.camera.viewport.Width/2, startPosition.Y - Globals.camera.viewport.Height/2);
            this.type = UICompositeType.TEXT_FRAME;


            Label label = new Label(text, startPosition);
            Frame frame = new Frame(startPosition, label.textSize);
            frameSize = frame.frameSize;

            components.AddRange(frame.components);
            components.AddRange(label.components);


            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsStickToCamera = true;
                components[i].IsStickToZoom = true;
            }

        }

    }
}
