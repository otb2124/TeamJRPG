using Microsoft.Xna.Framework;
using MonoGame;


namespace TeamJRPG
{
    public class Stroke
    {


        public int size;
        public Color color;
        public StrokeType effects;

        public Stroke(int size, Color color, StrokeType effect) 
        {
            this.size = size;
            this.color = color;
            this.effects = effect;
        }
    }
}
