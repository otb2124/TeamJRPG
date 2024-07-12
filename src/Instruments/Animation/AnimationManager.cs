using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class AnimationManager
    {


        public Dictionary<Tuple<LiveEntity.Direction, LiveEntity.AnimationState>, Animation> anims = new Dictionary<Tuple<LiveEntity.Direction, LiveEntity.AnimationState>, Animation>();

        public Tuple<LiveEntity.Direction, LiveEntity.AnimationState> lastKey;




        public void AddAnimation(Tuple<LiveEntity.Direction, LiveEntity.AnimationState> key, Animation animation)
        {
            anims.Add(key, animation);
            lastKey ??= key;
        }



        public void Update(Tuple<LiveEntity.Direction, LiveEntity.AnimationState> key)
        {
            if (anims.ContainsKey(lastKey))
            {
                anims[key].Start();
                anims[key].Update();
                lastKey = key;
            }
            else
            {
                anims[lastKey].Stop();
                anims[lastKey].Reset();
            }
        }


        public Rectangle GetCurrentFrame()
        {
            return anims[lastKey].GetCurrentFrame();
        }
    }
}
