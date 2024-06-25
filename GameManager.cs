using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class GameManager
    {

        Map map;


        public GameManager() 
        {
            Init();
        }



        public void Init()
        {
            map = new Map();
        }



        public void Load()
        {
            Globals.assetSetter.SetAssets();
            map.Init();
        }

        public void Update()
        {
            Globals.inputManager.Update();

            
        }



        public void Draw()
        {
            map.Draw();
        }
    }
}
