using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class PressAnyKeyCheckerLabel : UIComposite
    {


        public bool IsActive = false;

        public PressAnyKeyCheckerLabel()
        {
            type = UICompositeType.PRESS_ANY_KEY;
            string text = "Press any key to continue...";
            Vector2 textSize = Globals.assetSetter.fonts[1].MeasureString(text);
            Vector2 pakclMargin = new Vector2((Globals.camera.viewport.Width - textSize.X) / 2, (Globals.camera.viewport.Height - textSize.Y) / 2);
            this.position += pakclMargin;
            Label label = new Label(text, this.position, 1, Color.White, null);

            children.Add(label);
        }

        

    }
}
