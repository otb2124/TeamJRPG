using Microsoft.Xna.Framework;
using System;

namespace TeamJRPG
{
    public class RandomHelper
    {

        private static Random random = new Random();

        public static int RandomInteger(int min, int max)
        {
            return random.Next(min, max);
        }

        public static float RandomFloating(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }

        public static bool RandomBool()
        {
            return random.Next(2) == 0;
        }

        public static Color RandomColor()
        {
            return new Color(RandomFloating(0f, 1f), RandomFloating(0f, 1f), RandomFloating(0f, 1f));
        }
    }
}
