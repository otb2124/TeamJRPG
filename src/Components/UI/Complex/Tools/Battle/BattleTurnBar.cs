using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class BattleTurnBar : UIComposite
    {
        public BattleTurnBar()
        {
            type = UICompositeType.BATTLE_TURN_BAR;

            // Define the margin and icon size
            Vector2 margin = new Vector2(20, 0);
            Vector2 iconSize = new Vector2(64, 64);

            // Calculate the total width of all icons and margins
            float totalWidth = Globals.battleManager.turnQueue.Count * iconSize.X + (Globals.battleManager.turnQueue.Count - 1) * margin.X;

            // Calculate the starting position to center the turn bar
            Vector2 startPosition = new Vector2((Globals.camera.viewport.Width - totalWidth) / 2, 20);

            Color defaultColor = Color.White;
            Color choiceColor = Color.Red;
            Color currentColor;

            // Add frames and character icons to the turn bar
            for (int i = 0; i < Globals.battleManager.turnQueue.Count; i++)
            {

                currentColor = (i == 0) ? choiceColor : defaultColor;

                Vector2 positionOffset = startPosition + new Vector2((iconSize.X + margin.X) * i, 0);
                int entityId = Globals.battleManager.turnQueue.ElementAt(i);
                LiveEntity entity = Globals.battleManager.all[entityId];

                ImageHolder frame = new ImageHolder(
                    Globals.TextureManager.GetSprite(TextureManager.SheetCategory.ui, 2, new Vector2(0, 0), new Vector2(32, 32)),
                    positionOffset,
                    currentColor,
                    new Vector2(2, 2),
                    null);

                CharacterIconHolder cih = new CharacterIconHolder(
                    entity,
                    positionOffset,
                    Vector2.One,
                    null,
                    entity.name,
                    2);

                children.Add(frame);
                children.Add(cih);
            }


            CurrentCharacterPointer turnPointer = new CurrentCharacterPointer(Globals.battleManager.all[Globals.battleManager.turnQueue.ElementAt(0)]);
            children.Add(turnPointer);
        }
    }
}
