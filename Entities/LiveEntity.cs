using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class LiveEntity : Entity
    {

        public Item[] equipment;
        public Weapon weapon1;
        public Weapon weapon2;
        public Armor[] armor;


        public LiveEntity(Vector2 position) : base(position)
        {

        }
    }
}
