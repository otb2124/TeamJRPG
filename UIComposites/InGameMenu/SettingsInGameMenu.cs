using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class SettingsInGameMenu : UIComposite
    {
        public SettingsInGameMenu()
        {
            type = UICompositeType.INGAME_MENU_SETTINGS;

            Frame frame = new Frame(new Vector2(Globals.camera.viewport.Width / 3 * 1.75f, 10), new Vector2(Globals.camera.viewport.Width - Globals.camera.viewport.Width / 3 * 1.75f - 50, Globals.camera.viewport.Height - 40));

            components.AddRange(frame.components);



        }


    }
}
