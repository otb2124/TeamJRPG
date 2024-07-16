using System;


namespace TeamJRPG {


    [Serializable]
    public class Configuration
    {

        public bool IsFullScreen;



        public void ApplyAll()
        {
            Globals.graphics.IsFullScreen = IsFullScreen;
            Globals.graphics.ApplyChanges();
        }
    }
}
