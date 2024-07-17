using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TeamJRPG
{
    public class BattleManager
    {

        
        public List<LiveEntity> leftSide;
        public List<LiveEntity> rightSide;

        public int backGroundSetId = 0;
        public BattleBackground background;

        public float XDistanceBetweenSprites = 64;
        public float XDistanceBetweenSides = 200;
        public float TotalEntitiyX;

        public BattleManager() 
        {
            leftSide = new List<LiveEntity>();
            rightSide = new List<LiveEntity>();
        }


        public void StartBatlle(params LiveEntity[] enemies)
        {
            
            

            background = new BattleBackground(backGroundSetId);
            leftSide.AddRange(Globals.group.members);
            rightSide.AddRange(enemies);

            float leftSideWidth = (leftSide.Count) * XDistanceBetweenSprites;
            float rightSideWidth = (rightSide.Count) * XDistanceBetweenSprites;

            TotalEntitiyX = leftSideWidth + rightSideWidth + XDistanceBetweenSides;

            Globals.camera.Reload();

            float halfBackgroundWidth = (background.foreGroundWidth - (background.foreGroundWidth - Globals.camera.viewport.Width))/2 - TotalEntitiyX/2;


            for (int i = 0; i < leftSide.Count; i++)
            {
                leftSide[i].direction = LiveEntity.Direction.right;
                leftSide[i].battleScreenPosition = new Vector2(i * XDistanceBetweenSprites + halfBackgroundWidth, Globals.camera.viewport.Height - LiveEntity.DEFAULT_HUMANOID_BODY_SPRITE_SIZE.Y*2);
            }
            for (int i = 0; i < rightSide.Count; i++)
            {
                rightSide[i].direction = LiveEntity.Direction.left;
                rightSide[i].battleScreenPosition = new Vector2(i * XDistanceBetweenSprites + leftSide.Count * XDistanceBetweenSprites + XDistanceBetweenSides + halfBackgroundWidth + TotalEntitiyX, Globals.camera.viewport.Height - LiveEntity.DEFAULT_HUMANOID_BODY_SPRITE_SIZE.Y * 2);
            }
        }

        public void Update()
        {
            background.Update();

            for (int i = 0; i < leftSide.Count; i++)
            {
                leftSide[i].Update();
            }
            for (int i = 0; i < rightSide.Count; i++)
            {
                rightSide[i].Update();
            }
        }



        public void Draw()
        {
            background.Draw();

            for (int i = 0; i < leftSide.Count; i++)
            {
                leftSide[i].Draw();
            }
            for (int i = 0; i < rightSide.Count; i++)
            {
                rightSide[i].Draw();
            }
        }
    }
}
