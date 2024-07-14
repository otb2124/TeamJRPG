using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TeamJRPG
{
    public class GroupmemberBars : UIComposite
    {


        public Vector2 generalMargin;
        public Vector2 scale;
        public Vector2 margin;
        public Vector2 iconSize;
        public bool IsVertical = true;
        private bool IsSet = false;


        public List<CharacterIconHolder> holderList;

        public GroupmemberBars(Vector2 startPos) 
        {
            type = UICompositeType.GROUP_BARS;


            generalMargin = new Vector2(10, 10);
            position += startPos + generalMargin;
            scale = new Vector2(1.5f, 1.5f);
            iconSize = new Vector2(Globals.player.characterIcon.texture.Width * scale.X, Globals.player.characterIcon.texture.Height * scale.Y);

            holderList = new List<CharacterIconHolder>();


            RefreshIcons();
        }



        public override void Update()
        {

            bool refreshCondition = Globals.group.PlayerChanged || Globals.uiManager.CheckMenuStateChange();

            if (refreshCondition)
            {
                RefreshIcons();
            }



            base.Update();
        }



        public void RefreshIcons()
        {
            for (int i = 0; i < holderList.Count; i++)
            {
                children.Remove(holderList[i]);
            }
            holderList.Clear();


            if (Globals.uiManager.currentMenuState == UIManager.MenuState.inGameMenu)
            {
                if (!IsSet)
                {
                    this.position.X += iconSize.X;
                    IsVertical = false;
                    IsSet = true;
                }
                
            }
            else if(Globals.uiManager.currentMenuState == UIManager.MenuState.clean)
            {
                if (IsSet)
                {
                    this.position.X -= iconSize.X;
                    IsVertical = true;
                    IsSet = false;
                }
            }


            if (!IsVertical)
            {
                margin = new Vector2(iconSize.X + 10, 0);
            }
            else
            {
                margin = new Vector2(0, iconSize.Y + 10);
            }

            Stroke stroke = null;

            for (int i = 0; i < Globals.group.members.Count; i++)
            {
                if (Globals.player == Globals.group.members[i])
                {
                    stroke = new Stroke(1, Color.White, MonoGame.StrokeType.OutlineAndTexture);
                }
                else
                {
                    stroke = null;
                }


                string uiHint = Globals.group.members[i].name;

                if (Globals.currentGameState == Globals.GameState.playstate)
                {
                    uiHint = null;
                }

                holderList.Add(new CharacterIconHolder(Globals.group.members[i], position + (margin * i), scale, stroke, uiHint, 1));
            }
            
            children.AddRange(holderList);

            Globals.uiManager.RemoveAllCompositesOfTypes(UICompositeType.FLOATING_INFO_BOX);
        }



        
    }
}
