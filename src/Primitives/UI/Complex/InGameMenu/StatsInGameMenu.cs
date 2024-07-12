using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class StatsInGameMenu : UIComposite
    {


        public StatsInGameMenu()
        {
            type = UICompositeType.INGAME_MENU_STATS;


            //frame
            Vector2 frameSize = new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50, Globals.camera.viewport.Height - 40);
            Vector2 framePos = new Vector2(Globals.camera.viewport.Width / 3 * 1.75f, 10);


            Frame frame = new Frame(framePos, frameSize);

            children.Add(frame);



            //Table
        }


    }
}
